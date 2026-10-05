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
        EventCenter.Dispatch(GameEvent.SlotBeginDragged,_controller);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDraggedObjPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(_dragObj);
        _dragObj=null;
        EventCenter.Dispatch(GameEvent.SlotEndDragged,_controller);
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
        var worldPos=Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x,eventData.position.y,-Camera.main.transform.position.z));
        if(_ruler.enableAttach)_dragObj.transform.position=GetAttachVar(worldPos,_ruler);
        else _dragObj.transform.position=worldPos;
    }

    GameObject ConstructDragObj(GameObject obj)
    {
        var ret=new GameObject();
        var sr=ret.AddComponent<SpriteRenderer>();
        sr.sprite=obj.GetComponent<Image>().sprite;
        sr.transform.position=Vector3.zero;
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
