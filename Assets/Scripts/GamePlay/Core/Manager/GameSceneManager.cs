using System.Collections;
using AChen.Events;
using GamePlay.Player.Health;
using GamePlay.Player.Movement;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZZ.SceneLoader;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private string _firstMap = "Scene_1";
    private GameObject _player;
    private bool _loading;
    [SerializeField, Min(0f)] private float _fadeDuration = 0.35f;
    private CanvasGroup _fade;
    private float _exitUnlockTime;
    public GameObject Player => _player;
    public bool IsLoading => _loading;
    public bool CanUseExit => !_loading && Time.unscaledTime >= _exitUnlockTime;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap() => SceneLoader.RegisterPersistentScene("PersistentScene");

    private void OnEnable() => EventCenter.AddListener(GameEvent.OnGameStartedBtnClicked, StartGame);
    private void OnDisable() => EventCenter.RemoveListener(GameEvent.OnGameStartedBtnClicked, StartGame);

    private IEnumerator Start()
    {
        SceneLoader.RegisterPersistentScene("PersistentScene");
        yield return null;
        Scene active = SceneManager.GetActiveScene();
        if (active.name != "Menu" && active.name != "PersistentScene") CompleteMap(active, "Default");
    }

    private void StartGame() => LoadMap(_firstMap);

    public void LoadMap(string sceneName, string spawnId = "Default")
    {
        if (_loading) return;
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError("[GameSceneManager] 场景未加入 Build Settings: " + sceneName);
            return;
        }
        _loading = true;
        StartCoroutine(Transition(sceneName, spawnId));
    }

    private IEnumerator Transition(string sceneName, string spawnId)
    {
        var movement = _player != null ? _player.GetComponent<PlayerMovement>() : null;
        var body = _player != null ? _player.GetComponent<Rigidbody2D>() : null;
        bool movementEnabled = movement != null && movement.enabled;
        bool simulated = body != null && body.simulated;
        if (movement != null) movement.enabled = false;
        if (body != null) { body.linearVelocity = Vector2.zero; body.simulated = false; }
        yield return FadeTo(1f);

        Scene target = SceneManager.GetSceneByName(sceneName);
        bool newlyLoaded = !target.IsValid() || !target.isLoaded;
        if (newlyLoaded)
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            if (loading != null) yield return loading;
            target = SceneManager.GetSceneByName(sceneName);
        }
        MapSpawnPoint spawn = FindSpawn(target, spawnId);
        bool valid = target.IsValid() && target.isLoaded && spawn != null && (_player != null || _playerPrefab != null);
        if (valid)
        {
            bool completed = false;
            SceneLoader.LoadScene(sceneName, onSceneLoadCompleteCallback: () => completed = true);
            yield return new WaitUntil(() => completed);
            CompleteMap(target, spawnId);
            if (movement == null && _player != null)
            {
                movement = _player.GetComponent<PlayerMovement>();
                movementEnabled = movement != null && movement.enabled;
            }
            if (body != null) body.simulated = simulated;
            if (movement != null) movement.enabled = false;
            // 等待新相机接管和 UI 绑定后再揭开画面。
            yield return null;
            yield return null;
        }
        else
        {
            Debug.LogError("[GameSceneManager] 切换取消，目标地图或出生点无效: " + sceneName + "/" + spawnId);
            if (newlyLoaded && target.IsValid() && target.isLoaded) yield return SceneManager.UnloadSceneAsync(target);
        }
        yield return FadeTo(0f);
        if (body != null) body.simulated = simulated;
        if (movement != null) movement.enabled = movementEnabled;
        _exitUnlockTime = Time.unscaledTime + 0.5f;
        _loading = false;
    }

    private IEnumerator FadeTo(float alpha)
    {
        if (_fade == null)
        {
            var overlay = new GameObject("SceneTransition", typeof(RectTransform), typeof(Canvas), typeof(CanvasGroup), typeof(UnityEngine.UI.GraphicRaycaster));
            SceneManager.MoveGameObjectToScene(overlay, gameObject.scene);
            var canvas = overlay.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 32760;
            _fade = overlay.GetComponent<CanvasGroup>();
            _fade.alpha = 0;
            var image = new GameObject("Black", typeof(RectTransform), typeof(UnityEngine.UI.Image));
            image.transform.SetParent(overlay.transform, false);
            var rect = image.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = rect.offsetMax = Vector2.zero;
            image.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
        _fade.blocksRaycasts = true;
        float from = _fade.alpha;
        float elapsed = 0;
        while (elapsed < _fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            _fade.alpha = Mathf.Lerp(from, alpha, Mathf.SmoothStep(0, 1, elapsed / _fadeDuration));
            yield return null;
        }
        _fade.alpha = alpha;
        _fade.blocksRaycasts = alpha > 0f;
    }

    private MapSpawnPoint FindSpawn(Scene scene, string spawnId)
    {
        foreach (var point in FindObjectsByType<MapSpawnPoint>(FindObjectsSortMode.None))
            if (point.gameObject.scene == scene && point.SpawnId == spawnId) return point;
        return null;
    }

    private void CompleteMap(Scene scene, string spawnId)
    {
        MapSpawnPoint spawn = FindSpawn(scene, spawnId);
        if (spawn == null) { Debug.LogError("[GameSceneManager] 缺少出生点: " + scene.name + "/" + spawnId); return; }
        if (_player == null)
        {
            if (_playerPrefab == null) { Debug.LogError("[GameSceneManager] 未配置玩家 Prefab"); return; }
            _player = Instantiate(_playerPrefab, spawn.transform.position, spawn.transform.rotation);
            SceneManager.MoveGameObjectToScene(_player, gameObject.scene);
        }
        _player.transform.SetPositionAndRotation(spawn.transform.position, spawn.transform.rotation);
        var body = _player.GetComponent<Rigidbody2D>();
        if (body != null) { body.position = spawn.transform.position; body.linearVelocity = Vector2.zero; body.angularVelocity = 0; }
        _player.GetComponent<PlayerHealth>()?.SetRespawnPoint(spawn.transform);
        _player.SetActive(true);
        _player.GetComponent<PlayerMovement>()?.ResetAfterRespawn();
        EventCenter.Dispatch(GameEvent.BindCamera2Transform, _player.transform);
        EventCenter.Dispatch(GameEvent.OnGameSceneLoaded);
    }
}
