using AChen.Events;
using Common.Singleton;
using Sirenix.OdinInspector;
using Unity.Cinemachine;
using UnityEngine;
using GamePlay.CameraSystem;
using GamePlay.Player.Movement;

[RequireComponent(typeof(CinemachineCamera), typeof(CinemachineConfiner2D))]
public class CameraController : Singleton<CameraController>
{
    private CinemachineCamera _camera;
    private CinemachineConfiner2D _confiner;
    [SerializeField] private Transform _followTarget;
    [SerializeField] private CameraRegion2D _defaultRegion;
    [SerializeField] private bool _autoFindPlayer = true;
    [SerializeField] private bool _autoSwitchRegions = true;
    private CameraRegion2D _activeRegion;
    private PolygonCollider2D _fallbackBoundary;
    private float _nextPlayerSearch;
    public Transform nowFollowedTransf => _camera != null ? _camera.Follow : _followTarget;
    public CameraRegion2D ActiveRegion => _activeRegion;
    void Awake()
    {
        _camera=GetComponent<CinemachineCamera>();
        _confiner = GetComponent<CinemachineConfiner2D>();
        _fallbackBoundary = _confiner.BoundingShape2D as PolygonCollider2D;
        if (_followTarget != null) Bind2Transform(_followTarget);
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

    private void Update()
    {
        if (_camera.Follow == null && _autoFindPlayer && Time.unscaledTime >= _nextPlayerSearch)
        {
            _nextPlayerSearch = Time.unscaledTime + 0.5f;
            PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
            if (player != null) Bind2Transform(player.transform);
        }
        if (_camera.Follow == null || !_autoSwitchRegions) return;
        CameraRegion2D selected = CameraRegion2D.FindRegion(_camera.Follow.position, _activeRegion);
        if (selected == null && _defaultRegion != null && _defaultRegion.isActiveAndEnabled)
            selected = _defaultRegion;
        // 过门间隙仍保持上一个有效范围，避免画面临时越界。
        if (selected != null && selected != _activeRegion)
        {
            _activeRegion = selected;
            ApplyBoundary(selected.Boundary);
        }
        else if (selected == null && _activeRegion != null && !_activeRegion.isActiveAndEnabled)
        {
            _activeRegion = null;
            ApplyBoundary(_fallbackBoundary);
        }
    }
    [Button]
    void DebugInvokeBindEvent(Transform tf)=>EventCenter.Dispatch(GameEvent.BindCamera2Transform,tf);
    [Button]
    void DebugInvokeSetBoundEvent(PolygonCollider2D coll)=>EventCenter.Dispatch(GameEvent.SetCameraBound,coll);
    public void Bind2Transform(Transform tf)
    {
        _followTarget = tf;
        _camera.Follow=tf;
    }

    public void SetBound(PolygonCollider2D coll)
    {
        _autoSwitchRegions = false;
        _activeRegion = null;
        ApplyBoundary(coll);
    }

    /// <summary>外部手动设置边界后，可调用此方法恢复按玩家位置自动切区。</summary>
    public void EnableAutomaticRegions() => _autoSwitchRegions = true;

    private void ApplyBoundary(PolygonCollider2D boundary)
    {
        if (_confiner.BoundingShape2D == boundary) return;
        _confiner.BoundingShape2D = boundary;
        _confiner.InvalidateBoundingShapeCache();
    }
}
