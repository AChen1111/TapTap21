using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
namespace ZZ.ZTween
{
    public static partial class ZTweenUtility
    {
        public static ZTween ZFadeIn(this CanvasGroup canvasGroup,float time,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=canvasGroup.DOFade(1f,time).From(0f).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZFadeIn(this Image canvasGroup,float time,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=canvasGroup.DOFade(1f,time).From(0f).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZPopup(this RectTransform rectTransform,float time=0.5f,float maxScale=1.2f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one*maxScale,time*0.66f).From(Vector3.zero).SetEase(Ease.OutCubic));
            sq.Append(rectTransform.DOScale(Vector3.one,time*0.34f).SetEase(Ease.OutQuart));
            if (onCompleteCallBack != null)
            {
                sq.AppendCallback(()=>onCompleteCallBack());
            }
            return new(sq);
        }
        
        
    }




    //声明周期管理
    public static partial class ZTweenUtility
    {
        public static void KillAll()
        {
            DOTween.KillAll();
        }
        public static void Kill(ZTween zTween)
        {
            zTween.tween.Kill();
        }
        public static void Pause(ZTween zTween)
        {
            zTween.tween.Pause();
        }
        public static void Play(ZTween zTween)
        {
            zTween.tween.Play();
        }
        public static void ReStart(ZTween zTween)
        {
           zTween.tween.Restart();
        }
        public static void Rewind(ZTween zTween)
        {
            zTween.tween.Rewind();
        }
    }
    
}

