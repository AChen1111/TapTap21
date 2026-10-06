using System.Runtime.CompilerServices;
using AChen.Events;
using AChen.Log;
using UI.GamePlay.HUD;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject _dragObj;
    private SlotController _controller;
    private ItemAttachRuler _ruler;
    void Awake()
    {
        _controller=GetComponent<SlotController>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragObj=ConstructDragObj(_controller.GetItemTemplate());
        EventCenter.Dispatch(GameEvent.SlotBeginDragged,_dragObj);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDraggedObjPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EventCenter.Dispatch(GameEvent.SlotEndDragged,_dragObj);
    }

    public void SetAttachRule(ItemAttachRuler ruler)
    {
        _ruler=ruler;
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
