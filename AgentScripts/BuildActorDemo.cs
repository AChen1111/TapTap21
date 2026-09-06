using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class BuildActorDemo
{
    public static string Main()
    {
        var canvas = GameObject.Find("Canvas");
        if(canvas!=null)
        {
            Undo.RecordObject(canvas,"Hide UI canvas");
            canvas.SetActive(false);
        }

        var uiHost = GameObject.Find("ZTweenDemo");
        if(uiHost!=null)
        {
            Undo.RecordObject(uiHost,"Disable UI test");
            uiHost.SetActive(false);
        }

        var sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        Actor("ActorDemo",new Vector3(0f,0f,0f),new Vector3(1.4f,1.4f,1f),new Color(0.35f,0.65f,1f,1f),sprite,0);
        Actor("ActorCoin",new Vector3(2.4f,0.35f,0f),new Vector3(0.55f,0.55f,1f),new Color(1f,0.82f,0.2f,1f),sprite,1);
        Actor("ActorTarget",new Vector3(2.4f,-1.15f,0f),new Vector3(2.2f,0.18f,1f),new Color(0.25f,0.28f,0.34f,0.9f),sprite,-1);

        var host = GameObject.Find("ActorDemoHost");
        if(host==null)
        {
            host=new GameObject("ActorDemoHost");
            Undo.RegisterCreatedObjectUndo(host,"Create ActorDemoHost");
        }

        return "Actor demo scene ready";
    }

    static void Actor(string name,Vector3 pos,Vector3 scale,Color color,Sprite sprite,int order)
    {
        var go = GameObject.Find(name);
        if(go==null)
        {
            go=new GameObject(name);
            Undo.RegisterCreatedObjectUndo(go,"Create "+name);
        }
        go.transform.position=pos;
        go.transform.localScale=scale;
        var sr=go.GetComponent<SpriteRenderer>();
        if(sr==null)sr=Undo.AddComponent<SpriteRenderer>(go);
        sr.sprite=sprite;
        sr.color=color;
        sr.sortingOrder=order;
    }
}
