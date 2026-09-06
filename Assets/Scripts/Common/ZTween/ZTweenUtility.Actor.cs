using UnityEngine;
using DG.Tweening;
using System;
namespace ZZ.ZTween
{
    public static partial class ZTweenUtility
    {
        public static ZTween ZSpawn(this Transform transform,float time=0.25f,Action onCompleteCallBack=null)
        {
            transform.gameObject.SetActive(true);
            var origin=transform.localScale;
            var from=new Vector3(0.01f*SignX(origin),0.01f,origin.z);
            var sprite=SpriteOf(transform);
            var sq=DOTween.Sequence().SetLink(transform.gameObject);
            sq.Append(transform.DOScale(ScaleAbs(origin,1.15f,1.15f),time*0.6f).From(from).SetEase(Ease.OutCubic));
            sq.Append(transform.DOScale(origin,time*0.4f).SetEase(Ease.OutQuart));
            if(sprite!=null)sq.Insert(0,sprite.DOFade(1f,time).From(0f));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZDespawn(this Transform transform,float time=0.2f,Action onCompleteCallBack=null)
        {
            var origin=transform.localScale;
            var to=new Vector3(0.01f*SignX(origin),0.01f,origin.z);
            var sprite=SpriteOf(transform);
            var sq=DOTween.Sequence().SetLink(transform.gameObject);
            sq.Append(transform.DOScale(to,time).SetEase(Ease.InCubic));
            if(sprite!=null)sq.Join(sprite.DOFade(0f,time));
            sq.OnComplete(()=>
            {
                transform.gameObject.SetActive(false);
                transform.localScale=origin;
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return new(sq);
        }

        public static ZTween ZLand(this Transform transform,float time=0.22f,Action onCompleteCallBack=null)
        {
            return transform.ZSquash(time,onCompleteCallBack);
        }
        public static ZTween ZHit(this Transform transform,Vector3 hitDir=default,float time=0.16f,Action onCompleteCallBack=null)
        {
            if(hitDir==default)hitDir=Vector3.left;
            hitDir.z=0f;
            if(hitDir.sqrMagnitude<0.0001f)hitDir=Vector3.left;
            hitDir.Normalize();
            var sprite=SpriteOf(transform);
            var sq=DOTween.Sequence().SetLink(transform.gameObject);
            sq.Append(transform.DOPunchPosition(-hitDir*0.22f,time,6,0.45f));
            sq.Join(transform.DOShakePosition(time,0.08f,16,90,false,true));
            if(sprite!=null)
            {
                var origin=sprite.color;
                sq.Insert(0,sprite.DOColor(Color.white,time*0.35f));
                sq.Insert(time*0.35f,sprite.DOColor(origin,time*0.65f));
            }
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZDash(this Transform transform,Vector3 endValue,float time=0.18f,Action onCompleteCallBack=null)
        {
            var origin=transform.localScale;
            var dir=endValue-transform.position;
            dir.z=0f;
            var stretch=dir.sqrMagnitude>0.0001f&&Mathf.Abs(dir.x)>=Mathf.Abs(dir.y)
                ?ScaleAbs(origin,1.35f,0.8f)
                :ScaleAbs(origin,0.8f,1.25f);
            var sq=DOTween.Sequence().SetLink(transform.gameObject);
            sq.Append(transform.DOMove(endValue,time).SetEase(Ease.OutCubic));
            sq.Join(transform.DOScale(stretch,time*0.35f).SetEase(Ease.OutQuad));
            sq.Insert(time*0.45f,transform.DOScale(origin,time*0.55f).SetEase(Ease.OutQuart));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZKnockback(this Transform transform,Vector3 hitDir=default,float distance=0.7f,float time=0.22f,Action onCompleteCallBack=null)
        {
            if(hitDir==default)hitDir=Vector3.left;
            hitDir.z=0f;
            if(hitDir.sqrMagnitude<0.0001f)hitDir=Vector3.left;
            hitDir.Normalize();
            var start=transform.position;
            var far=start+hitDir*distance;
            var settle=start+hitDir*distance*0.65f;
            var sq=DOTween.Sequence().SetLink(transform.gameObject);
            sq.Append(transform.DOMove(far,time*0.4f).SetEase(Ease.OutCubic));
            sq.Append(transform.DOMove(settle,time*0.6f).SetEase(Ease.OutQuad));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZCollect(this Transform transform,Vector3 target,float time=0.35f,Action onCompleteCallBack=null)
        {
            var origin=transform.localScale;
            var sprite=SpriteOf(transform);
            var sq=DOTween.Sequence().SetLink(transform.gameObject);
            sq.Append(transform.DOMove(target,time).SetEase(Ease.InCubic));
            sq.Join(transform.DOScale(origin*0.15f,time).SetEase(Ease.InQuad));
            if(sprite!=null)sq.Join(sprite.DOFade(0f,time));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZIdleBob(this Transform transform,float time=0.7f,float offset=0.12f,int loops=-1,Action onCompleteCallBack=null)
        {
            var tween=transform.DOLocalMoveY(transform.localPosition.y+offset,time).SetEase(Ease.InOutSine).SetLoops(loops,LoopType.Yoyo).SetLink(transform.gameObject);
            if(loops>0&&onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZAttackSwing(this Transform transform,float time=0.16f,float angle=22f,Action onCompleteCallBack=null)
        {
            var tween=transform.DOPunchRotation(new Vector3(0f,0f,angle),time,6,0.4f).SetLink(transform.gameObject);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZSpawn(this GameObject go,float time=0.25f,Action onCompleteCallBack=null)=>go.transform.ZSpawn(time,onCompleteCallBack);
        public static ZTween ZDespawn(this GameObject go,float time=0.2f,Action onCompleteCallBack=null)=>go.transform.ZDespawn(time,onCompleteCallBack);
        public static ZTween ZLand(this GameObject go,float time=0.22f,Action onCompleteCallBack=null)=>go.transform.ZLand(time,onCompleteCallBack);
        public static ZTween ZHit(this GameObject go,Vector3 hitDir=default,float time=0.16f,Action onCompleteCallBack=null)=>go.transform.ZHit(hitDir,time,onCompleteCallBack);
        public static ZTween ZDash(this GameObject go,Vector3 endValue,float time=0.18f,Action onCompleteCallBack=null)=>go.transform.ZDash(endValue,time,onCompleteCallBack);
        public static ZTween ZKnockback(this GameObject go,Vector3 hitDir=default,float distance=0.7f,float time=0.22f,Action onCompleteCallBack=null)=>go.transform.ZKnockback(hitDir,distance,time,onCompleteCallBack);
        public static ZTween ZCollect(this GameObject go,Vector3 target,float time=0.35f,Action onCompleteCallBack=null)=>go.transform.ZCollect(target,time,onCompleteCallBack);
        public static ZTween ZIdleBob(this GameObject go,float time=0.7f,float offset=0.12f,int loops=-1,Action onCompleteCallBack=null)=>go.transform.ZIdleBob(time,offset,loops,onCompleteCallBack);
        public static ZTween ZAttackSwing(this GameObject go,float time=0.16f,float angle=22f,Action onCompleteCallBack=null)=>go.transform.ZAttackSwing(time,angle,onCompleteCallBack);
    }

    public static partial class ZTweenUtility
    {
        static float SignX(Vector3 scale)
        {
            return scale.x<0f?-1f:1f;
        }
        static Vector3 ScaleAbs(Vector3 origin,float absXMul,float absYMul)
        {
            return new Vector3(Mathf.Abs(origin.x)*absXMul*SignX(origin),origin.y*absYMul,origin.z);
        }
        static SpriteRenderer SpriteOf(Transform transform)
        {
            var sprite=transform.GetComponent<SpriteRenderer>();
            if(sprite==null)sprite=transform.GetComponentInChildren<SpriteRenderer>();
            return sprite;
        }
    }
}
