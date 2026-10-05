using AChen.Log;
using UI.GamePlay.HUD;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject _dragObj;
    private SlotController _controller;
    void Awake()
    {
        _controller=GetComponent<SlotController>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragObj=ConstructDragObj(_controller.GetItemTemplate());
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDraggedObjPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(_dragObj);
        _dragObj=null;
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
        _dragObj.GetComponent<RectTransform>().position=eventData.position;
    }

    GameObject ConstructDragObj(GameObject obj)
    {
        var ret=Instantiate(obj,this.transform);
        Destroy(ret.GetComponent<SlotDragger>());
        return ret;
    }
}
