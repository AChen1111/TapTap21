using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace FXSystem.Demo
{
    /// <summary>
    /// 自包含 Demo：生成粒子 Prefab、登记到 FX、搭建场景物体和操作说明。
    /// </summary>
    [DefaultExecutionOrder(-50)]
    public class FXDemoController : MonoBehaviour
    {
        public const string IdHit = "demo_hit";
        public const string IdBurst = "demo_burst";
        public const string IdAura = "demo_aura";
        public const string IdUiClick = "demo_ui_click";
        public const string IdUiGlow = "demo_ui_glow";

        Transform _player;
        Transform _templates;
        Text _log;
        FXHandle _aura;
        FXHandle _follow;
        bool _followOn;
        int _logLines;

        void Awake()
        {
            _templates = new GameObject("FXDemo_Templates").transform;
            _templates.SetParent(transform, false);

            RegisterWorld(IdHit, CreateParticles("Hit", new Color(1f, 0.55f, 0.12f), 0.55f, 0.4f, looping: false, burstCount: 24, speed: 4.5f), looping: false, prewarm: 6, max: 24);
            RegisterWorld(IdBurst, CreateParticles("Burst", new Color(0.35f, 0.9f, 1f), 0.7f, 0.55f, looping: false, burstCount: 36, speed: 3.4f), looping: false, prewarm: 4, max: 12);
            RegisterWorld(IdAura, CreateParticles("Aura", new Color(0.4f, 1f, 0.5f), 0.35f, 0.7f, looping: true, burstCount: 0, speed: 1.1f, rate: 28f), looping: true, prewarm: 2, max: 4);

            var uiClick = RegisterUi(IdUiClick, CreateParticles("UiClick", new Color(1f, 0.92f, 0.25f), 0.12f, 0.28f, looping: false, burstCount: 10, speed: 1.1f), looping: false);
            uiClick.UIScale = 1f;
            uiClick.SortingOrder = 80;

            var uiGlow = RegisterUi(IdUiGlow, CreateParticles("UiGlow", new Color(0.75f, 0.5f, 1f), 0.08f, 0.4f, looping: true, burstCount: 0, speed: 0.35f, rate: 12f), looping: true);
            uiGlow.UIScale = 1f;
            uiGlow.SortingOrder = 40;
        }

        void Start()
        {
            BuildWorld();
            var canvas = BuildUi();
            FX.BindCanvas(canvas);
            Log("Demo 已就绪。左键斩击，WASD 移动。");
        }

        void Update()
        {
            MovePlayer();
            HandleHotkeys();
        }

        void MovePlayer()
        {
            if (_player == null)
                return;

            var keyboard = Keyboard.current;
            if (keyboard == null)
                return;

            var move = Vector3.zero;
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
                move.y += 1f;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
                move.y -= 1f;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                move.x -= 1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                move.x += 1f;

            if (move.sqrMagnitude > 0f)
                _player.position += move.normalized * (6f * Time.deltaTime);
        }

        void HandleHotkeys()
        {
            var mouse = Mouse.current;
            var keyboard = Keyboard.current;

            if (mouse != null && mouse.leftButton.wasPressedThisFrame && !PointerOverUi())
                PlayHitAtPointer();

            if (keyboard == null)
                return;

            if (keyboard.spaceKey.wasPressedThisFrame)
                PlayBurstOnPlayer();
            if (keyboard.qKey.wasPressedThisFrame)
                ToggleAura();
            if (keyboard.eKey.wasPressedThisFrame)
                ToggleFollow();
            if (keyboard.rKey.wasPressedThisFrame)
                StopWorldFx();
            if (keyboard.fKey.wasPressedThisFrame)
                PlayUiAtPointer();
            if (keyboard.cKey.wasPressedThisFrame)
            {
                FX.StopAllUI();
                Log("StopAllUI()");
            }
        }

        public void PlayHitAtPointer()
        {
            Vector3 world = PointerWorld();
            Vector2 facing = world - (_player != null ? _player.position : Vector3.zero);
            if (facing.sqrMagnitude < 0.01f)
                facing = Vector2.right;

            var p = FXPlayParams.At(world);
            p.Rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg);
            p.OnComplete = (handle, reason) =>
            {
                if (reason == FXCompleteReason.Finished)
                    Log($"hit 播完 Finished  handleValid={handle.IsValid}");
            };
            FX.Play("FX1", in p);
            Log($"FX.Play(\"{IdHit}\", 指向鼠标)");
        }

        public void PlayBurstOnPlayer()
        {
            if (_player == null)
                return;
            FX.Play(IdBurst, _player.position, () => Log("burst 结束回调（任意原因）"));
            Log($"FX.Play(\"{IdBurst}\", player.position, onComplete)");
        }

        public void ToggleAura()
        {
            if (_aura.IsValid)
            {
                _aura.Stop();
                Log("aura.Stop() 循环光环停止发射");
                return;
            }

            _aura = FX.PlayAttached(IdAura, _player);
            Log($"FX.PlayAttached(\"{IdAura}\", player)  句柄有效={_aura.IsValid}");
        }

        public void ToggleFollow()
        {
            if (_followOn && _follow.IsValid)
            {
                _follow.Stop(immediate: true);
                _followOn = false;
                Log("follow.Stop(immediate) 取消跟随");
                return;
            }

            _follow = FX.PlayFollow(IdAura, _player, new Vector3(0f, 0.6f, 0f));
            _followOn = _follow.IsValid;
            Log($"FX.PlayFollow(\"{IdAura}\", player, offset)  不改父节点");
        }

        public void PlayUiAtPointer()
        {
            Vector2 screen = PointerScreen();
            FX.PlayUI(IdUiClick, screen, (handle, reason) =>
            {
                if (reason == FXCompleteReason.Finished)
                    Log("UI click Finished");
            });
            Log($"FX.PlayUI(\"{IdUiClick}\", screen)");
        }

        public void PlayUiOnButton(RectTransform target)
        {
            FX.PlayUIAttached(IdUiClick, target);
            Log($"FX.PlayUIAttached(\"{IdUiClick}\", button)");
        }

        public void ToggleUiGlow(RectTransform target)
        {
            FX.PlayUIFollow(IdUiGlow, target);
            Log($"FX.PlayUIFollow(\"{IdUiGlow}\", button)  再点一次不会自动停，用 C 清 UI");
        }

        public void StopWorldFx()
        {
            FX.StopAll();
            _aura = FXHandle.Invalid;
            _follow = FXHandle.Invalid;
            _followOn = false;
            Log("FX.StopAll()");
        }

        void BuildWorld()
        {
            var cam = Camera.main;
            if (cam != null)
                cam.backgroundColor = new Color(0.07f, 0.09f, 0.14f);

            _player = CreateSprite("Player", new Color(0.95f, 0.35f, 0.4f), new Vector3(0f, -1f, 0f), 0.7f).transform;
            CreateSprite("DummyA", new Color(0.3f, 0.45f, 0.7f), new Vector3(-4f, 1.2f, 0f), 0.5f);
            CreateSprite("DummyB", new Color(0.3f, 0.6f, 0.45f), new Vector3(4f, 0.8f, 0f), 0.5f);
        }

        Canvas BuildUi()
        {
            EnsureEventSystem();

            var canvasGo = new GameObject("DemoCanvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvasGo.layer = LayerMask.NameToLayer("UI");
            var canvas = canvasGo.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = 10f;
            canvas.sortingOrder = 100;
            canvas.overrideSorting = true;

            var scaler = canvasGo.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;

            var panel = CreateUiPanel(canvas.transform, "Hud", new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0f, -210f), new Vector2(0f, 0f), new Color(0f, 0f, 0f, 0.55f));

            var help = CreateUiText(panel, "Help",
                "FX System Demo\n" +
                "左键：世界斩击（朝向鼠标）   WASD：移动红块\n" +
                "空格：自身爆炸   Q：挂接循环光环   E：跟随光环   R：StopAll 世界\n" +
                "F：鼠标处 UI 特效   下方按钮：挂到 UI 上   C：StopAllUI",
                22, TextAnchor.UpperLeft);
            Stretch(help.rectTransform, new Vector2(16f, 16f), new Vector2(-16f, -16f));

            _log = CreateUiText(panel, "Log", "", 20, TextAnchor.LowerLeft);
            var logRt = _log.rectTransform;
            logRt.anchorMin = new Vector2(0.42f, 0f);
            logRt.anchorMax = new Vector2(1f, 1f);
            logRt.offsetMin = new Vector2(8f, 12f);
            logRt.offsetMax = new Vector2(-16f, -12f);
            _log.color = new Color(1f, 0.92f, 0.55f);

            var bar = CreateUiPanel(canvas.transform, "Buttons", new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 0f), new Vector2(0f, 88f), new Color(0f, 0f, 0f, 0.4f));
            var layout = bar.gameObject.AddComponent<HorizontalLayoutGroup>();
            layout.padding = new RectOffset(16, 16, 12, 12);
            layout.spacing = 12f;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            AddButton(bar, "斩击 Play", PlayHitAtPointer);
            AddButton(bar, "爆炸 Burst", PlayBurstOnPlayer);
            AddButton(bar, "光环 Attached", ToggleAura);
            AddButton(bar, "跟随 Follow", ToggleFollow);
            var uiBtn = AddButton(bar, "按钮上 UI", null);
            uiBtn.onClick.AddListener(() => PlayUiOnButton(uiBtn.GetComponent<RectTransform>()));
            var glowBtn = AddButton(bar, "按钮跟随光", null);
            glowBtn.onClick.AddListener(() => ToggleUiGlow(glowBtn.GetComponent<RectTransform>()));
            AddButton(bar, "StopAll", StopWorldFx);

            return canvas;
        }

        void RegisterWorld(string id, GameObject prefab, bool looping, int prewarm, int max)
        {
            var def = ScriptableObject.CreateInstance<FXDefinition>();
            def.name = id;
            def.Id = id;
            def.Prefab = prefab;
            def.Space = FXSpace.World;
            def.Looping = looping;
            def.PrewarmCount = prewarm;
            def.MaxActive = max;
            def.Overflow = FXOverflowPolicy.RecycleOldest;
            def.SortingLayer = "Default";
            def.SortingOrder = 20;
            def.RecycleOnSceneChange = true;
            FX.Register(def);
        }

        FXDefinition RegisterUi(string id, GameObject prefab, bool looping)
        {
            var def = ScriptableObject.CreateInstance<FXDefinition>();
            def.name = id;
            def.Id = id;
            def.Prefab = prefab;
            def.Space = FXSpace.UI;
            def.Looping = looping;
            def.PrewarmCount = 4;
            def.MaxActive = 16;
            def.Overflow = FXOverflowPolicy.RecycleOldest;
            def.SortingLayer = "Default";
            def.SortingOrder = 40;
            def.OverrideCanvasSorting = true;
            def.RecycleOnSceneChange = true;
            def.UIScale = 1f;
            FX.Register(def);
            return def;
        }

        GameObject CreateParticles(string name, Color color, float size, float duration, bool looping, short burstCount, float speed, float rate = 0f)
        {
            var go = new GameObject(name);
            go.transform.SetParent(_templates, false);
            var ps = go.AddComponent<ParticleSystem>();
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            var main = ps.main;
            main.playOnAwake = false;
            main.loop = looping;
            main.duration = Mathf.Max(0.2f, duration);
            main.startLifetime = looping ? 0.55f : 0.5f;
            main.startSpeed = speed;
            main.startSize = size;
            main.startColor = color;
            main.gravityModifier = 0f;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            main.maxParticles = 128;
            main.scalingMode = ParticleSystemScalingMode.Local;
            main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;
            main.stopAction = ParticleSystemStopAction.None;

            var emission = ps.emission;
            emission.enabled = true;
            emission.rateOverTime = looping ? rate : 0f;
            if (!looping && burstCount > 0)
                emission.SetBursts(new[] { new ParticleSystem.Burst(0f, (short)Mathf.Max((int)burstCount, 8)) });

            var shape = ps.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Circle;
            shape.radius = looping ? 0.22f : 0.08f;
            shape.radiusThickness = 1f;

            var sizeLife = ps.sizeOverLifetime;
            sizeLife.enabled = true;
            sizeLife.size = new ParticleSystem.MinMaxCurve(1f, AnimationCurve.EaseInOut(0f, 1f, 1f, 0.15f));

            var colorLife = ps.colorOverLifetime;
            colorLife.enabled = true;
            var grad = new Gradient();
            grad.SetKeys(
                new[] { new GradientColorKey(Color.white, 0f), new GradientColorKey(color, 0.4f), new GradientColorKey(color, 1f) },
                new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 0.35f), new GradientAlphaKey(0f, 1f) });
            colorLife.color = grad;

            var renderer = go.GetComponent<ParticleSystemRenderer>();
            renderer.enabled = true;
            renderer.renderMode = ParticleSystemRenderMode.Billboard;
            renderer.alignment = ParticleSystemRenderSpace.View;
            renderer.sharedMaterial = CreateParticleMaterial();
            renderer.sortingLayerName = "Default";
            renderer.sortingOrder = 80;

            go.SetActive(false);
            return go;
        }

        static Material CreateParticleMaterial()
        {
            var texture = CreateSoftParticleTexture();
            var shader = Shader.Find("Universal Render Pipeline/Particles/Unlit")
                         ?? Shader.Find("Universal Render Pipeline/2D/Sprite-Unlit-Default")
                         ?? Shader.Find("Universal Render Pipeline/Unlit")
                         ?? Shader.Find("Sprites/Default")
                         ?? Shader.Find("UI/Default");

            if (shader == null)
            {
                Debug.LogError("[FX Demo] 找不到可用的粒子 Shader，特效将不可见。");
                return null;
            }

            var mat = new Material(shader) { name = "FXDemoParticle" };
            mat.mainTexture = texture;
            if (mat.HasProperty("_BaseMap"))
                mat.SetTexture("_BaseMap", texture);
            if (mat.HasProperty("_MainTex"))
                mat.SetTexture("_MainTex", texture);
            if (mat.HasProperty("_Color"))
                mat.SetColor("_Color", Color.white);
            if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", Color.white);
            if (mat.HasProperty("_Cutoff"))
                mat.SetFloat("_Cutoff", 0.05f);
            return mat;
        }

        static Texture2D CreateSoftParticleTexture()
        {
            const int size = 64;
            var tex = new Texture2D(size, size, TextureFormat.RGBA32, false)
            {
                name = "FXDemoSoftParticle",
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Bilinear
            };

            float center = (size - 1) * 0.5f;
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float dx = (x - center) / center;
                    float dy = (y - center) / center;
                    float alpha = Mathf.Clamp01(1f - Mathf.Sqrt(dx * dx + dy * dy));
                    alpha *= alpha;
                    tex.SetPixel(x, y, new Color(1f, 1f, 1f, alpha));
                }
            }

            tex.Apply(false, false);
            return tex;
        }

        GameObject CreateSprite(string name, Color color, Vector3 position, float scale)
        {
            var go = new GameObject(name);
            go.transform.position = position;
            go.transform.localScale = Vector3.one * scale;
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0f, 0f, 1f, 1f), new Vector2(0.5f, 0.5f), 1f);
            sr.color = color;
            sr.sortingOrder = 5;
            return go;
        }

        RectTransform CreateUiPanel(Transform parent, string name, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax, Color color)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            go.layer = LayerMask.NameToLayer("UI");
            var rt = go.GetComponent<RectTransform>();
            rt.SetParent(parent, false);
            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;
            rt.offsetMin = offsetMin;
            rt.offsetMax = offsetMax;
            go.GetComponent<Image>().color = color;
            return rt;
        }

        Text CreateUiText(Transform parent, string name, string content, int size, TextAnchor align)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            go.layer = LayerMask.NameToLayer("UI");
            go.transform.SetParent(parent, false);
            var text = go.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            if (text.font == null)
                text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = size;
            text.color = Color.white;
            text.alignment = align;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.text = content;
            return text;
        }

        Button AddButton(Transform parent, string label, UnityEngine.Events.UnityAction onClick)
        {
            var go = new GameObject(label, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            go.layer = LayerMask.NameToLayer("UI");
            go.transform.SetParent(parent, false);
            var image = go.GetComponent<Image>();
            image.color = new Color(0.18f, 0.22f, 0.32f, 0.95f);
            var button = go.GetComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = new Color(0.3f, 0.4f, 0.55f);
            colors.pressedColor = new Color(0.12f, 0.16f, 0.22f);
            button.colors = colors;
            if (onClick != null)
                button.onClick.AddListener(onClick);

            var text = CreateUiText(go.transform, "Label", label, 22, TextAnchor.MiddleCenter);
            Stretch(text.rectTransform, Vector2.zero, Vector2.zero);
            var le = go.AddComponent<LayoutElement>();
            le.preferredHeight = 64f;
            le.minWidth = 140f;
            return button;
        }

        static void Stretch(RectTransform rt, Vector2 min, Vector2 max)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = min;
            rt.offsetMax = max;
        }

        static void EnsureEventSystem()
        {
            if (FindFirstObjectByType<EventSystem>() != null)
                return;
            var go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<InputSystemUIInputModule>();
        }

        Vector3 PointerWorld()
        {
            var cam = Camera.main;
            Vector2 screen = PointerScreen();
            if (cam == null)
                return screen;
            var z = -cam.transform.position.z;
            return cam.ScreenToWorldPoint(new Vector3(screen.x, screen.y, z));
        }

        static Vector2 PointerScreen()
        {
            var mouse = Mouse.current;
            return mouse != null ? mouse.position.ReadValue() : Vector2.zero;
        }

        static bool PointerOverUi()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }

        void Log(string message)
        {
            Debug.Log("[FX Demo] " + message);
            if (_log == null)
                return;
            _logLines++;
            _log.text = message + "\n" + _log.text;
            if (_logLines > 12)
            {
                int cut = _log.text.LastIndexOf('\n');
                if (cut > 0)
                    _log.text = _log.text.Substring(0, cut);
            }
        }
    }
}
