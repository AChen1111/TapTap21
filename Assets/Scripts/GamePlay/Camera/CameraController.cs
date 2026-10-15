using AChen.Events;
using Common.Singleton;
using Sirenix.OdinInspector;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineCamera _camera;
    public Transform nowFollowedTransf=>_camera.Follow;
    void Awake()
    {
        _camera=GetComponent<CinemachineCamera>();
    }
    void OnEnable()
    {
        EventCenter.AddListener(GameEvent.BindCamera2Transform,Bind2Transform);
        EventCenter.AddListener(GameEvent.SetCameraBound,SetBound);
    }
    void OnDisable()
    {
        EventCenter.RemoveListener(GameEvent.BindCamera2Transform,Bind2Transform);
        EventCenter.RemoveListener(GameEvent.SetCameraBound,SetBound);
    }
    [Button]
    void DebugInvokeBindEvent(Transform tf)=>EventCenter.Dispatch(GameEvent.BindCamera2Transform,tf);
    [Button]
    void DebugInvokeSetBoundEvent(PolygonCollider2D coll)=>EventCenter.Dispatch(GameEvent.SetCameraBound,coll);
    void Bind2Transform(Transform tf)
    {
        _camera.Follow=tf;
    }

    void SetBound(PolygonCollider2D coll)
    {
        _camera.GetComponent<CinemachineConfiner2D>().BoundingShape2D=coll;
    }
}
