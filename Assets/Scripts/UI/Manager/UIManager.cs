using System;
using System.Collections.Generic;
using AChen.Events;
using AChen.Log;
using AChen.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI.Manager
{
    public class UIManager : MonoBehaviour
    {
        private GameObject _menuUI;
        public EUIMode Mode{get;private set;}
        [SerializeField]private GameObject _menuPrefab;
        [SerializeField]private GameObject _gamePlayUIPrefab;
        private GameObject _gameplayUI;
        private GameObject _menu;
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
            EventCenter.AddListener(GameEvent.OnGameSceneLoaded,InstantiateGamePlayUI);
        }
        void OnDisable()
        {
            EventCenter.RemoveListener(GameEvent.GameStarted,OnGameStarted);
            EventCenter.RemoveListener(GameEvent.OnGameSceneLoaded,InstantiateGamePlayUI);
        }

        private void OnGameStarted()
        {
            _menu=Instantiate(_menuPrefab,_menuUI.transform);
            Mode=EUIMode.Menu;
        }
        private void InstantiateGamePlayUI()
        {
            _menuUI.SetActive(false);
            Mode=EUIMode.Game;
            var uIParent=GameObject.Find("UIParent");
            _gameplayUI=Instantiate(_gamePlayUIPrefab,uIParent.transform);
        }
        
    }
}