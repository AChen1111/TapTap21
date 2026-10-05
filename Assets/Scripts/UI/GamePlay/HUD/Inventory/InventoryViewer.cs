using System.Collections.Generic;
using GamePlay.Inventory;
using UnityEngine;
namespace UI.GamePlay.HUD
{
    public class InventoryViewer : MonoBehaviour
    {
        [SerializeField]private Transform _contentTf;
        [SerializeField]private Sprite2ItemTable _table;
        [SerializeField]private ItemAttachRuler _ruler;
        [SerializeField,Sirenix.OdinInspector.ReadOnly]private List<SlotController> _slots;
        public void InitSlots(int size,GameObject slot)
        {
            _slots=new();
            _table.UpdateDictInfo();
            for (int i=0;i<size;++i)
            {
                var slotCl=Instantiate(slot,_contentTf).GetComponent<SlotController>();
                slotCl.SetTable(_table);
                _slots.Add(slotCl);
                slotCl.GetComponent<SlotDragger>().SetAttachRule(_ruler);
            }
        }
        /// <summary>
        /// 更新每个格子的显示
        /// </summary>
        public void UpdateSlotDisplay(InventoryModel model)
        {
            for(int i = 0; i < model.Size; ++i)
            {
                _slots[i].UpdateInfo(model[i]);
            }
        }
    }

}
