using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
namespace ZZ.ZTween
{
    public static partial class ZTweenUtility
    {
        public static ZTween ZStagger(this IList<RectTransform> items,Func<RectTransform,ZTween> anim,float interval=0.05f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            for(int i=0;i<items.Count;i++)
            {
                if(items[i]==null)continue;
                var z=anim(items[i]);
                z.tween.Pause();
                sq.Insert(i*interval,z.tween);
            }
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZListShow(this IList<RectTransform> items,float time=0.35f,float interval=0.05f,Action onCompleteCallBack=null)
        {
            return items.ZStagger(item=>item.ZPopup(time),interval,onCompleteCallBack);
        }
        public static ZTween ZListHide(this IList<RectTransform> items,float time=0.25f,float interval=0.04f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            for(int i=0;i<items.Count;i++)
            {
                int idx=items.Count-1-i;
                if(items[idx]==null)continue;
                var z=items[idx].ZClose(time);
                z.tween.Pause();
                sq.Insert(i*interval,z.tween);
            }
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }

        public static ZTween ZModalShow(this CanvasGroup background,RectTransform panel,float time=0.3f,float bgAlpha=0.5f,Action onCompleteCallBack=null)
        {
            background.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            SetRaycast(background,false);
            var sq=DOTween.Sequence();
            sq.Append(background.DOFade(bgAlpha,time).From(0f));
            var pop=panel.ZPopup(time);
            pop.tween.Pause();
            sq.Join(pop.tween);
            sq.OnComplete(()=>
            {
                SetRaycast(background,true);
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return new(sq);
        }
        public static ZTween ZModalHide(this CanvasGroup background,RectTransform panel,float time=0.3f,Action onCompleteCallBack=null)
        {
            SetRaycast(background,false);
            var sq=DOTween.Sequence();
            var close=panel.ZClose(time);
            close.tween.Pause();
            sq.Append(close.tween);
            sq.Join(background.DOFade(0f,time));
            sq.OnComplete(()=>
            {
                background.gameObject.SetActive(false);
                panel.gameObject.SetActive(false);
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return new(sq);
        }
        public static ZTween ZDialogShow(this RectTransform dialog,float time=0.4f,Action onCompleteCallBack=null)
        {
            dialog.gameObject.SetActive(true);
            return dialog.ZPopupFade(time,1.15f,onCompleteCallBack);
        }
        public static ZTween ZDialogHide(this RectTransform dialog,float time=0.3f,Action onCompleteCallBack=null)
        {
            var close=dialog.ZClose(time);
            close.tween.OnComplete(()=>
            {
                dialog.gameObject.SetActive(false);
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return close;
        }
    }

    //进度与加载
    public static partial class ZTweenUtility
    {
        public static ZTween ZProgress(this Image bar,float from,float to,float time,Action onCompleteCallBack=null)
        {
            bar.fillAmount=from;
            var tween=bar.DOFillAmount(to,time).SetEase(Ease.OutCubic);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZHealthBar(this Image bar,float from,float to,float time=0.3f,Image delayedBar=null,Action onCompleteCallBack=null)
        {
            bar.fillAmount=from;
            var sq=DOTween.Sequence();
            sq.Append(bar.DOFillAmount(to,time).SetEase(Ease.OutCubic));
            if(delayedBar!=null)
            {
                delayedBar.fillAmount=from;
                sq.Insert(0.12f,delayedBar.DOFillAmount(to,time).SetEase(Ease.OutQuad));
            }
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZSlider(this Slider slider,float endValue,float time=0.3f,Action onCompleteCallBack=null)
        {
            var tween=slider.DOValue(endValue,time).SetEase(Ease.OutCubic);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZSpinner(this RectTransform rectTransform,float time=0.8f,int loops=-1,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DORotate(new Vector3(0f,0f,-360f),time,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(loops,LoopType.Restart);
            if(loops>0&&onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZLoadingDots(this Text text,string baseText="Loading",float time=0.8f,int loops=-1,Action onCompleteCallBack=null)
        {
            var tween=DOTween.To(()=>0,x=>
            {
                int n=Mathf.Clamp(Mathf.RoundToInt(x),0,3);
                text.text=baseText+new string('.',n);
            },3f,time).SetEase(Ease.Linear).SetLoops(loops,LoopType.Restart);
            if(loops>0&&onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZLoadingProgress(this Image bar,float progress,float time=0.3f,Action onCompleteCallBack=null)
        {
            return bar.ZProgress(bar.fillAmount,progress,time,onCompleteCallBack);
        }
        public static ZTween ZWipe(this Image image,ZDirection direction=ZDirection.Left,float time=0.4f,Action onCompleteCallBack=null)
        {
            if(direction==ZDirection.Left||direction==ZDirection.Right)
            {
                image.fillMethod=Image.FillMethod.Horizontal;
                image.fillOrigin=direction==ZDirection.Left?0:1;
            }
            else
            {
                image.fillMethod=Image.FillMethod.Vertical;
                image.fillOrigin=direction==ZDirection.Up?1:0;
            }
            image.fillAmount=0f;
            var tween=image.DOFillAmount(1f,time).SetEase(Ease.InOutCubic);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZTransitionFadeOut(this CanvasGroup canvasGroup,float time=0.35f,Action onCompleteCallBack=null)
        {
            canvasGroup.gameObject.SetActive(true);
            return canvasGroup.ZFadeIn(time,Ease.Linear,onCompleteCallBack);
        }
        public static ZTween ZTransitionFadeIn(this CanvasGroup canvasGroup,float time=0.35f,Action onCompleteCallBack=null)
        {
            var tween=canvasGroup.DOFade(0f,time);
            tween.OnComplete(()=>
            {
                canvasGroup.gameObject.SetActive(false);
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return new(tween);
        }
        public static ZTween ZCountdown(this Text text,int from=3,float stepTime=0.8f,string goText="GO!",Action onCompleteCallBack=null)
        {
            var rectTransform=(RectTransform)text.transform;
            var sq=DOTween.Sequence();
            for(int i=from;i>=1;i--)
            {
                int value=i;
                sq.AppendCallback(()=>text.text=value.ToString());
                sq.Append(rectTransform.DOScale(Vector3.one,stepTime*0.7f).From(Vector3.one*1.4f).SetEase(Ease.OutBack));
                sq.Join(text.DOFade(1f,stepTime*0.25f).From(0f));
                sq.Append(text.DOFade(0f,stepTime*0.3f));
            }
            sq.AppendCallback(()=>text.text=goText);
            sq.Append(rectTransform.DOPunchScale(Vector3.one*0.3f,stepTime*0.6f,8,0.5f));
            sq.Join(text.DOFade(1f,0.15f).From(0f));
            if(onCompleteCallBack!=null)sq.AppendCallback(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZTimerBar(this Image bar,float duration,Action onCompleteCallBack=null)
        {
            bar.fillAmount=1f;
            var tween=bar.DOFillAmount(0f,duration).SetEase(Ease.Linear);
            if(onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZCooldown(this Image mask,float duration,Text numberText=null,RectTransform punchTarget=null,Action onCompleteCallBack=null)
        {
            mask.fillAmount=1f;
            var sq=DOTween.Sequence();
            sq.Append(mask.DOFillAmount(0f,duration).SetEase(Ease.Linear));
            if(numberText!=null)
            {
                float remain=duration;
                sq.Join(DOTween.To(()=>remain,x=>
                {
                    remain=x;
                    numberText.text=Mathf.CeilToInt(x).ToString();
                },0f,duration).SetEase(Ease.Linear));
            }
            if(punchTarget!=null)sq.Append(punchTarget.DOPunchScale(Vector3.one*0.2f,0.25f,8,0.5f));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
    }

    //循环与引导
    public static partial class ZTweenUtility
    {
        public static ZTween ZFloat(this RectTransform rectTransform,float time=0.8f,float offset=12f,int loops=-1,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y+offset,time).SetEase(Ease.InOutSine).SetLoops(loops,LoopType.Yoyo);
            if(loops>0&&onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZPulse(this RectTransform rectTransform,float time=0.4f,float maxScale=1.05f,int loops=-1,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DOScale(Vector3.one*maxScale,time).SetEase(Ease.InOutSine).SetLoops(loops,LoopType.Yoyo);
            if(loops>0&&onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZBlink(this CanvasGroup canvasGroup,float time=0.35f,int loops=-1,Action onCompleteCallBack=null)
        {
            var tween=canvasGroup.DOFade(0.2f,time).SetEase(Ease.InOutSine).SetLoops(loops,LoopType.Yoyo);
            if(loops>0&&onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZBlink(this Graphic graphic,float time=0.35f,int loops=-1,Action onCompleteCallBack=null)
        {
            var tween=graphic.DOFade(0.2f,time).SetEase(Ease.InOutSine).SetLoops(loops,LoopType.Yoyo);
            if(loops>0&&onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }
        public static ZTween ZRotateLoop(this RectTransform rectTransform,float time=1f,int loops=-1,Action onCompleteCallBack=null)
        {
            var tween=rectTransform.DORotate(new Vector3(0f,0f,-360f),time,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(loops,LoopType.Restart);
            if(loops>0&&onCompleteCallBack!=null)tween.OnComplete(()=>onCompleteCallBack());
            return new(tween);
        }

        public static ZTween ZHighlight(this CanvasGroup target,CanvasGroup[] others=null,float dim=0.3f,float time=0.25f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(target.DOFade(1f,time));
            if(others!=null)
            {
                for(int i=0;i<others.Length;i++)
                {
                    if(others[i]==null||others[i]==target)continue;
                    sq.Join(others[i].DOFade(dim,time));
                }
            }
            var rectTransform=target.transform as RectTransform;
            if(rectTransform!=null)sq.Join(rectTransform.DOPunchScale(Vector3.one*0.08f,time,4,0.4f));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZSpotlight(this CanvasGroup target,CanvasGroup[] others,float dim=0.2f,float time=0.25f,Action onCompleteCallBack=null)
        {
            return target.ZHighlight(others,dim,time,onCompleteCallBack);
        }
        public static ZTween ZTutorialShow(this CanvasGroup step,CanvasGroup background=null,float time=0.3f,float bgAlpha=0.55f,Action onCompleteCallBack=null)
        {
            step.gameObject.SetActive(true);
            var sq=DOTween.Sequence();
            if(background!=null)
            {
                background.gameObject.SetActive(true);
                sq.Append(background.DOFade(bgAlpha,time).From(0f));
            }
            var rectTransform=step.transform as RectTransform;
            if(rectTransform!=null)
            {
                var pop=rectTransform.ZPopupFade(time);
                pop.tween.Pause();
                if(sq.Duration(false)>0f)sq.Join(pop.tween);
                else sq.Append(pop.tween);
            }
            else sq.Append(step.DOFade(1f,time).From(0f));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZTutorialHide(this CanvasGroup step,CanvasGroup background=null,float time=0.25f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            var rectTransform=step.transform as RectTransform;
            if(rectTransform!=null)
            {
                var close=rectTransform.ZClose(time);
                close.tween.Pause();
                sq.Append(close.tween);
            }
            else sq.Append(step.DOFade(0f,time));
            if(background!=null)sq.Join(background.DOFade(0f,time));
            sq.OnComplete(()=>
            {
                step.gameObject.SetActive(false);
                if(background!=null)background.gameObject.SetActive(false);
                if(onCompleteCallBack!=null)onCompleteCallBack();
            });
            return new(sq);
        }
    }

    //卡片与物品
    public static partial class ZTweenUtility
    {
        public static ZTween ZPointerEnter(this RectTransform rectTransform,float time=0.15f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZHoverEnter(time,1.05f,onCompleteCallBack);
        }
        public static ZTween ZPointerExit(this RectTransform rectTransform,float time=0.15f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZHoverExit(time,onCompleteCallBack);
        }
        public static ZTween ZPointerClick(this RectTransform rectTransform,float time=0.2f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZButtonPress(time,onCompleteCallBack);
        }

        public static ZTween ZCardHover(this RectTransform rectTransform,float time=0.18f,float y=24f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one*1.08f,time).SetEase(Ease.OutCubic));
            sq.Join(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y+y,time).SetEase(Ease.OutCubic));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZCardSelect(this RectTransform rectTransform,float time=0.2f,float y=32f,float z=8f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one*1.12f,time).SetEase(Ease.OutBack));
            sq.Join(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y+y,time).SetEase(Ease.OutCubic));
            sq.Join(rectTransform.DORotate(new Vector3(0f,0f,z),time).SetEase(Ease.OutCubic));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZCardPlay(this RectTransform rectTransform,Vector2 endValue,float time=0.35f,Action onCompleteCallBack=null)
        {
            var canvasGroup=rectTransform.GetComponent<CanvasGroup>();
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOAnchorPos(endValue,time).SetEase(Ease.InOutCubic));
            sq.Join(rectTransform.DORotate(new Vector3(0f,0f,12f),time));
            sq.Join(rectTransform.DOScale(Vector3.one*0.85f,time));
            if(canvasGroup!=null)sq.Join(canvasGroup.DOFade(0f,time));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZCardDiscard(this RectTransform rectTransform,float time=0.3f,Action onCompleteCallBack=null)
        {
            var canvasGroup=rectTransform.GetComponent<CanvasGroup>();
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DORotate(new Vector3(0f,0f,-20f),time).SetEase(Ease.InCubic));
            sq.Join(rectTransform.DOScale(Vector3.zero,time));
            if(canvasGroup!=null)sq.Join(canvasGroup.DOFade(0f,time));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }

        public static ZTween ZItemAdd(this RectTransform rectTransform,float time=0.35f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            var pop=rectTransform.ZPopup(time);
            pop.tween.Pause();
            sq.Append(pop.tween);
            sq.Append(rectTransform.DOPunchScale(Vector3.one*0.15f,0.2f,8,0.5f));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZItemRemove(this RectTransform rectTransform,float time=0.25f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZClose(time,0.9f,onCompleteCallBack);
        }
        public static ZTween ZItemSelect(this RectTransform rectTransform,float time=0.15f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZSelect(time,1.08f,onCompleteCallBack);
        }
        public static ZTween ZItemHover(this RectTransform rectTransform,float time=0.15f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZHoverEnter(time,1.06f,onCompleteCallBack);
        }

        public static ZTween ZDragBegin(this RectTransform rectTransform,float time=0.12f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZScale(1.1f,time,Ease.OutCubic,onCompleteCallBack);
        }
        public static ZTween ZDragEnd(this RectTransform rectTransform,float time=0.12f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZScale(1f,time,Ease.OutCubic,onCompleteCallBack);
        }
        public static ZTween ZDragInvalid(this RectTransform rectTransform,float time=0.3f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZShake(time,20f,onCompleteCallBack);
        }

        public static ZTween ZFocus(this RectTransform rectTransform,float time=0.15f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZSelect(time,1.06f,onCompleteCallBack);
        }
        public static ZTween ZUnfocus(this RectTransform rectTransform,float time=0.15f,Action onCompleteCallBack=null)
        {
            return rectTransform.ZDeselect(time,onCompleteCallBack);
        }

        public static ZTween ZRewardShow(this RectTransform rectTransform,float time=0.55f,Action onCompleteCallBack=null)
        {
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one*1.2f,time*0.45f).From(Vector3.zero).SetEase(Ease.OutCubic));
            sq.Append(rectTransform.DOScale(Vector3.one,time*0.25f).SetEase(Ease.OutQuart));
            sq.Join(rectTransform.DORotate(new Vector3(0f,0f,-360f),time*0.55f,RotateMode.FastBeyond360).SetEase(Ease.OutCubic));
            sq.Append(rectTransform.DOPunchScale(Vector3.one*0.18f,time*0.3f,8,0.5f));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZLevelUp(this Text levelText,float time=0.5f,Action onCompleteCallBack=null)
        {
            var rectTransform=(RectTransform)levelText.transform;
            var sq=DOTween.Sequence();
            sq.Append(rectTransform.DOScale(Vector3.one,time*0.45f).From(Vector3.one*0.4f).SetEase(Ease.OutBack));
            sq.Append(rectTransform.DOPunchScale(Vector3.one*0.25f,time*0.35f,8,0.5f));
            var origin=levelText.color;
            sq.Insert(0,levelText.DOColor(Color.yellow,time*0.25f));
            sq.Insert(time*0.25f,levelText.DOColor(origin,time*0.25f));
            if(onCompleteCallBack!=null)sq.OnComplete(()=>onCompleteCallBack());
            return new(sq);
        }
        public static ZTween ZAchievementShow(this RectTransform panel,float stay=1.6f,ZDirection direction=ZDirection.Right,float time=0.3f,Action onCompleteCallBack=null)
        {
            return panel.ZNotification(stay,direction,time,onCompleteCallBack);
        }
    }

    //序列
    public static partial class ZTweenUtility
    {
        public static ZTween ZSequence()
        {
            return new(DOTween.Sequence());
        }
        public static ZTween ZAppend(this ZTween zTween,ZTween other)
        {
            if(zTween.tween is Sequence sq)
            {
                sq.Append(other.tween);
                return zTween;
            }
            var n=DOTween.Sequence();
            n.Append(zTween.tween);
            n.Append(other.tween);
            return new(n);
        }
        public static ZTween ZJoin(this ZTween zTween,ZTween other)
        {
            if(zTween.tween is Sequence sq)
            {
                sq.Join(other.tween);
                return zTween;
            }
            var n=DOTween.Sequence();
            n.Append(zTween.tween);
            n.Join(other.tween);
            return new(n);
        }
        public static ZTween ZAppendInterval(this ZTween zTween,float time)
        {
            if(zTween.tween is Sequence sq)
            {
                sq.AppendInterval(time);
                return zTween;
            }
            var n=DOTween.Sequence();
            n.Append(zTween.tween);
            n.AppendInterval(time);
            return new(n);
        }
        public static ZTween ZAppendCallback(this ZTween zTween,Action callback)
        {
            if(zTween.tween is Sequence sq)
            {
                sq.AppendCallback(()=>callback());
                return zTween;
            }
            var n=DOTween.Sequence();
            n.Append(zTween.tween);
            n.AppendCallback(()=>callback());
            return new(n);
        }
        public static void Reverse(ZTween zTween)
        {
            zTween.tween.PlayBackwards();
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
        public static void Kill(Component target,bool complete=false)
        {
            target.DOKill(complete);
        }
        public static void Pause(ZTween zTween)
        {
            zTween.tween.Pause();
        }
        public static void Pause(Component target)
        {
            target.DOPause();
        }
        public static void Play(ZTween zTween)
        {
            zTween.tween.Play();
        }
        public static void Play(Component target)
        {
            target.DOPlay();
        }
        public static void ReStart(ZTween zTween)
        {
           zTween.tween.Restart();
        }
        public static void Rewind(ZTween zTween)
        {
            zTween.tween.Rewind();
        }
        public static void Resume(ZTween zTween)
        {
            zTween.tween.Play();
        }
        public static void Resume(Component target)
        {
            target.DOPlay();
        }
        public static bool IsPlaying(ZTween zTween)
        {
            return zTween!=null&&zTween.tween!=null&&zTween.tween.IsActive()&&zTween.tween.IsPlaying();
        }
        public static bool IsPlaying(Component target)
        {
            return DOTween.IsTweening(target);
        }
    }

    public static partial class ZTweenUtility
    {
        static Vector2 SlideOffset(RectTransform rectTransform,ZDirection direction)
        {
            var size=rectTransform.rect.size;
            switch(direction)
            {
                case ZDirection.Left: return new Vector2(-size.x,0f);
                case ZDirection.Right: return new Vector2(size.x,0f);
                case ZDirection.Up: return new Vector2(0f,size.y);
                default: return new Vector2(0f,-size.y);
            }
        }
        static ZDirection Opposite(ZDirection direction)
        {
            switch(direction)
            {
                case ZDirection.Left: return ZDirection.Right;
                case ZDirection.Right: return ZDirection.Left;
                case ZDirection.Up: return ZDirection.Down;
                default: return ZDirection.Up;
            }
        }
        static void SetRaycast(CanvasGroup canvasGroup,bool value)
        {
            canvasGroup.interactable=value;
            canvasGroup.blocksRaycasts=value;
        }
    }
}
