using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using ZZ.ZTween;

public class ActorTest : MonoBehaviour
{
    Transform actor;
    Transform coin;
    Transform marker;
    SpriteRenderer actorSprite;
    SpriteRenderer coinSprite;
    Text titleText;
    Text hintText;
    Vector3 actorHome;
    Vector3 coinHome;
    Vector3 actorScale;
    Vector3 coinScale;
    Color actorColor;
    Color coinColor;

    int index;
    bool finished;
    readonly List<Step> steps = new();

    void Awake()
    {
        BuildWorld();
        BuildHud();
        Capture();
        BuildSteps();
    }

    void Start()
    {
        PlayCurrent();
    }

    void Update()
    {
        var keyboard=Keyboard.current;
        if(keyboard==null||!keyboard.spaceKey.wasPressedThisFrame)return;
        if(finished)
        {
            finished=false;
            index=0;
            PlayCurrent();
            return;
        }
        Next();
    }

    void Next()
    {
        index++;
        if(index>=steps.Count)
        {
            finished=true;
            ZTweenUtility.KillAll();
            Restore();
            titleText.text="演示结束";
            hintText.text="空格：从头再看一遍";
            Debug.Log("[ZTween Actor] 全部效果已演示完。按空格重新开始。");
            return;
        }
        PlayCurrent();
    }

    void PlayCurrent()
    {
        var step=steps[index];
        ZTweenUtility.KillAll();
        Restore();
        titleText.text=$"{index+1}/{steps.Count}  {step.name}";
        hintText.text="空格：下一个效果";
        Debug.Log($"[ZTween Actor] {index+1}/{steps.Count}  {step.name}  —  {step.desc}");
        step.play();
    }

    void BuildSteps()
    {
        Add("ZMoveTo", "走到右侧", ()=>actor.ZMoveTo(actorHome+new Vector3(2.2f,0f,0f),0.4f));
        Add("ZLocalMove", "本地上移", ()=>actor.ZLocalMove(actor.localPosition+new Vector3(0f,1.2f,0f),0.35f));
        Add("ZJump", "原地跳跃", ()=>actor.ZJump(1.4f,0.4f));
        Add("ZJumpTo", "跳到右侧", ()=>actor.ZJumpTo(actorHome+new Vector3(2.2f,0f,0f),1.4f,0.45f));
        Add("ZPunchScale", "缩放冲击", ()=>actor.ZPunchScale(0.22f,0.25f));
        Add("ZPunchPosition", "位移冲击", ()=>actor.ZPunchPosition(0.22f,new Vector3(0.35f,0f,0f)));
        Add("ZPunchRotation", "旋转冲击", ()=>actor.ZPunchRotation(0.22f,20f));
        Add("ZShake", "受击抖动", ()=>actor.ZShake(0.22f,0.2f));
        Add("ZPop", "弹出出现", ()=>actor.ZPop());
        Add("ZSquash", "落地挤压", ()=>actor.ZSquash());
        Add("ZFadeOut", "精灵淡出", ()=>actorSprite.ZFadeOut(0.35f));
        Add("ZFadeIn", "精灵淡入", ()=>
        {
            var c=actorSprite.color;
            c.a=0f;
            actorSprite.color=c;
            actorSprite.ZFadeIn(0.35f);
        });
        Add("ZFade", "淡到半透明", ()=>actorSprite.ZFade(0.35f,0.3f));
        Add("ZTint", "染成红色", ()=>actorSprite.ZTint(new Color(0.95f,0.25f,0.25f),0.25f));
        Add("ZFlash", "闪白", ()=>actorSprite.ZFlash(Color.white,0.16f));
        Add("ZBlink", "无敌闪烁（循环）", ()=>actorSprite.ZBlink(0.12f,-1));
        Add("ZFlipX", "转身翻转", ()=>actor.ZFlipX(0.16f));
        Add("ZSpawn", "出生：弹出 + 淡入", ()=>
        {
            actor.gameObject.SetActive(true);
            actor.ZSpawn();
        });
        Add("ZDespawn", "消失：缩小 + 淡出", ()=>actor.ZDespawn());
        Add("ZLand", "落地挤压", ()=>actor.ZLand());
        Add("ZHit", "受击：闪白 + 后坐 + 抖动", ()=>actor.ZHit(Vector3.right));
        Add("ZDash", "冲刺到右侧", ()=>actor.ZDash(actorHome+new Vector3(2.4f,0f,0f),0.2f));
        Add("ZKnockback", "向左击退", ()=>actor.ZKnockback(Vector3.left,1.1f,0.25f));
        Add("ZCollect", "金币飞向角色", ()=>
        {
            coin.gameObject.SetActive(true);
            coin.ZCollect(actor.position,0.4f);
        });
        Add("ZIdleBob", "待机上下漂（循环）", ()=>actor.ZIdleBob(0.55f,0.18f,-1));
        Add("ZAttackSwing", "近战挥击", ()=>actor.ZAttackSwing(0.18f,28f));
    }

