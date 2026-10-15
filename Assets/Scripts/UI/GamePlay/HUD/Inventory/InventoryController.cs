using System;
using AChen.Events;
using AChen.Log;
using GamePlay.Core;
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
            EventCenter.AddListener(GameEvent.OnRecycleItem,OnRecycleItem);
        }
        void OnDisable()
        {
            EventCenter.RemoveListener(GameEvent.ModelBinded,BindModel);
            EventCenter.RemoveListener(GameEvent.OnRecycleItem,OnRecycleItem);
            if(_model!=null) _model.OnInventoryUpdated-=UpdateView; 
        }

        public void BindModel(InventoryModel model)
        {
            if (_model != null)
            {
                _model.OnInventoryUpdated-=UpdateView;
            }

            _model=model;
            model.OnInventoryUpdated+=UpdateView;
            if(_autoInit)_viewer.InitSlots(_model.Size,_slot);
            UpdateView(model);
        }
        public void SetInventory(InventoryModel model)
        {
            BindModel(model);
        }
        void UpdateView(InventoryModel model)
        {
            _viewer.UpdateSlotDisplay(model);
        }
        void OnRecycleItem(Item item)
        {
            int firstNull=-1;
            for(int i = 0; i < _model.Size; ++i)
            {
                var nowItemStack=_model.Items[i];
                if (nowItemStack == null&&firstNull==-1)
                {
                    firstNull=i;
                }

                else if (nowItemStack!=null&&nowItemStack.Item.GetType() == item.GetType()&&nowItemStack.Item.State==item.State)
                {
                    _model.PutItem(item,i);
                    goto end;
                }
            }
            if (firstNull == -1)
            {
                ALog.LogError("物品栏已满,无法放置新物品！");
                return;
            }
            _model.PutItem(item,firstNull);
            end:
            return;
        }

    }

}
