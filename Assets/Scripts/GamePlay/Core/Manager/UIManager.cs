using System;
using AChen.Events;
using AChen.Log;
using AChen.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameObject _menuUI;
    [SerializeField]private GameObject _menuPrefab;
    private GameObject menu;
    void Awake()
    {
        _menuUI=GameObject.Find("menuUI");
        if (_menuPrefab == null)
        {
            ALog.LogError("没有给 _menuPrefab 赋值!");
        }
    }
    void OnEnable()
    {
        EventCenter.AddListener(GameEvent.GameStarted,OnGameStarted);
    }
    void OnDisable()
    {
        EventCenter.RemoveListener(GameEvent.GameStarted,OnGameStarted);
    }

    private void OnGameStarted()
    {
        menu=Instantiate(_menuPrefab,_menuUI.transform);
        
    }
}
