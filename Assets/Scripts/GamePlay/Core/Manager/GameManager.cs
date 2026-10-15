using AChen.Events;
using AChen.Log;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        OnGameStart();
    }

    void OnEnable()
    {
        EventCenter.AddListener(GameEvent.OnGameExitBtnClicked,OnGameExit);
    }
    void OnDisable()
    {
        EventCenter.RemoveListener(GameEvent.OnGameExitBtnClicked,OnGameExit);
    }


    [Button]
    void OnGameStart()
    {
        EventCenter.Dispatch(GameEvent.GameStarted);
    }

    void OnGameExit()
    {
        Application.Quit();
        ALog.Log("游戏退出!");
    }
}
