using UnityEngine;
using DG.Tweening;
using System;
namespace ZZ.ZTween
{
    public static partial class ZTweenUtility
    {
        public static ZTween ZMoveTo(this Transform transform,Vector3 endValue,float time,Ease ease=Ease.OutCubic,Action onCompleteCallBack=null)
        {
            var tween=transform.DOMove(endValue,time).SetEase(ease).SetLink(transform.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZLocalMove(this Transform transform,Vector3 endValue,float time,Ease ease=Ease.OutCubic,Action onCompleteCallBack=null)
        {
            var tween=transform.DOLocalMove(endValue,time).SetEase(ease).SetLink(transform.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZJump(this Transform transform,float height=1.5f,float time=0.4f,Action onCompleteCallBack=null)
        {
            var tween=transform.DOJump(transform.position,height,1,time).SetEase(Ease.OutQuad).SetLink(transform.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZJumpTo(this Transform transform,Vector3 endValue,float height=1.5f,float time=0.45f,Action onCompleteCallBack=null)
        {
            var tween=transform.DOJump(endValue,height,1,time).SetEase(Ease.OutQuad).SetLink(transform.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZPunchScale(this Transform transform,float time=0.2f,float punch=0.2f,Action onCompleteCallBack=null)
        {
            var tween=transform.DOPunchScale(Vector3.one*punch,time,8,0.6f).SetLink(transform.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZPunchPosition(this Transform transform,float time=0.2f,Vector3 punch=default,Action onCompleteCallBack=null)
        {
            if(punch==default)punch=new Vector3(0.2f,0f,0f);
            var tween=transform.DOPunchPosition(punch,time,8,0.6f).SetLink(transform.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZPunchRotation(this Transform transform,float time=0.2f,float punch=18f,Action onCompleteCallBack=null)
        {
            var tween=transform.DOPunchRotation(new Vector3(0f,0f,punch),time,8,0.6f).SetLink(transform.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZShake(this Transform transform,float time=0.18f,float strength=0.18f,Action onCompleteCallBack=null)
        {
            var tween=transform.DOShakePosition(time,strength,18,90,false,true).SetLink(transform.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZPop(this Transform transform,float time=0.25f,Action onCompleteCallBack=null)
        {
            var origin=transform.localScale;
            var from=new Vector3(0.01f*SignX(origin),0.01f,origin.z);
            var sq=DOTween.Sequence().SetLink(transform.gameObject);
            sq.Append(transform.DOScale(ScaleAbs(origin,1.15f,1.15f),time*0.6f).From(from).SetEase(Ease.OutCubic));
            sq.Append(transform.DOScale(origin,time*0.4f).SetEase(Ease.OutQuart));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZSquash(this Transform transform,float time=0.22f,Action onCompleteCallBack=null)
        {
            var origin=transform.localScale;
            var sq=DOTween.Sequence().SetLink(transform.gameObject);
            sq.Append(transform.DOScale(ScaleAbs(origin,1.25f,0.65f),time*0.35f).SetEase(Ease.OutCubic));
            sq.Append(transform.DOScale(ScaleAbs(origin,0.9f,1.12f),time*0.35f).SetEase(Ease.OutQuad));
            sq.Append(transform.DOScale(origin,time*0.3f).SetEase(Ease.OutQuart));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
    }
}
