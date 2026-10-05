using GamePlay.Core;
using GamePlay.Inventory;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
namespace UI.GamePlay.HUD
{
    public class SlotController:MonoBehaviour
    {
        [SerializeField]private GameObject _frame;
        [SerializeField]private GameObject _item;
        [SerializeField]private TMP_Text txt_Count;
        [SerializeField,Sirenix.OdinInspector.ReadOnly]private int _count;
        public ItemStack itemStack{get;private set;}
        private Sprite2ItemTable _table;
        /// <summary>
        /// 此格子上有物品
        /// </summary>
        public bool IsDisplaying{get;private set;}

        private Image Img_Frame;
        private Image Img_Item;

        void Awake()
        {
            Img_Frame=_frame.GetComponent<Image>();
            Img_Item=_item.GetComponent<Image>();
            _frame.SetActive(true);
            UnDisplayItem();
            SetTextCount(-1);
        }

        /// <summary>
        /// 根据传入的信息，更新格子
        /// </summary>
        public void UpdateInfo(ItemStack itemStack)
        {
            if (itemStack == null)
            {
                UnDisplayItem();
                SetTextCount(-1);
            }
            else
            {
                DisplayItem();
                Img_Item.sprite=_table.GetSprite(itemStack.Item.GetType());
                SetTextCount(itemStack.Count);
            }
            this.itemStack= itemStack;

        } 
        public void SetTable(Sprite2ItemTable table)
        {
            _table=table;
        }
        void DisplayItem()
        {
            _item.SetActive(true);
            IsDisplaying=true;
        }


        void UnDisplayItem()
        {
            _item.SetActive(false);
            IsDisplaying=false;
        }

        void SetTextCount(int cnt)
        {
            if(cnt==-1){
                txt_Count.text="";
            }
            else
            {
                txt_Count.text=cnt.ToString();
            }
        }
        /// <summary>
        /// 返回Item对象,作为复制的模板
        /// </summary>
        /// <returns></returns>
        public GameObject GetItemTemplate()
        {
            return _item;
        }
    
    }
}
