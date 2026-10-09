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
    }
    void OnDisable()
    {
        EventCenter.RemoveListener(GameEvent.BindCamera2Transform,Bind2Transform);
    }
    [Button]
    void DebugInvokeBindEvent(Transform tf)=>EventCenter.Dispatch(GameEvent.BindCamera2Transform,tf);
    void Bind2Transform(Transform tf)
    {
        _camera.Follow=tf;
    }
}
