using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using AChen.Log;
using System;
using AChen.Events;
namespace ZZ.SceneLoader{
    
public static class SceneLoader
{

    static readonly string _selfName="[SceneLoader]";
    static readonly HashSet<string> s_PersistentScenes=new HashSet<string>();

    public static void LoadScene(string sceneName,LoadSceneMode mode=LoadSceneMode.Single,Action onSceneLoadCompleteCallback=null){
        LoadSceneAsync(sceneName,mode,onSceneLoadCompleteCallback).Forget();
    }
    public static void LoadSceneWithPic(string sceneName,float picShowTime=2f,LoadSceneMode mode=LoadSceneMode.Single,Action onSceneLoadCompleteCallback=null){
        LoadSceneWithPicAsync(sceneName,picShowTime,mode,onSceneLoadCompleteCallback).Forget();
    }

    public static void RegisterPersistentScene(string sceneName){
        RegisterPersistentSceneAsync(sceneName).Forget();
    }

    public static void UnregisterPersistentScene(string sceneName){
        if(string.IsNullOrEmpty(sceneName)){
            return;
        }
        s_PersistentScenes.Remove(sceneName);
    }

    public static bool IsPersistentScene(string sceneName){
        return !string.IsNullOrEmpty(sceneName)&&s_PersistentScenes.Contains(sceneName);
    }

    static async UniTask RegisterPersistentSceneAsync(string sceneName){
        if(string.IsNullOrEmpty(sceneName)){
            ALog.LogError($"{_selfName} 注册持久场景失败: 场景名为空");
            return;
        }

        s_PersistentScenes.Add(sceneName);
        if(IsSceneLoaded(sceneName)){
            return;
        }

        var asyncOp=SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        if(asyncOp==null){
            ALog.LogError($"{_selfName} 无法加载持久场景: {sceneName}");
            return;
        }
        await asyncOp;
    }

    static async UniTask LoadSceneAsync(string sceneName,LoadSceneMode mode=LoadSceneMode.Single,Action onSceneLoadCompleteCallback=null){
        var asyncOp=BeginLoad(sceneName,mode);
        if(asyncOp==null){
            if(mode==LoadSceneMode.Single&&IsSceneLoaded(sceneName)){
                await ActivateAndUnloadOthers(sceneName,mode);
                onSceneLoadCompleteCallback?.Invoke();
            }
            return;
        }

        await asyncOp;
        await ActivateAndUnloadOthers(sceneName,mode);
        onSceneLoadCompleteCallback?.Invoke();
    }
    static async UniTask LoadSceneWithPicAsync(string sceneName,float picShowTime,LoadSceneMode mode=LoadSceneMode.Single,Action onSceneLoadCompleteCallback=null){
        picShowTime=Mathf.Max(0f,picShowTime);
        var asyncOp=BeginLoad(sceneName,mode);
        if(asyncOp==null){
            if(mode==LoadSceneMode.Single&&IsSceneLoaded(sceneName)){
                await ShowPic(picShowTime);
                await ActivateAndUnloadOthers(sceneName,mode);
                onSceneLoadCompleteCallback?.Invoke();
            }
            return;
        }

        asyncOp.allowSceneActivation=false;
        await UniTask.WhenAll(
            ShowPic(picShowTime),
            UniTask.WaitUntil(()=>asyncOp.progress>=0.9f)
        );
        asyncOp.allowSceneActivation=true;
        await asyncOp;
        await ActivateAndUnloadOthers(sceneName,mode);
        onSceneLoadCompleteCallback?.Invoke();
    }

    static AsyncOperation BeginLoad(string sceneName,LoadSceneMode mode){
        if(mode==LoadSceneMode.Single&&IsSceneLoaded(sceneName)){
            return null;
        }

        var loadMode=mode==LoadSceneMode.Single?LoadSceneMode.Additive:mode;
        var asyncOp=SceneManager.LoadSceneAsync(sceneName,loadMode);
        if(asyncOp==null){
            ALog.LogError($"{_selfName} 无法开始加载场景: {sceneName}");
        }
        return asyncOp;
    }

    static async UniTask ActivateAndUnloadOthers(string sceneName,LoadSceneMode requestedMode){
        if(requestedMode!=LoadSceneMode.Single){
            return;
        }

        var scene=SceneManager.GetSceneByName(sceneName);
        if(scene.IsValid()&&scene.isLoaded){
            SceneManager.SetActiveScene(scene);
        }

        var toUnload=new List<Scene>();
        for(int i=0;i<SceneManager.sceneCount;i++){
            var loaded=SceneManager.GetSceneAt(i);
            if(!loaded.IsValid()||!loaded.isLoaded){
                continue;
            }
            if(loaded.name==sceneName||s_PersistentScenes.Contains(loaded.name)){
                continue;
            }
            toUnload.Add(loaded);
        }

        foreach(var loaded in toUnload){
            var unloadOp=SceneManager.UnloadSceneAsync(loaded);
            if(unloadOp!=null){
                await unloadOp;
            }
        }
    }

    static bool IsSceneLoaded(string sceneName){
        var scene=SceneManager.GetSceneByName(sceneName);
        return scene.IsValid()&&scene.isLoaded;
    }
    static async UniTask ShowPic(float time){
        EventCenter.Dispatch(GameEvent.StartShowShenTu,time);
        await UniTask.WaitForSeconds(time);
    }

}

}
