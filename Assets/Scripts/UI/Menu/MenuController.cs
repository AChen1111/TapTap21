using System;
using AChen.Events;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]private Button _btn_StartGame;
    [SerializeField]private Button _btn_Settings;
    [SerializeField]private Button _btn_ExitGame;

    void Awake()
    {
        if(_btn_StartGame==null){
            Debug.LogError("没有给 _btn_StartGame 赋值!");
        }
        if(_btn_Settings==null){
            Debug.LogError("没有给 _btn_Settings 赋值!");
        }
        if(_btn_ExitGame==null){
            Debug.LogError("没有给 _btn_ExitGame赋值!");
        }
    }

    void OnEnable()
    {
        _btn_StartGame.onClick.AddListener(OnBtnStartGameClick);
        _btn_ExitGame.onClick.AddListener(OnBtnExitGameClick);
    }


    void OnDisable()
    {
        _btn_StartGame.onClick.RemoveListener(OnBtnStartGameClick);
        _btn_ExitGame.onClick.RemoveListener(OnBtnExitGameClick);
    }

    private void OnBtnExitGameClick()
    {
        EventCenter.Dispatch(GameEvent.OnGameExitBtnClicked);
    }

    void OnBtnStartGameClick()
    {
        EventCenter.Dispatch(GameEvent.OnGameStartedBtnClicked);
    }
}
