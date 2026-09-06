using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class BuildZTweenDemoUI
{
    public static string Main()
    {
        var canvas = GameObject.Find("Canvas");
        if(canvas==null)return "Canvas not found";

        var sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        var font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        if(font==null)font=Font.CreateDynamicFontFromOSFont("Arial",24);
        var root = canvas.transform;

        RenameIfExists("/GameObject","ZTweenDemo");
        RenameIfExists("/Canvas/Image","DemoPanel");

        Image("DemoPanel",root,new Vector2(0f,40f),new Vector2(320f,200f),new Color(0.23f,0.32f,0.52f,1f),sprite,true);
        Image("DemoIcon",root,new Vector2(-280f,40f),new Vector2(96f,96f),new Color(0.98f,0.76f,0.22f,1f),sprite,false);
        Image("DemoButton",root,new Vector2(280f,40f),new Vector2(160f,72f),new Color(0.28f,0.58f,0.92f,1f),sprite,false);
        Text("DemoTitle",root,new Vector2(0f,460f),new Vector2(1400f,70f),36,font,"ZTween Demo",Color.white);
        Text("DemoHint",root,new Vector2(0f,-480f),new Vector2(1200f,48f),24,font,"空格：下一个效果",new Color(0.8f,0.85f,0.95f,0.9f));
        Text("DemoText",root,new Vector2(0f,-140f),new Vector2(720f,56f),30,font,"Score 100",Color.white);
        Image("DemoBar",root,new Vector2(0f,-250f),new Vector2(420f,28f),new Color(0.25f,0.82f,0.42f,1f),sprite,false);
        Image("DemoDelayedBar",root,new Vector2(0f,-250f),new Vector2(420f,28f),new Color(0.85f,0.28f,0.28f,1f),sprite,false);
        Image("DemoCard",root,new Vector2(0f,-360f),new Vector2(130f,180f),new Color(0.78f,0.36f,0.42f,1f),sprite,false);
        Image("DemoDimA",root,new Vector2(-280f,-140f),new Vector2(88f,88f),new Color(0.4f,0.4f,0.45f,1f),sprite,false);
        Image("DemoDimB",root,new Vector2(280f,-140f),new Vector2(88f,88f),new Color(0.4f,0.4f,0.45f,1f),sprite,false);

        var scaler = canvas.GetComponent<CanvasScaler>();
        if(scaler!=null)
        {
            scaler.uiScaleMode=CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution=new Vector2(1920f,1080f);
        }

        EditorUtility.SetDirty(canvas);
        return "ZTween demo UI created";
    }

    static void RenameIfExists(string path,string name)
    {
        var go = GameObject.Find(path);
        if(go==null)return;
        Undo.RecordObject(go,"Rename "+name);
        go.name=name;
    }

    static RectTransform Image(string name,Transform parent,Vector2 pos,Vector2 size,Color color,Sprite sprite,bool addGroup)
    {
        var t = parent.Find(name);
        GameObject go;
        if(t==null)
        {
            go=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image));
            Undo.RegisterCreatedObjectUndo(go,"Create "+name);
            go.transform.SetParent(parent,false);
        }
        else go=t.gameObject;

        var rt=(RectTransform)go.transform;
        rt.anchorMin=rt.anchorMax=rt.pivot=new Vector2(0.5f,0.5f);
        rt.anchoredPosition=pos;
        rt.sizeDelta=size;
        var image=go.GetComponent<Image>();
        image.sprite=sprite;
        image.color=color;
        image.raycastTarget=false;
        if(addGroup&&go.GetComponent<CanvasGroup>()==null)Undo.AddComponent<CanvasGroup>(go);
        return rt;
    }

    static void Text(string name,Transform parent,Vector2 pos,Vector2 size,int fontSize,Font font,string content,Color color)
    {
        var t = parent.Find(name);
        GameObject go;
        if(t==null)
        {
            go=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Text));
            Undo.RegisterCreatedObjectUndo(go,"Create "+name);
            go.transform.SetParent(parent,false);
        }
        else go=t.gameObject;

        var rt=(RectTransform)go.transform;
        rt.anchorMin=rt.anchorMax=rt.pivot=new Vector2(0.5f,0.5f);
        rt.anchoredPosition=pos;
        rt.sizeDelta=size;
        var text=go.GetComponent<Text>();
        text.font=font;
        text.fontSize=fontSize;
        text.alignment=TextAnchor.MiddleCenter;
        text.color=color;
        text.text=content;
        text.raycastTarget=false;
    }
}
