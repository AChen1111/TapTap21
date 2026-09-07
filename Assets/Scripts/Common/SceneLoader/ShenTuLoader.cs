using AChen.Events;
using DG.Tweening;
using UnityEngine;
using ZZ.ZTween;
/// <summary>
/// 用于在场景加载的时候显示一张神图
/// </summary>
public class ShenTuLoader : MonoBehaviour
{
    private CanvasGroup _cg;
    void Awake(){
        _cg=GetComponent<CanvasGroup>();
        _cg.alpha=0f;
    }
    void OnEnable(){
        EventCenter.AddListener(GameEvent.StartShowShenTu,StartShowShenTu);
    }
    void OnDisable(){
        EventCenter.RemoveListener(GameEvent.StartShowShenTu,StartShowShenTu);
    }

    void StartShowShenTu(float time){
        ZTweenUtility.ZSequence().ZAppend(_cg.ZFadeIn(time/2,Ease.InQuart)).ZAppend(_cg.ZFadeOut(time/2));
    }
}
