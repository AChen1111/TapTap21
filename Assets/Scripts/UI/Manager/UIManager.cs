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
        [SerializeField] private UISettings _gamePlaySettings;
        private global::GamePlay.Inventory.InventoryModel _inventory;
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
            if (_gameplayUI != null) return;
            if (_gamePlaySettings != null)
            {
                _gameplayUI = _gamePlaySettings.CreateUIInstance().gameObject;
                SceneManager.MoveGameObjectToScene(_gameplayUI, gameObject.scene);
                RegisterPausePanel();
                _inventory = new global::GamePlay.Inventory.InventoryModel(10);
                EventCenter.Dispatch(GameEvent.ModelBinded, _inventory);
                _inventory.PutItem(new global::GamePlay.Core.Item_Tree(), 2);
                _inventory.PutItem(new global::GamePlay.Core.Item_Water(), 1);
                return;
            }
            var uIParent=GameObject.Find("UIParent");
            _gameplayUI=Instantiate(_gamePlayUIPrefab,uIParent.transform);
            RegisterPausePanel();
        }

        private void RegisterPausePanel()
        {
            var frame = _gameplayUI.GetComponent<UIFrame>();
            if (frame == null) return;
            var root = new GameObject("GamePausePanel", typeof(RectTransform));
            root.transform.SetParent(frame.transform, false);
            var panel = root.AddComponent<GamePausePanel>();
            frame.RegisterScreen("GamePausePanel", panel, root.transform);
            frame.ShowPanel("GamePausePanel");
        }
        
    }
}
