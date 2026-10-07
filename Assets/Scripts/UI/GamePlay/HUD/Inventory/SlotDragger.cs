using AChen.Events;
using GamePlay.Inventory;
using UI.GamePlay.HUD;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;

public class SlotDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject _dragObj;
    private SlotController _controller;
    private ItemAttachRuler _ruler;
    private InventoryModel _inventory;
    private int _slotIndex = -1;

    void Awake()
    {
        _controller=GetComponent<SlotController>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_controller == null ||
            !_controller.IsDisplaying ||
            _controller.itemStack == null ||
            _controller.itemStack.Item == null ||
            _inventory == null ||
            _slotIndex < 0)
        {
            return;
        }

        _dragObj=ConstructDragObj(_controller.GetItemTemplate());
        EventCenter.Dispatch(GameEvent.SlotBeginDragged,_dragObj);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDraggedObjPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_dragObj == null)
        {
            return;
        }

        InventoryDragContext context = new(
            _inventory,
            _slotIndex,
            _controller.itemStack.Item,
            _dragObj,
            eventData.position
        );

        // 保留旧事件，兼容已有 UI 监听器；场景桥接器使用带完整数据的新事件。
        EventCenter.Dispatch(GameEvent.SlotEndDragged,_dragObj);
        EventCenter.Dispatch(GameEvent.InventoryDragEnded, context);

        Destroy(_dragObj);
        _dragObj = null;
    }

    public void SetAttachRule(ItemAttachRuler ruler)
    {
        _ruler=ruler;
    }

    public void SetInventorySource(InventoryModel inventory, int slotIndex)
    {
        _inventory = inventory;
        _slotIndex = slotIndex;
    }

    /// <summary>
    /// 实时更新位置
    /// </summary>
    void UpdateDraggedObjPosition(PointerEventData eventData)
    {
        if (_dragObj == null)
        {
            return;
        }
        if(_ruler.enableAttach)_dragObj.GetComponent<RectTransform>().position=GetAttachVar(eventData.position,_ruler);
        else _dragObj.GetComponent<RectTransform>().position=eventData.position;
    }

    GameObject ConstructDragObj(GameObject obj)
    {
        var ret=Instantiate(obj,transform);
        ret.SetActive(true);
        return ret;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Vector3 GetAttachVar(Vector2 pos, ItemAttachRuler ruler)
    {
        pos.x -= ruler.OffsetX;
        pos.y -= ruler.OffsetY;

        pos.x = Mathf.Round(pos.x / ruler.Width) * ruler.Width;
        pos.y = Mathf.Round(pos.y / ruler.Height) * ruler.Height;

        pos.x += ruler.OffsetX;
        pos.y += ruler.OffsetY;

        return pos;
    }
}
