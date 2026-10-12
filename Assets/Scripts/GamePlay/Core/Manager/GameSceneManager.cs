using System;
using AChen.Events;
using AChen.Log;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using ZZ.SceneLoader;
public class GameSceneManager : MonoBehaviour
{
    private static readonly string _persistentSceneName="PersistentScene";
    private static readonly string _gameSceneName="GameScene";
    void Awake()
    {
        
    }
    void OnEnable()
    {
        EventCenter.AddListener(GameEvent.GameStarted,OnGameStarted);
        EventCenter.AddListener(GameEvent.OnGameStartedBtnClicked,OnGameStartBtnClicked);
    }
    void OnDisable()
    {
        EventCenter.RemoveListener(GameEvent.GameStarted,OnGameStarted);
        EventCenter.RemoveListener(GameEvent.OnGameStartedBtnClicked,OnGameStartBtnClicked);
    }
    void OnGameStarted()
    {
        SceneLoader.RegisterPersistentScene(_persistentSceneName);
    }
    void OnGameStartBtnClicked()
    {
        SceneLoader.LoadScene(_gameSceneName);
    }
}
