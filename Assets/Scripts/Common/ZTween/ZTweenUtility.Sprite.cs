using UnityEngine;
using DG.Tweening;
using System;
namespace ZZ.ZTween
{
    public static partial class ZTweenUtility
    {
        public static ZTween ZFadeIn(this SpriteRenderer sprite,float time=0.2f,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=sprite.DOFade(1f,time).From(0f).SetEase(ease).SetLink(sprite.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZFadeOut(this SpriteRenderer sprite,float time=0.2f,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=sprite.DOFade(0f,time).SetEase(ease).SetLink(sprite.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZFade(this SpriteRenderer sprite,float endValue,float time=0.2f,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=sprite.DOFade(endValue,time).SetEase(ease).SetLink(sprite.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZColor(this SpriteRenderer sprite,Color endValue,float time=0.15f,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=sprite.DOColor(endValue,time).SetEase(ease).SetLink(sprite.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZTint(this SpriteRenderer sprite,Color endValue,float time=0.15f,Action onCompleteCallBack=null)
        {
            return sprite.ZColor(endValue,time,Ease.Linear,onCompleteCallBack);
        }
        public static ZTween ZFlash(this SpriteRenderer sprite,Color flashColor,float time=0.12f,Action onCompleteCallBack=null)
        {
            var origin=sprite.color;
            var sq=DOTween.Sequence().SetLink(sprite.gameObject);
            sq.Append(sprite.DOColor(flashColor,time*0.4f));
            sq.Append(sprite.DOColor(origin,time*0.6f));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZBlink(this SpriteRenderer sprite,float time=0.12f,int loops=-1,Action onCompleteCallBack=null)
        {
            var tween=sprite.DOFade(0.2f,time).SetEase(Ease.InOutSine).SetLoops(loops,LoopType.Yoyo).SetLink(sprite.gameObject);
            if(loops>0&&onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZFlipX(this Transform transform,float time=0.12f,Action onCompleteCallBack=null)
        {
            var origin=transform.localScale;
            var mid=new Vector3(0.08f*SignX(origin),origin.y,origin.z);
            var end=new Vector3(-origin.x,origin.y,origin.z);
            var sq=DOTween.Sequence().SetLink(transform.gameObject);
            sq.Append(transform.DOScale(mid,time*0.45f).SetEase(Ease.InCubic));
            sq.Append(transform.DOScale(end,time*0.55f).SetEase(Ease.OutCubic));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
    }
}
