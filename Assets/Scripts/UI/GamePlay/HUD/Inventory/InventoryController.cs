using System;
using AChen.Events;
using AChen.UI;
using GamePlay.Inventory;
using UnityEngine;
using UnityEngine.UI;
namespace UI.GamePlay.HUD
{
    public class InventoryController: MonoBehaviour
    {
        
        [SerializeField]private GameObject _slot;
        [SerializeField]private bool _autoInit=true;
        private InventoryModel _model=null;
        private InventoryViewer _viewer;
        void Awake()
        {
            _viewer=GetComponent<InventoryViewer>();
        }
        void OnEnable()
        {
            EventCenter.AddListener(GameEvent.ModelBinded,BindModel);
        }
        void OnDisable()
        {
            EventCenter.RemoveListener(GameEvent.ModelBinded,BindModel);
            if(_model!=null) _model.OnInventoryUpdated-=UpdateView; 
        }

        public void BindModel(InventoryModel model)
        {
            _model=model;
            model.OnInventoryUpdated+=UpdateView;
            if(_autoInit)_viewer.InitSlots(_model.Size,_slot);
        }
        public void SetInventory(InventoryModel model)
        {
            _model=model;
        }
        void UpdateView(InventoryModel model)
        {
            _viewer.UpdateSlotDisplay(model);
        }

    }

}
