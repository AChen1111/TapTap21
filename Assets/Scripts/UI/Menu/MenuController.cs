using System;
using AChen.Events;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]private Button _btn_StartGame;
    [SerializeField]private Button _btn_Settings;

    void Awake()
    {
        if(_btn_StartGame==null){
            Debug.LogError("没有给 _btn_StartGame 赋值!");
        }
        if(_btn_Settings==null){
            Debug.LogError("没有给 _btn_Settings 赋值!");
        }
    }

    void OnEnable()
    {
        _btn_StartGame.onClick.AddListener(OnBtnStartGameClick);
    }


    void OnDisable()
    {
        _btn_StartGame.onClick.RemoveListener(OnBtnStartGameClick);
    }
    void OnBtnStartGameClick()
    {
        EventCenter.Dispatch(GameEvent.OnGameStartedBtnClicked);
    }
}