    void Add(string name,string desc,Action play)
    {
        steps.Add(new Step{name=name,desc=desc,play=play});
    }

    void BuildWorld()
    {
        var sprite=WhiteSprite();
        actor=EnsureActor("ActorDemo",new Vector3(0f,0f,0f),new Vector3(1.4f,1.4f,1f),new Color(0.35f,0.65f,1f,1f),sprite);
        coin=EnsureActor("ActorCoin",new Vector3(2.4f,0.35f,0f),new Vector3(0.55f,0.55f,1f),new Color(1f,0.82f,0.2f,1f),sprite);
        marker=EnsureActor("ActorTarget",new Vector3(2.4f,-1.15f,0f),new Vector3(2.2f,0.18f,1f),new Color(0.25f,0.28f,0.34f,0.9f),sprite);
        actorSprite=actor.GetComponent<SpriteRenderer>();
        coinSprite=coin.GetComponent<SpriteRenderer>();
        marker.GetComponent<SpriteRenderer>().sortingOrder=-1;
    }

    void BuildHud()
    {
        var canvasGo=GameObject.Find("ActorDemoHUD");
        if(canvasGo==null)
        {
            canvasGo=new GameObject("ActorDemoHUD",typeof(RectTransform),typeof(Canvas),typeof(CanvasScaler),typeof(GraphicRaycaster));
            var canvas=canvasGo.GetComponent<Canvas>();
            canvas.renderMode=RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder=50;
            var scaler=canvasGo.GetComponent<CanvasScaler>();
            scaler.uiScaleMode=CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution=new Vector2(1920f,1080f);
        }
        var font=UiFont();
        titleText=EnsureText(canvasGo.transform,"ActorTitle",new Vector2(0f,460f),new Vector2(1400f,70f),36,font,"Actor Demo");
        hintText=EnsureText(canvasGo.transform,"ActorHint",new Vector2(0f,-480f),new Vector2(1200f,48f),24,font,"空格：下一个效果");
        hintText.color=new Color(0.8f,0.85f,0.95f,0.9f);
    }

    void Capture()
    {
        actorHome=actor.position;
        coinHome=coin.position;
        actorScale=actor.localScale;
        coinScale=coin.localScale;
        actorColor=actorSprite.color;
        coinColor=coinSprite.color;
    }

    void Restore()
    {
        actor.gameObject.SetActive(true);
        coin.gameObject.SetActive(true);
        actor.position=actorHome;
        coin.position=coinHome;
        actor.localScale=actorScale;
        coin.localScale=coinScale;
        actor.localEulerAngles=Vector3.zero;
        coin.localEulerAngles=Vector3.zero;
        actorSprite.color=actorColor;
        coinSprite.color=coinColor;
    }

    static Sprite whiteSprite;
    static Sprite WhiteSprite()
    {
        if(whiteSprite!=null)return whiteSprite;
        var tex=Texture2D.whiteTexture;
        whiteSprite=Sprite.Create(tex,new Rect(0f,0f,tex.width,tex.height),new Vector2(0.5f,0.5f),4f);
        return whiteSprite;
    }

    static Font UiFont()
    {
        var font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        if(font==null)font=Font.CreateDynamicFontFromOSFont("Arial",24);
        return font;
    }

    static Transform EnsureActor(string name,Vector3 pos,Vector3 scale,Color color,Sprite sprite)
    {
        var go=GameObject.Find(name);
        if(go==null)go=new GameObject(name);
        go.transform.position=pos;
        go.transform.localScale=scale;
        go.transform.rotation=Quaternion.identity;
        var sr=go.GetComponent<SpriteRenderer>();
        if(sr==null)sr=go.AddComponent<SpriteRenderer>();
        sr.sprite=sprite;
        sr.color=color;
        go.SetActive(true);
        return go.transform;
    }

    static Text EnsureText(Transform parent,string name,Vector2 pos,Vector2 size,int fontSize,Font font,string content)
    {
        var t=parent.Find(name);
        GameObject go;
        if(t==null)
        {
            go=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Text));
            go.transform.SetParent(parent,false);
        }
        else go=t.gameObject;
        var rt=(RectTransform)go.transform;
        rt.anchorMin=rt.anchorMax=rt.pivot=new Vector2(0.5f,0.5f);
        rt.anchoredPosition=pos;
        rt.sizeDelta=size;
        var text=go.GetComponent<Text>();
        if(text==null)text=go.AddComponent<Text>();
        text.font=font;
        text.fontSize=fontSize;
        text.alignment=TextAnchor.MiddleCenter;
        text.color=Color.white;
        text.text=content;
        text.raycastTarget=false;
        return text;
    }

    class Step
    {
        public string name;
        public string desc;
        public Action play;
    }
}
