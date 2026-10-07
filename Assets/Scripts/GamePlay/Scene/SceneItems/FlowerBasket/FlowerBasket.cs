using System.Runtime.CompilerServices;
using AChen.Events;
using AChen.Log;
using GamePlay.Scene;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using ZZ.ZTween;
public class FlowerBasket : MonoBehaviour, ISceneItem
{
    enum MoveState
    {
        Stop,Moving
    }
    [SerializeField]private int _moveSpeed;                                     //移动速度

    public CablePoint NowPoint{get;set;}                                        //现在属于哪个点

    [ReadOnly,SerializeField]private MoveState _moveState=MoveState.Stop;       //移动状态
    private ZTween _nowTween;
    void OnEnable()
    {
        EventCenter.AddListener(GameEvent.CallFlowerBasket,CallSelfToCablePoint);
    }
    void OnDisable()
    {
        EventCenter.RemoveListener(GameEvent.CallFlowerBasket,CallSelfToCablePoint);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    float timeCalc(Vector3 tar)
    {
        return (tar-transform.position).magnitude/_moveSpeed;
    }

    /// <summary>
    /// 自己被召回
    /// </summary>
    /// <param name="pos"></param>
    void CallSelfToCablePoint(Vector3 pos)
    {
        if(_nowTween!=null&&_nowTween.active)
        ZTweenUtility.Kill(_nowTween);
        _nowTween=transform.ZMoveTo(pos,timeCalc(pos),ease:DG.Tweening.Ease.InOutQuad);
    }
}
