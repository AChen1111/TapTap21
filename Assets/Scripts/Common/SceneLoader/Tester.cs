using System;
using System.Text;
using AChen.Log;
using Cysharp.Threading.Tasks;
using GamePlay.Gravity;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZZ.SceneLoader;

public class Tester : MonoBehaviour
{
    public string SceneName="TestScene";
    [Button]
    void FlipG()
    {
        GravityService.Instance.Flip();
    }
    [Button("LoadScene")]
    void LoadScene(){
        SceneLoader.LoadScene(SceneName);
    }
    [Button("LoadSceneWithPic")]
    void LoadSceneWithPic(){
        SceneLoader.LoadSceneWithPic(SceneName);
    }

    [Button(nameof(RegistPersistentScene))]
    void RegistPersistentScene(){
        SceneLoader.RegisterPersistentScene(SceneName);
    }
    
    [Button(nameof(UnRegistPersistentScene))]
    void UnRegistPersistentScene(){
        SceneLoader.UnregisterPersistentScene(SceneName);
    }
    [Button(nameof(IsPersistentScene))]
    bool IsPersistentScene(){
        var b=SceneLoader.IsPersistentScene(SceneName);
        ALog.LogWarning(b.ToString());
        return b;
    }
}
