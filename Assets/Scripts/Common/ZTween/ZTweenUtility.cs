using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
namespace ZZ.ZTween
{
    public enum ZDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public enum ZShowType
    {
        Fade,
        Popup,
        Slide
    }

    public enum ZButtonState
    {
        Normal,
        Hover,
        Pressed,
        Selected,
        Disabled
    }

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
        public static ZTween ZFadeIn(this Text text,float time,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=text.DOFade(1f,time).From(0f).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZFadeOut(this CanvasGroup canvasGroup,float time,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=canvasGroup.DOFade(0f,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZFadeOut(this Image image,float time,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=image.DOFade(0f,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZFadeOut(this Text text,float time,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=text.DOFade(0f,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZFade(this CanvasGroup canvasGroup,float endValue,float time,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=canvasGroup.DOFade(endValue,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZFade(this Image image,float endValue,float time,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=image.DOFade(endValue,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZFade(this Text text,float endValue,float time,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=text.DOFade(endValue,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZScale(this RectTransform rectTransform,float endValue,float time,Ease ease=Ease.OutCubic,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DOScale(Vector3.one*endValue,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZMove(this RectTransform rectTransform,Vector2 endValue,float time,Ease ease=Ease.OutCubic,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DOAnchorPos(endValue,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZRotate(this RectTransform rectTransform,float z,float time,Ease ease=Ease.OutCubic,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DORotate(new Vector3(0f,0f,z),time).SetEase(ease);
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

        public static ZTween ZPopupFade(this RectTransform rectTransform,float time=0.5f,float maxScale=1.2f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one*maxScale,time*0.66f).From(Vector3.zero).SetEase(Ease.OutCubic));
            sq.Append(rectTransform.DOScale(Vector3.one,time*0.34f).SetEase(Ease.OutQuart));
            var canvasGroup=rectTransform.GetComponent<CanvasGroup>();
            if(canvasGroup!=null)
            {
                sq.Insert(0,canvasGroup.DOFade(1f,time).From(0f));
            }
            else
            {
                var graphic=rectTransform.GetComponent<Graphic>();
                if(graphic!=null)sq.Insert(0,graphic.DOFade(1f,time).From(0f));
            }
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }

        public static ZTween ZClose(this RectTransform rectTransform,float time=0.4f,float minScale=0.9f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one*minScale,time*0.34f).SetEase(Ease.InQuart));
            sq.Append(rectTransform.DOScale(Vector3.zero,time*0.66f).SetEase(Ease.InCubic));
            var canvasGroup=rectTransform.GetComponent<CanvasGroup>();
            if(canvasGroup!=null)
            {
                sq.Insert(0,canvasGroup.DOFade(0f,time));
            }
            else
            {
                var graphic=rectTransform.GetComponent<Graphic>();
                if(graphic!=null)sq.Insert(0,graphic.DOFade(0f,time));
            }
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }

        public static ZTween ZSlideIn(this RectTransform rectTransform,ZDirection direction=ZDirection.Left,float time=0.35f,Ease ease=Ease.OutCubic,Action onCompleteCallBack=null)
        {
            var offset=SlideOffset(rectTransform,direction);
            var tween=rectTransform.DOAnchorPos(rectTransform.anchoredPosition,time).From(rectTransform.anchoredPosition+offset).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZSlideOut(this RectTransform rectTransform,ZDirection direction=ZDirection.Right,float time=0.35f,Ease ease=Ease.InCubic,Action onCompleteCallBack=null)
        {
            var offset=SlideOffset(rectTransform,direction);
            var tween=rectTransform.DOAnchorPos(rectTransform.anchoredPosition+offset,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZShow(this CanvasGroup canvasGroup,float time=0.3f,ZShowType showType=ZShowType.Fade,ZDirection direction=ZDirection.Up,Action onCompleteCallBack=null)
        {
            canvasGroup.gameObject.SetActive(true);
            SetRaycast(canvasGroup,false);
            var rectTransform=canvasGroup.transform as RectTransform;
            var sq=DOTween.Sequence();
            switch(showType)
            {
                case ZShowType.Popup:
                    if(rectTransform!=null)
                    {
                        sq.Append(rectTransform.DOScale(Vector3.one*1.2f,time*0.66f).From(Vector3.zero).SetEase(Ease.OutCubic));
                        sq.Append(rectTransform.DOScale(Vector3.one,time*0.34f).SetEase(Ease.OutQuart));
                    }
                    sq.Insert(0,canvasGroup.DOFade(1f,time).From(0f));
                    break;
                case ZShowType.Slide:
                    if(rectTransform!=null)
                    {
                        var offset=SlideOffset(rectTransform,direction);
                        sq.Append(rectTransform.DOAnchorPos(rectTransform.anchoredPosition,time).From(rectTransform.anchoredPosition+offset).SetEase(Ease.OutCubic));
                    }
                    sq.Insert(0,canvasGroup.DOFade(1f,time).From(0f));
                    break;
                default:
                    sq.Append(canvasGroup.DOFade(1f,time).From(0f));
                    break;
            }
            sq.OnComplete(()=>
            {
                SetRaycast(canvasGroup,true);
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return new(sq);
        }

        public static ZTween ZHide(this CanvasGroup canvasGroup,float time=0.3f,ZShowType showType=ZShowType.Fade,ZDirection direction=ZDirection.Down,Action onCompleteCallBack=null)
        {
            SetRaycast(canvasGroup,false);
            var rectTransform=canvasGroup.transform as RectTransform;
            var sq=DOTween.Sequence();
            switch(showType)
            {
                case ZShowType.Popup:
                    if(rectTransform!=null)
                    {
                        sq.Append(rectTransform.DOScale(Vector3.one*0.9f,time*0.34f).SetEase(Ease.InQuart));
                        sq.Append(rectTransform.DOScale(Vector3.zero,time*0.66f).SetEase(Ease.InCubic));
                    }
                    sq.Insert(0,canvasGroup.DOFade(0f,time));
                    break;
                case ZShowType.Slide:
                    if(rectTransform!=null)
                    {
                        var offset=SlideOffset(rectTransform,direction);
                        sq.Append(rectTransform.DOAnchorPos(rectTransform.anchoredPosition+offset,time).SetEase(Ease.InCubic));
                    }
                    sq.Insert(0,canvasGroup.DOFade(0f,time));
                    break;
                default:
                    sq.Append(canvasGroup.DOFade(0f,time));
                    break;
            }
            sq.OnComplete(()=>
            {
                canvasGroup.gameObject.SetActive(false);
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return new(sq);
        }
    }

    //按钮
    public static partial class ZTweenUtility
    {
        public static ZTween ZButtonPress(this RectTransform rectTransform,float time=0.2f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one*0.9f,time*0.3f).SetEase(Ease.OutQuad));
            sq.Append(rectTransform.DOScale(Vector3.one*1.05f,time*0.4f).SetEase(Ease.OutCubic));
            sq.Append(rectTransform.DOScale(Vector3.one,time*0.3f).SetEase(Ease.OutQuart));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZButtonPress(this Button button,float time=0.2f,Action onCompleteCallBack=null)
        {
            return ((RectTransform)button.transform).ZButtonPress(time,onCompleteCallBack);
        }

        public static ZTween ZHoverEnter(this RectTransform rectTransform,float time=0.15f,float maxScale=1.05f,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DOScale(Vector3.one*maxScale,time).SetEase(Ease.OutCubic);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZHoverEnter(this Button button,float time=0.15f,float maxScale=1.05f,Action onCompleteCallBack=null)
        {
            return ((RectTransform)button.transform).ZHoverEnter(time,maxScale,onCompleteCallBack);
        }

        public static ZTween ZHoverExit(this RectTransform rectTransform,float time=0.15f,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DOScale(Vector3.one,time).SetEase(Ease.OutCubic);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZHoverExit(this Button button,float time=0.15f,Action onCompleteCallBack=null)
        {
            return ((RectTransform)button.transform).ZHoverExit(time,onCompleteCallBack);
        }

        public static ZTween ZSelect(this RectTransform rectTransform,float time=0.15f,float maxScale=1.08f,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DOScale(Vector3.one*maxScale,time).SetEase(Ease.OutBack);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZDeselect(this RectTransform rectTransform,float time=0.15f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZHoverExit(time,onCompleteCallBack);
        }

        public static ZTween ZDisable(this CanvasGroup canvasGroup,float time=0.2f,Action onCompleteCallBack=null)
        {
            SetRaycast(canvasGroup,false);
            var tween=canvasGroup.DOFade(0.5f,time);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZEnable(this CanvasGroup canvasGroup,float time=0.2f,Action onCompleteCallBack=null)
        {
            var tween=canvasGroup.DOFade(1f,time);
            tween.OnComplete(()=>
            {
                SetRaycast(canvasGroup,true);
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return new(tween);
        }
        public static ZTween ZDisable(this Graphic graphic,float time=0.2f,Action onCompleteCallBack=null)
        {
            var tween=graphic.DOFade(0.5f,time);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZEnable(this Graphic graphic,float time=0.2f,Action onCompleteCallBack=null)
        {
            var tween=graphic.DOFade(1f,time);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZSetState(this RectTransform rectTransform,ZButtonState state,float time=0.15f,Action onCompleteCallBack=null)
        {
            switch(state)
            {
                case ZButtonState.Hover: return rectTransform.ZHoverEnter(time,1.05f,onCompleteCallBack);
                case ZButtonState.Pressed: return rectTransform.ZButtonPress(time,onCompleteCallBack);
                case ZButtonState.Selected: return rectTransform.ZSelect(time,1.08f,onCompleteCallBack);
                case ZButtonState.Disabled:
                    var canvasGroup=rectTransform.GetComponent<CanvasGroup>();
                    if(canvasGroup!=null)return canvasGroup.ZDisable(time,onCompleteCallBack);
                    var graphic=rectTransform.GetComponent<Graphic>();
                    if(graphic!=null)return graphic.ZDisable(time,onCompleteCallBack);
                    return rectTransform.ZScale(1f,time,Ease.OutCubic,onCompleteCallBack);
                default: return rectTransform.ZHoverExit(time,onCompleteCallBack);
            }
        }
    }

    //颜色
    public static partial class ZTweenUtility
    {
        public static ZTween ZColor(this Graphic graphic,Color endValue,float time=0.2f,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=graphic.DOColor(endValue,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZTextColor(this Text text,Color endValue,float time=0.2f,Ease ease=Ease.Linear,Action onCompleteCallBack=null)
        {
            var tween=text.DOColor(endValue,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZFlash(this Graphic graphic,Color flashColor,float time=0.1f,Action onCompleteCallBack=null)
        {
            var origin=graphic.color;
            var sq=DOTween.Sequence();
            sq.Append(graphic.DOColor(flashColor,time*0.5f));
            sq.Append(graphic.DOColor(origin,time*0.5f));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
    }

    //反馈
    public static partial class ZTweenUtility
    {
        public static ZTween ZShake(this RectTransform rectTransform,float time=0.35f,float strength=24f,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DOShakeAnchorPos(time,new Vector2(strength,0f),20,90,false,true);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZError(this RectTransform rectTransform,float time=0.4f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOShakeAnchorPos(time,new Vector2(24f,0f),20,90,false,true));
            var graphic=rectTransform.GetComponent<Graphic>();
            if(graphic!=null)
            {
                var origin=graphic.color;
                sq.Insert(0,graphic.DOColor(Color.red,time*0.35f));
                sq.Insert(time*0.35f,graphic.DOColor(origin,time*0.35f));
            }
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZWarning(this RectTransform rectTransform,float time=0.4f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOShakeAnchorPos(time,new Vector2(16f,0f),16,90,false,true));
            var graphic=rectTransform.GetComponent<Graphic>();
            if(graphic!=null)
            {
                var origin=graphic.color;
                var warn=new Color(1f,0.75f,0.1f,origin.a);
                sq.Insert(0,graphic.DOColor(warn,time*0.35f));
                sq.Insert(time*0.35f,graphic.DOColor(origin,time*0.35f));
            }
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZSuccess(this RectTransform rectTransform,float time=0.35f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOPunchScale(Vector3.one*0.2f,time,8,0.6f));
            var graphic=rectTransform.GetComponent<Graphic>();
            if(graphic!=null)
            {
                var origin=graphic.color;
                sq.Insert(0,graphic.DOColor(Color.green,time*0.35f));
                sq.Insert(time*0.35f,graphic.DOColor(origin,time*0.35f));
            }
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }

        public static ZTween ZPunchScale(this RectTransform rectTransform,float time=0.3f,float punch=0.2f,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DOPunchScale(Vector3.one*punch,time,8,0.6f);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZPunchPosition(this RectTransform rectTransform,float time=0.3f,Vector2 punch=default,Action onCompleteCallBack=null)
        {
            if(punch==default)punch=new Vector2(0f,20f);
            var tween=rectTransform.DOPunchAnchorPos(punch,time,8,0.6f);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZPunchRotation(this RectTransform rectTransform,float time=0.3f,float punch=15f,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DOPunchRotation(new Vector3(0f,0f,punch),time,8,0.6f);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZBounce(this RectTransform rectTransform,float time=0.45f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one*1.2f,time*0.28f).SetEase(Ease.OutCubic));
            sq.Append(rectTransform.DOScale(Vector3.one*0.95f,time*0.24f).SetEase(Ease.InOutQuad));
            sq.Append(rectTransform.DOScale(Vector3.one*1.05f,time*0.24f).SetEase(Ease.OutQuad));
            sq.Append(rectTransform.DOScale(Vector3.one,time*0.24f).SetEase(Ease.OutQuart));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }

        public static ZTween ZAttention(this RectTransform rectTransform,float time=0.4f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZPunchScale(time,0.18f,onCompleteCallBack);
        }
    }

    //文本
    public static partial class ZTweenUtility
    {
        public static ZTween ZNumber(this Text text,int from,int to,float time,Action onCompleteCallBack=null)
        {
            var tween=DOTween.To(()=>from,x=>
            {
                from=x;
                text.text=x.ToString();
            },to,time).SetEase(Ease.OutCubic);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZNumber(this Text text,float from,float to,float time,string format="0.##",Action onCompleteCallBack=null)
        {
            var tween=DOTween.To(()=>from,x=>
            {
                from=x;
                text.text=x.ToString(format);
            },to,time).SetEase(Ease.OutCubic);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZCurrency(this Text text,int from,int to,float time,string prefix="$",Action onCompleteCallBack=null)
        {
            var tween=DOTween.To(()=>from,x=>
            {
                from=x;
                text.text=prefix+x;
            },to,time).SetEase(Ease.OutCubic);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZTypewriter(this Text text,string content,float time,Action onCompleteCallBack=null)
        {
            text.text="";
            var tween=text.DOText(content==null?"":content,time,true).SetEase(Ease.Linear);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZReveal(this Text text,float time=0.25f,Action onCompleteCallBack=null)
        {
            var tween=text.DOFade(1f,time).From(0f);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZShakeText(this Text text,float time=0.3f,Action onCompleteCallBack=null)
        {
            return ((RectTransform)text.transform).ZShake(time,16f,onCompleteCallBack);
        }
        public static ZTween ZPunchText(this Text text,float time=0.3f,Action onCompleteCallBack=null)
        {
            return ((RectTransform)text.transform).ZPunchScale(time,0.25f,onCompleteCallBack);
        }
    }

    //图标
    public static partial class ZTweenUtility
    {
        public static ZTween ZIconPopup(this RectTransform rectTransform,float time=0.35f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZPopup(time,1.25f,onCompleteCallBack);
        }
        public static ZTween ZIconRotate(this RectTransform rectTransform,float time=0.4f,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DORotate(new Vector3(0f,0f,-360f),time,RotateMode.FastBeyond360).SetEase(Ease.OutCubic);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZIconBounce(this RectTransform rectTransform,float time=0.4f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZBounce(time,onCompleteCallBack);
        }
        public static ZTween ZIconFloat(this RectTransform rectTransform,float time=0.8f,float offset=12f,int loops=-1,Action onCompleteCallBack=null)
        {
            return rectTransform.ZFloat(time,offset,loops,onCompleteCallBack);
        }
    }

    //提示
    public static partial class ZTweenUtility
    {
        public static ZTween ZToast(this RectTransform rectTransform,float stay=2f,ZDirection direction=ZDirection.Up,float time=0.3f,Action onCompleteCallBack=null)
        {
            var origin=rectTransform.anchoredPosition;
            var offset=SlideOffset(rectTransform,direction);
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOAnchorPos(origin,time).From(origin+offset).SetEase(Ease.OutCubic));
            sq.AppendInterval(stay);
            sq.Append(rectTransform.DOAnchorPos(origin+offset,time).SetEase(Ease.InCubic));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZNotification(this RectTransform rectTransform,float stay=2f,ZDirection direction=ZDirection.Right,float time=0.3f,Action onCompleteCallBack=null)
        {
            var origin=rectTransform.anchoredPosition;
            var offset=SlideOffset(rectTransform,direction);
            var canvasGroup=rectTransform.GetComponent<CanvasGroup>();
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOAnchorPos(origin,time).From(origin+offset).SetEase(Ease.OutCubic));
            if(canvasGroup!=null)sq.Join(canvasGroup.DOFade(1f,time).From(0f));
            sq.AppendInterval(stay);
            sq.Append(rectTransform.DOAnchorPos(origin+offset,time).SetEase(Ease.InCubic));
            if(canvasGroup!=null)sq.Join(canvasGroup.DOFade(0f,time));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZTooltipShow(this RectTransform rectTransform,float time=0.2f,Action onCompleteCallBack=null)
        {
            rectTransform.gameObject.SetActive(true);
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one,time).From(Vector3.one*0.95f).SetEase(Ease.OutCubic));
            var canvasGroup=rectTransform.GetComponent<CanvasGroup>();
            if(canvasGroup!=null)sq.Join(canvasGroup.DOFade(1f,time).From(0f));
            else
            {
                var graphic=rectTransform.GetComponent<Graphic>();
                if(graphic!=null)sq.Join(graphic.DOFade(1f,time).From(0f));
            }
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZTooltipHide(this RectTransform rectTransform,float time=0.15f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one*0.95f,time).SetEase(Ease.InCubic));
            var canvasGroup=rectTransform.GetComponent<CanvasGroup>();
            if(canvasGroup!=null)sq.Join(canvasGroup.DOFade(0f,time));
            else
            {
                var graphic=rectTransform.GetComponent<Graphic>();
                if(graphic!=null)sq.Join(graphic.DOFade(0f,time));
            }
            sq.OnComplete(()=>
            {
                rectTransform.gameObject.SetActive(false);
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return new(sq);
        }
        public static ZTween ZBadgeShow(this RectTransform rectTransform,float time=0.3f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            var pop=rectTransform.ZPopup(time);
            pop.tween.Pause();
            sq.Append(pop.tween);
            sq.Append(rectTransform.DOPunchScale(Vector3.one*0.2f,0.2f,8,0.5f));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZBadgePunch(this RectTransform rectTransform,float time=0.25f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZPunchScale(time,0.25f,onCompleteCallBack);
        }
    }

    //面板组合
    public static partial class ZTweenUtility
    {
        public static ZTween ZTabSelect(this RectTransform rectTransform,float time=0.2f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one*1.08f,time).SetEase(Ease.OutBack));
            var canvasGroup=rectTransform.GetComponent<CanvasGroup>();
            if(canvasGroup!=null)sq.Join(canvasGroup.DOFade(1f,time));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZTabDeselect(this RectTransform rectTransform,float time=0.2f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one,time).SetEase(Ease.OutCubic));
            var canvasGroup=rectTransform.GetComponent<CanvasGroup>();
            if(canvasGroup!=null)sq.Join(canvasGroup.DOFade(0.6f,time));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }

        public static ZTween ZPageSwitch(this RectTransform oldPage,RectTransform newPage,ZDirection direction=ZDirection.Left,float time=0.35f,Action onCompleteCallBack=null)
        {
            newPage.gameObject.SetActive(true);
            var enterDir=Opposite(direction);
            var sq=DOTween.Sequence();
            var outTween=oldPage.ZSlideOut(direction,time);
            outTween.tween.Pause();
            var inTween=newPage.ZSlideIn(enterDir,time);
            inTween.tween.Pause();
            sq.Append(outTween.tween);
            sq.Join(inTween.tween);
            sq.OnComplete(()=>
            {
                oldPage.gameObject.SetActive(false);
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return new(sq);
        }

        public static ZTween ZScrollTo(this ScrollRect scrollRect,RectTransform target,float time=0.35f,Ease ease=Ease.OutCubic,Action onCompleteCallBack=null)
        {
            Canvas.ForceUpdateCanvases();
            var content=scrollRect.content;
            var viewport=scrollRect.viewport!=null?scrollRect.viewport:(RectTransform)scrollRect.transform;
            var end=content.anchoredPosition;
            if(scrollRect.horizontal)end.x=-(viewport.localPosition.x+target.localPosition.x);
            if(scrollRect.vertical)end.y=-(viewport.localPosition.y+target.localPosition.y);
            var tween=content.DOAnchorPos(end,time).SetEase(ease);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZScrollToIndex(this ScrollRect scrollRect,int index,float time=0.35f,Ease ease=Ease.OutCubic,Action onCompleteCallBack=null)
        {
            var child=scrollRect.content.GetChild(index) as RectTransform;
            return scrollRect.ZScrollTo(child,time,ease,onCompleteCallBack);
        }
        public static ZTween ZScrollSnap(this ScrollRect scrollRect,int index,float time=0.35f,Action onCompleteCallBack=null)
        {
            return scrollRect.ZScrollToIndex(index,time,Ease.OutBack,onCompleteCallBack);
        }
    }
}
