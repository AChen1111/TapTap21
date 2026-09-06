using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using ZZ.ZTween;

public class UITest : MonoBehaviour
{
    Canvas canvas;
    RectTransform panel;
    RectTransform icon;
    RectTransform button;
    RectTransform toast;
    RectTransform tooltip;
    RectTransform badge;
    RectTransform pageA;
    RectTransform pageB;
    RectTransform card;
    RectTransform modalPanel;
    CanvasGroup panelGroup;
    CanvasGroup toastGroup;
    CanvasGroup tooltipGroup;
    CanvasGroup modalBg;
    CanvasGroup transition;
    CanvasGroup dimA;
    CanvasGroup dimB;
    CanvasGroup tutorialStep;
    Image bar;
    Image delayedBar;
    Image wipe;
    Image cooldownMask;
    Image colorTarget;
    Slider slider;
    ScrollRect scrollRect;
    Text demoText;
    Text titleText;
    Text hintText;
    Text loadingText;
    Text countdownText;
    readonly List<RectTransform> listItems = new();
    readonly List<Snapshot> snapshots = new();

    int index;
    bool finished;
    readonly List<Step> steps = new();

    void Awake()
    {
        BuildUI();
        CaptureSnapshots();
        BuildSteps();
    }

    void Start()
    {
        PlayCurrent();
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if(keyboard==null||!keyboard.spaceKey.wasPressedThisFrame)return;
        if(finished)
        {
            finished=false;
            index=0;
            PlayCurrent();
            return;
        }
        Next();
    }

    void Next()
    {
        index++;
        if(index>=steps.Count)
        {
            finished=true;
            ZTweenUtility.KillAll();
            Restore();
            titleText.text="演示结束";
            hintText.text="空格：从头再看一遍";
            Debug.Log("[ZTween UI] 全部效果已演示完。按空格重新开始。");
            return;
        }
        PlayCurrent();
    }

    void PlayCurrent()
    {
        var step=steps[index];
        ZTweenUtility.KillAll();
        Restore();
        titleText.text=$"{index+1}/{steps.Count}  {step.name}";
        hintText.text="空格：下一个效果";
        Debug.Log($"[ZTween UI] {index+1}/{steps.Count}  {step.name}  —  {step.desc}");
        step.play();
    }

    void BuildSteps()
    {
        Add("ZFadeIn", "CanvasGroup 淡入", ()=>
        {
            panelGroup.alpha=0f;
            panelGroup.ZFadeIn(0.45f);
        });
        Add("ZFadeOut", "CanvasGroup 淡出", ()=>panelGroup.ZFadeOut(0.45f));
        Add("ZFade", "CanvasGroup 淡到 0.35", ()=>panelGroup.ZFade(0.35f,0.4f));
        Add("ZFadeIn Image", "Image 淡入", ()=>
        {
            colorTarget.color=new Color(0.35f,0.62f,0.95f,0f);
            colorTarget.ZFadeIn(0.45f);
        });
        Add("ZScale", "缩放到 1.35", ()=>panel.ZScale(1.35f,0.4f));
        Add("ZMove", "面板右移", ()=>panel.ZMove(panel.anchoredPosition+new Vector2(180f,0f),0.4f));
        Add("ZRotate", "旋转 25 度", ()=>panel.ZRotate(25f,0.4f));
        Add("ZPopup", "弹出：0 → 1.2 → 1", ()=>panel.ZPopup());
        Add("ZPopupFade", "弹出并淡入", ()=>panel.ZPopupFade());
        Add("ZClose", "关闭：1 → 0.9 → 0", ()=>panel.ZClose());
        Add("ZSlideIn", "从左侧滑入", ()=>panel.ZSlideIn(ZDirection.Left));
        Add("ZSlideOut", "向右滑出", ()=>panel.ZSlideOut(ZDirection.Right));
        Add("ZShow Fade", "Show + Fade，并恢复点击", ()=>panelGroup.ZShow(0.35f,ZShowType.Fade));
        Add("ZShow Popup", "Show + Popup", ()=>panelGroup.ZShow(0.4f,ZShowType.Popup));
        Add("ZShow Slide", "Show + 从下滑入", ()=>panelGroup.ZShow(0.4f,ZShowType.Slide,ZDirection.Down));
        Add("ZHide Fade", "Hide + Fade，结束后隐藏", ()=>panelGroup.ZHide(0.35f,ZShowType.Fade));
        Add("ZButtonPress", "按钮按下回弹", ()=>button.ZButtonPress());
        Add("ZHoverEnter", "悬停放大", ()=>button.ZHoverEnter());
        Add("ZHoverExit", "悬停恢复", ()=>
        {
            button.localScale=Vector3.one*1.05f;
            button.ZHoverExit();
        });
        Add("ZSelect", "选中放大", ()=>button.ZSelect());
        Add("ZDeselect", "取消选中", ()=>
        {
            button.localScale=Vector3.one*1.08f;
            button.ZDeselect();
        });
        Add("ZDisable", "CanvasGroup 禁用变淡", ()=>panelGroup.ZDisable());
        Add("ZEnable", "CanvasGroup 重新启用", ()=>
        {
            panelGroup.alpha=0.5f;
            panelGroup.interactable=false;
            panelGroup.ZEnable();
        });
        Add("ZSetState Hover", "按钮状态：Hover", ()=>button.ZSetState(ZButtonState.Hover));
        Add("ZSetState Pressed", "按钮状态：Pressed", ()=>button.ZSetState(ZButtonState.Pressed));
        Add("ZColor", "Image 变色", ()=>colorTarget.ZColor(new Color(1f,0.45f,0.2f),0.4f));
        Add("ZTextColor", "文字变色", ()=>demoText.ZTextColor(new Color(1f,0.85f,0.2f),0.4f));
        Add("ZFlash", "闪白", ()=>colorTarget.ZFlash(Color.white,0.18f));
        Add("ZShake", "左右抖动", ()=>panel.ZShake());
        Add("ZError", "错误：抖动 + 闪红", ()=>panel.ZError());
        Add("ZWarning", "警告：抖动 + 闪黄", ()=>panel.ZWarning());
        Add("ZSuccess", "成功：Punch + 闪绿", ()=>panel.ZSuccess());
        Add("ZPunchScale", "缩放冲击", ()=>panel.ZPunchScale());
        Add("ZPunchPosition", "位移冲击", ()=>panel.ZPunchPosition());
        Add("ZPunchRotation", "旋转冲击", ()=>panel.ZPunchRotation());
        Add("ZBounce", "弹跳 1 → 1.2 → 0.95 → 1.05 → 1", ()=>panel.ZBounce());
        Add("ZAttention", "引起注意的 Punch", ()=>panel.ZAttention());
        Add("ZNumber", "数字 0 → 500", ()=>
        {
            demoText.text="0";
            demoText.ZNumber(0,500,0.8f);
        });
        Add("ZCurrency", "货币 $100 → $250", ()=>
        {
            demoText.text="$100";
            demoText.ZCurrency(100,250,0.8f);
        });
        Add("ZTypewriter", "打字机 Hello ZTween", ()=>demoText.ZTypewriter("Hello ZTween",0.9f));
        Add("ZReveal", "文字淡入显现", ()=>
        {
            var c=demoText.color;
            c.a=0f;
            demoText.color=c;
            demoText.ZReveal(0.4f);
        });
        Add("ZShakeText", "文字抖动", ()=>demoText.ZShakeText());
        Add("ZPunchText", "文字 Punch", ()=>demoText.ZPunchText());
        Add("ZIconPopup", "图标弹出", ()=>icon.ZIconPopup());
        Add("ZIconRotate", "图标旋转一圈", ()=>icon.ZIconRotate());
        Add("ZIconBounce", "图标弹跳", ()=>icon.ZIconBounce());
        Add("ZIconFloat", "图标上下漂浮（循环，空格停止）", ()=>icon.ZIconFloat(0.7f,18f,-1));
        Add("ZToast", "Toast 进入-停留-退出", ()=>
        {
            toast.gameObject.SetActive(true);
            toast.ZToast(1.1f,ZDirection.Up,0.25f);
        });
        Add("ZNotification", "通知滑入后淡出", ()=>
        {
            toast.gameObject.SetActive(true);
            toastGroup.alpha=1f;
            toast.ZNotification(1.1f,ZDirection.Right,0.25f);
        });
        Add("ZTooltipShow", "Tooltip 显示", ()=>
        {
            tooltip.gameObject.SetActive(true);
            tooltip.ZTooltipShow();
        });
        Add("ZTooltipHide", "Tooltip 隐藏", ()=>
        {
            tooltip.gameObject.SetActive(true);
            tooltip.localScale=Vector3.one;
            tooltipGroup.alpha=1f;
            tooltip.ZTooltipHide();
        });
        Add("ZBadgeShow", "徽章弹出 + Punch", ()=>
        {
            badge.gameObject.SetActive(true);
            badge.ZBadgeShow();
        });
        Add("ZBadgePunch", "徽章 Punch", ()=>
        {
            badge.gameObject.SetActive(true);
            badge.ZBadgePunch();
        });
        Add("ZTabSelect", "Tab 选中", ()=>button.ZTabSelect());
        Add("ZTabDeselect", "Tab 取消选中", ()=>
        {
            button.localScale=Vector3.one*1.08f;
            button.ZTabDeselect();
        });
        Add("ZPageSwitch", "页面 A 左出，B 右进", ()=>
        {
            pageA.gameObject.SetActive(true);
            pageB.gameObject.SetActive(true);
            pageA.ZPageSwitch(pageB,ZDirection.Left);
        });
        Add("ZListShow", "列表依次弹出", ()=>
        {
            foreach(var item in listItems)
            {
                item.gameObject.SetActive(true);
                item.localScale=Vector3.zero;
            }
            listItems.ZListShow();
        });
        Add("ZListHide", "列表反向关闭", ()=>
        {
            foreach(var item in listItems)item.gameObject.SetActive(true);
            listItems.ZListHide();
        });
        Add("ZStagger", "自定义交错 Popup", ()=>
        {
            foreach(var item in listItems)
            {
                item.gameObject.SetActive(true);
                item.localScale=Vector3.zero;
            }
            listItems.ZStagger(x=>x.ZPopup(0.3f),0.08f);
        });
        Add("ZModalShow", "模态：遮罩 + 面板弹出", ()=>
        {
            modalBg.gameObject.SetActive(true);
            modalPanel.gameObject.SetActive(true);
            modalBg.ZModalShow(modalPanel);
        });
        Add("ZModalHide", "模态关闭", ()=>
        {
            modalBg.gameObject.SetActive(true);
            modalPanel.gameObject.SetActive(true);
            modalBg.alpha=0.5f;
            modalBg.ZModalHide(modalPanel);
        });
        Add("ZDialogShow", "对话框弹出", ()=>
        {
            modalPanel.gameObject.SetActive(true);
            modalPanel.ZDialogShow();
        });
        Add("ZDialogHide", "对话框关闭", ()=>
        {
            modalPanel.gameObject.SetActive(true);
            modalPanel.ZDialogHide();
        });
        Add("ZProgress", "进度条 0.15 → 0.85", ()=>bar.ZProgress(0.15f,0.85f,0.7f));
        Add("ZHealthBar", "血条 + 延迟掉血", ()=>bar.ZHealthBar(0.9f,0.4f,0.45f,delayedBar));
        Add("ZSlider", "Slider 滑到 0.8", ()=>
        {
            slider.value=0.15f;
            slider.ZSlider(0.8f,0.5f);
        });
        Add("ZSpinner", "加载转圈（循环，空格停止）", ()=>icon.ZSpinner(0.7f,-1));
        Add("ZLoadingDots", "Loading... 循环点", ()=>
        {
            loadingText.gameObject.SetActive(true);
            loadingText.ZLoadingDots("Loading",0.7f,-1);
        });
        Add("ZLoadingProgress", "加载进度到 1", ()=>bar.ZLoadingProgress(1f,0.7f));
        Add("ZWipe", "从左擦除铺满", ()=>
        {
            wipe.gameObject.SetActive(true);
            wipe.ZWipe(ZDirection.Left,0.55f);
        });
        Add("ZTransitionFadeOut", "转场压黑", ()=>
        {
            transition.gameObject.SetActive(true);
            transition.alpha=0f;
            transition.ZTransitionFadeOut(0.45f);
        });
        Add("ZTransitionFadeIn", "转场揭开", ()=>
        {
            transition.gameObject.SetActive(true);
            transition.alpha=1f;
            transition.ZTransitionFadeIn(0.45f);
        });
        Add("ZCountdown", "倒计时 3 2 1 GO!", ()=>
        {
            countdownText.gameObject.SetActive(true);
            countdownText.ZCountdown(3,0.55f);
        });
        Add("ZTimerBar", "计时条耗尽", ()=>bar.ZTimerBar(1.2f));
        Add("ZCooldown", "冷却遮罩 + 数字 + 完成 Punch", ()=>
        {
            cooldownMask.gameObject.SetActive(true);
            demoText.text="3";
            cooldownMask.ZCooldown(1.4f,demoText,icon);
        });
        Add("ZFloat", "面板上下漂浮（循环）", ()=>panel.ZFloat(0.7f,22f,-1));
        Add("ZPulse", "呼吸缩放（循环）", ()=>panel.ZPulse(0.4f,1.08f,-1));
        Add("ZBlink", "闪烁（循环）", ()=>panelGroup.ZBlink(0.3f,-1));
        Add("ZRotateLoop", "持续旋转（循环）", ()=>icon.ZRotateLoop(0.9f,-1));
        Add("ZHighlight", "高亮自己，压暗旁边", ()=>
        {
            dimA.gameObject.SetActive(true);
            dimB.gameObject.SetActive(true);
            panelGroup.ZHighlight(new[]{dimA,dimB},0.25f,0.3f);
        });
        Add("ZSpotlight", "聚光：更强压暗", ()=>
        {
            dimA.gameObject.SetActive(true);
            dimB.gameObject.SetActive(true);
            panelGroup.ZSpotlight(new[]{dimA,dimB},0.15f,0.3f);
        });
        Add("ZTutorialShow", "教程步骤 + 背景压暗", ()=>
        {
            tutorialStep.gameObject.SetActive(true);
            modalBg.gameObject.SetActive(true);
            tutorialStep.ZTutorialShow(modalBg);
        });
        Add("ZTutorialHide", "教程收起", ()=>
        {
            tutorialStep.gameObject.SetActive(true);
            modalBg.gameObject.SetActive(true);
            modalBg.alpha=0.55f;
            tutorialStep.ZTutorialHide(modalBg);
        });
        Add("ZPointerEnter", "指针进入放大", ()=>card.ZPointerEnter());
        Add("ZPointerClick", "指针点击回弹", ()=>card.ZPointerClick());
        Add("ZCardHover", "卡牌悬停上移", ()=>
        {
            card.gameObject.SetActive(true);
            card.ZCardHover();
        });
        Add("ZCardSelect", "卡牌选中上移旋转", ()=>
        {
            card.gameObject.SetActive(true);
            card.ZCardSelect();
        });
        Add("ZCardPlay", "打出卡牌", ()=>
        {
            card.gameObject.SetActive(true);
            card.ZCardPlay(card.anchoredPosition+new Vector2(0f,220f),0.4f);
        });
        Add("ZCardDiscard", "弃牌缩小淡出", ()=>
        {
            card.gameObject.SetActive(true);
            card.ZCardDiscard();
        });
        Add("ZItemAdd", "物品获得", ()=>icon.ZItemAdd());
        Add("ZItemRemove", "物品移除", ()=>icon.ZItemRemove());
        Add("ZItemSelect", "物品选中", ()=>icon.ZItemSelect());
        Add("ZItemHover", "物品悬停", ()=>icon.ZItemHover());
        Add("ZDragBegin", "拖拽开始放大", ()=>icon.ZDragBegin());
        Add("ZDragEnd", "拖拽结束恢复", ()=>
        {
            icon.localScale=Vector3.one*1.1f;
            icon.ZDragEnd();
        });
        Add("ZDragInvalid", "非法拖拽抖动", ()=>icon.ZDragInvalid());
        Add("ZFocus", "手柄焦点", ()=>button.ZFocus());
        Add("ZUnfocus", "取消焦点", ()=>
        {
            button.localScale=Vector3.one*1.06f;
            button.ZUnfocus();
        });
        Add("ZRewardShow", "奖励：放大旋转 Punch", ()=>icon.ZRewardShow());
        Add("ZLevelUp", "升级文字", ()=>
        {
            demoText.text="LEVEL UP!";
            demoText.ZLevelUp();
        });
        Add("ZAchievementShow", "成就通知滑入滑出", ()=>
        {
            toast.gameObject.SetActive(true);
            toast.ZAchievementShow(1.1f,ZDirection.Right,0.28f);
        });
        Add("ZScrollToIndex", "滚动到第 4 项", ()=>
        {
            scrollRect.gameObject.SetActive(true);
            scrollRect.verticalNormalizedPosition=1f;
            scrollRect.ZScrollToIndex(3,0.45f);
        });
        Add("ZScrollSnap", "吸附到第 6 项", ()=>
        {
            scrollRect.gameObject.SetActive(true);
            scrollRect.ZScrollSnap(5);
        });
    }

    void Add(string name,string desc,Action play)
    {
        steps.Add(new Step{name=name,desc=desc,play=play});
    }

    void BuildUI()
    {
        canvas=FindFirstObjectByType<Canvas>();
        if(canvas==null)
        {
            var canvasGo=new GameObject("Canvas",typeof(RectTransform),typeof(Canvas),typeof(CanvasScaler),typeof(GraphicRaycaster));
            canvas=canvasGo.GetComponent<Canvas>();
            canvas.renderMode=RenderMode.ScreenSpaceOverlay;
            var scaler=canvasGo.GetComponent<CanvasScaler>();
            scaler.uiScaleMode=CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution=new Vector2(1920f,1080f);
        }

        var root=canvas.transform;
        var sprite=WhiteSprite();
        var font=UiFont();

        titleText=EnsureText(root,"DemoTitle",new Vector2(0f,460f),new Vector2(1400f,70f),36,font,"ZTween Demo");
        hintText=EnsureText(root,"DemoHint",new Vector2(0f,-480f),new Vector2(1200f,48f),24,font,"空格：下一个效果");
        hintText.color=new Color(0.8f,0.85f,0.95f,0.9f);

        panel=EnsureImage(root,"DemoPanel",new Vector2(0f,40f),new Vector2(320f,200f),new Color(0.23f,0.32f,0.52f,1f),sprite);
        panelGroup=EnsureGroup(panel.gameObject);
        colorTarget=panel.GetComponent<Image>();

        icon=EnsureImage(root,"DemoIcon",new Vector2(-280f,40f),new Vector2(96f,96f),new Color(0.98f,0.76f,0.22f,1f),sprite);
        button=EnsureImage(root,"DemoButton",new Vector2(280f,40f),new Vector2(160f,72f),new Color(0.28f,0.58f,0.92f,1f),sprite);
        if(button.GetComponent<Button>()==null)button.gameObject.AddComponent<Button>();
        EnsureText(button,"Label",Vector2.zero,new Vector2(160f,72f),22,font,"Button").raycastTarget=false;

        demoText=EnsureText(root,"DemoText",new Vector2(0f,-140f),new Vector2(720f,56f),30,font,"Score 100");
        loadingText=EnsureText(root,"DemoLoading",new Vector2(0f,-200f),new Vector2(480f,40f),26,font,"Loading");
        loadingText.gameObject.SetActive(false);
        countdownText=EnsureText(root,"DemoCountdown",new Vector2(0f,40f),new Vector2(400f,140f),72,font,"3");
        countdownText.gameObject.SetActive(false);

        delayedBar=EnsureImage(root,"DemoDelayedBar",new Vector2(0f,-250f),new Vector2(420f,28f),new Color(0.85f,0.28f,0.28f,1f),sprite).GetComponent<Image>();
        SetupFill(delayedBar);
        bar=EnsureImage(root,"DemoBar",new Vector2(0f,-250f),new Vector2(420f,28f),new Color(0.25f,0.82f,0.42f,1f),sprite).GetComponent<Image>();
        SetupFill(bar);
        bar.fillAmount=0.7f;
        delayedBar.fillAmount=0.7f;

        cooldownMask=EnsureImage(root,"DemoCooldown",new Vector2(-280f,40f),new Vector2(96f,96f),new Color(0f,0f,0f,0.55f),sprite).GetComponent<Image>();
        SetupFill(cooldownMask);
        cooldownMask.fillMethod=Image.FillMethod.Radial360;
        cooldownMask.fillAmount=0f;
        cooldownMask.gameObject.SetActive(false);

        slider=EnsureSlider(root,sprite);

        toast=EnsureImage(root,"DemoToast",new Vector2(0f,280f),new Vector2(360f,80f),new Color(0.12f,0.14f,0.2f,0.95f),sprite);
        toastGroup=EnsureGroup(toast.gameObject);
        EnsureText(toast,"Label",Vector2.zero,new Vector2(360f,80f),22,font,"获得金币！").raycastTarget=false;
        toast.gameObject.SetActive(false);

        tooltip=EnsureImage(root,"DemoTooltip",new Vector2(280f,140f),new Vector2(200f,56f),new Color(0.08f,0.08f,0.1f,0.92f),sprite);
        tooltipGroup=EnsureGroup(tooltip.gameObject);
        EnsureText(tooltip,"Label",Vector2.zero,new Vector2(200f,56f),20,font,"Tooltip").raycastTarget=false;
        tooltip.gameObject.SetActive(false);

        badge=EnsureImage(root,"DemoBadge",new Vector2(360f,90f),new Vector2(44f,44f),new Color(0.92f,0.22f,0.28f,1f),sprite);
        EnsureText(badge,"Label",Vector2.zero,new Vector2(44f,44f),20,font,"3").raycastTarget=false;
        badge.gameObject.SetActive(false);

        pageA=EnsureImage(root,"DemoPageA",new Vector2(-520f,-40f),new Vector2(180f,120f),new Color(0.45f,0.3f,0.62f,1f),sprite);
        pageB=EnsureImage(root,"DemoPageB",new Vector2(-520f,-40f),new Vector2(180f,120f),new Color(0.22f,0.55f,0.5f,1f),sprite);
        EnsureText(pageA,"Label",Vector2.zero,new Vector2(180f,120f),22,font,"Page A").raycastTarget=false;
        EnsureText(pageB,"Label",Vector2.zero,new Vector2(180f,120f),22,font,"Page B").raycastTarget=false;
        pageA.gameObject.SetActive(false);
        pageB.gameObject.SetActive(false);

        listItems.Clear();
        for(int i=0;i<4;i++)
        {
            var item=EnsureImage(root,"DemoItem"+(i+1),new Vector2(520f,80f-i*70f),new Vector2(160f,56f),new Color(0.32f,0.4f,0.58f,1f),sprite);
            EnsureText(item,"Label",Vector2.zero,new Vector2(160f,56f),20,font,"Item "+(i+1)).raycastTarget=false;
            item.gameObject.SetActive(false);
            listItems.Add(item);
        }

        modalBg=EnsureGroup(EnsureImage(root,"DemoModalBg",Vector2.zero,new Vector2(2400f,1600f),new Color(0f,0f,0f,0.55f),sprite).gameObject);
        modalPanel=EnsureImage(root,"DemoModalPanel",Vector2.zero,new Vector2(380f,220f),new Color(0.18f,0.22f,0.34f,1f),sprite);
        EnsureText(modalPanel,"Label",Vector2.zero,new Vector2(380f,220f),26,font,"确认删除？").raycastTarget=false;
        modalBg.gameObject.SetActive(false);
        modalPanel.gameObject.SetActive(false);

        tutorialStep=EnsureGroup(EnsureImage(root,"DemoTutorial",new Vector2(0f,180f),new Vector2(420f,90f),new Color(0.95f,0.92f,0.75f,1f),sprite).gameObject);
        EnsureText(tutorialStep.transform,"Label",Vector2.zero,new Vector2(420f,90f),22,font,"点击这个按钮").raycastTarget=false;
        var tutorialLabel=tutorialStep.transform.Find("Label");
        if(tutorialLabel!=null)
        {
            var t=tutorialLabel.GetComponent<Text>();
            if(t!=null)t.color=new Color(0.15f,0.12f,0.08f);
        }
        tutorialStep.gameObject.SetActive(false);

        card=EnsureImage(root,"DemoCard",new Vector2(0f,-360f),new Vector2(130f,180f),new Color(0.78f,0.36f,0.42f,1f),sprite);
        EnsureText(card,"Label",Vector2.zero,new Vector2(130f,180f),22,font,"Card").raycastTarget=false;
        if(card.GetComponent<CanvasGroup>()==null)card.gameObject.AddComponent<CanvasGroup>();
        card.gameObject.SetActive(true);

        dimA=EnsureGroup(EnsureImage(root,"DemoDimA",new Vector2(-280f,-140f),new Vector2(88f,88f),new Color(0.4f,0.4f,0.45f,1f),sprite).gameObject);
        dimB=EnsureGroup(EnsureImage(root,"DemoDimB",new Vector2(280f,-140f),new Vector2(88f,88f),new Color(0.4f,0.4f,0.45f,1f),sprite).gameObject);

        wipe=EnsureImage(root,"DemoWipe",Vector2.zero,new Vector2(2400f,1600f),new Color(0.08f,0.1f,0.16f,0.92f),sprite).GetComponent<Image>();
        SetupFill(wipe);
        wipe.gameObject.SetActive(false);

        transition=EnsureGroup(EnsureImage(root,"DemoTransition",Vector2.zero,new Vector2(2400f,1600f),new Color(0f,0f,0f,1f),sprite).gameObject);
        transition.alpha=0f;
        transition.gameObject.SetActive(false);

        scrollRect=EnsureScroll(root,sprite,font);
        scrollRect.gameObject.SetActive(false);

        var existing=root.Find("Image");
        if(existing!=null)existing.gameObject.SetActive(false);
    }

    void CaptureSnapshots()
    {
        snapshots.Clear();
        foreach(var rt in canvas.GetComponentsInChildren<RectTransform>(true))
        {
            if(rt==canvas.transform)continue;
            snapshots.Add(Snapshot.From(rt));
        }
    }

    void Restore()
    {
        for(int i=0;i<snapshots.Count;i++)snapshots[i].Apply();
    }

    static Sprite whiteSprite;
    static Sprite WhiteSprite()
    {
        if(whiteSprite!=null)return whiteSprite;
        var tex=Texture2D.whiteTexture;
        whiteSprite=Sprite.Create(tex,new Rect(0f,0f,tex.width,tex.height),new Vector2(0.5f,0.5f),100f);
        return whiteSprite;
    }

    static Font UiFont()
    {
        var font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        if(font==null)font=Font.CreateDynamicFontFromOSFont("Arial",24);
        return font;
    }

    static CanvasGroup EnsureGroup(GameObject go)
    {
        var g=go.GetComponent<CanvasGroup>();
        if(g==null)g=go.AddComponent<CanvasGroup>();
        return g;
    }

    static void SetupFill(Image image)
    {
        image.type=Image.Type.Filled;
        image.fillMethod=Image.FillMethod.Horizontal;
        image.fillOrigin=0;
        image.fillAmount=1f;
    }

    RectTransform EnsureImage(Transform parent,string name,Vector2 pos,Vector2 size,Color color,Sprite sprite)
    {
        var t=parent.Find(name);
        GameObject go;
        if(t==null)
        {
            go=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image));
            go.transform.SetParent(parent,false);
        }
        else go=t.gameObject;

        var rt=(RectTransform)go.transform;
        rt.anchorMin=rt.anchorMax=rt.pivot=new Vector2(0.5f,0.5f);
        rt.anchoredPosition=pos;
        rt.sizeDelta=size;
        rt.localScale=Vector3.one;
        rt.localEulerAngles=Vector3.zero;
        var image=go.GetComponent<Image>();
        if(image==null)image=go.AddComponent<Image>();
        image.sprite=sprite;
        image.color=color;
        image.raycastTarget=false;
        go.SetActive(true);
        return rt;
    }

    Text EnsureText(Transform parent,string name,Vector2 pos,Vector2 size,int fontSize,Font font,string content)
    {
        var t=parent.Find(name);
        GameObject go;
        if(t==null)
        {
            go=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Text));
            go.transform.SetParent(parent,false);
        }
        else go=t.gameObject;

        var rt=(RectTransform)go.transform;
        rt.anchorMin=rt.anchorMax=rt.pivot=new Vector2(0.5f,0.5f);
        rt.anchoredPosition=pos;
        rt.sizeDelta=size;
        var text=go.GetComponent<Text>();
        if(text==null)text=go.AddComponent<Text>();
        text.font=font;
        text.fontSize=fontSize;
        text.alignment=TextAnchor.MiddleCenter;
        text.color=Color.white;
        text.text=content;
        text.raycastTarget=false;
        go.SetActive(true);
        return text;
    }

    Slider EnsureSlider(Transform parent,Sprite sprite)
    {
        var root=EnsureImage(parent,"DemoSlider",new Vector2(0f,-300f),new Vector2(420f,24f),new Color(0.15f,0.18f,0.24f,1f),sprite);
        var slider=root.GetComponent<Slider>();
        if(slider==null)slider=root.gameObject.AddComponent<Slider>();
        var fill=EnsureImage(root,"Fill",Vector2.zero,new Vector2(420f,24f),new Color(0.35f,0.7f,1f,1f),sprite);
        fill.anchorMin=new Vector2(0f,0f);
        fill.anchorMax=new Vector2(1f,1f);
        fill.offsetMin=fill.offsetMax=Vector2.zero;
        slider.fillRect=fill;
        slider.minValue=0f;
        slider.maxValue=1f;
        slider.value=0.3f;
        slider.interactable=false;
        return slider;
    }

    ScrollRect EnsureScroll(Transform parent,Sprite sprite,Font font)
    {
        var root=EnsureImage(parent,"DemoScroll",new Vector2(620f,40f),new Vector2(180f,260f),new Color(0.1f,0.12f,0.16f,0.9f),sprite);
        var scroll=root.GetComponent<ScrollRect>();
        if(scroll==null)scroll=root.gameObject.AddComponent<ScrollRect>();
        var viewport=EnsureImage(root,"Viewport",Vector2.zero,new Vector2(180f,260f),new Color(1f,1f,1f,0.02f),sprite);
        viewport.anchorMin=Vector2.zero;
        viewport.anchorMax=Vector2.one;
        viewport.offsetMin=viewport.offsetMax=Vector2.zero;
        var mask=viewport.GetComponent<Mask>();
        if(mask==null)mask=viewport.gameObject.AddComponent<Mask>();
        mask.showMaskGraphic=false;

        var existing=viewport.Find("Content");
        RectTransform content;
        if(existing==null)
        {
            var go=new GameObject("Content",typeof(RectTransform));
            go.transform.SetParent(viewport,false);
            content=(RectTransform)go.transform;
        }
        else content=(RectTransform)existing;
        content.anchorMin=new Vector2(0f,1f);
        content.anchorMax=new Vector2(1f,1f);
        content.pivot=new Vector2(0.5f,1f);
        content.anchoredPosition=Vector2.zero;
        content.sizeDelta=new Vector2(0f,560f);
        for(int i=0;i<8;i++)
        {
            var item=EnsureImage(content,"ScrollItem"+(i+1),new Vector2(0f,-28f-i*68f),new Vector2(160f,56f),new Color(0.3f,0.36f,0.5f,1f),sprite);
            EnsureText(item,"Label",Vector2.zero,new Vector2(160f,56f),20,font,"Scroll "+(i+1)).raycastTarget=false;
        }
        scroll.viewport=viewport;
        scroll.content=content;
        scroll.horizontal=false;
        scroll.vertical=true;
        scroll.movementType=ScrollRect.MovementType.Clamped;
        return scroll;
    }

    class Step
    {
        public string name;
        public string desc;
        public Action play;
    }

    struct Snapshot
    {
        public RectTransform rt;
        public Vector2 pos;
        public Vector3 scale;
        public Vector3 euler;
        public bool active;
        public float alpha;
        public bool hasGroup;
        public Color color;
        public bool hasGraphic;
        public string text;
        public bool hasText;
        public float fill;
        public bool hasImage;
        public float sliderValue;
        public bool hasSlider;

        public static Snapshot From(RectTransform rt)
        {
            var s=new Snapshot
            {
                rt=rt,
                pos=rt.anchoredPosition,
                scale=rt.localScale,
                euler=rt.localEulerAngles,
                active=rt.gameObject.activeSelf
            };
            var g=rt.GetComponent<CanvasGroup>();
            if(g!=null)
            {
                s.hasGroup=true;
                s.alpha=g.alpha;
            }
            var graphic=rt.GetComponent<Graphic>();
            if(graphic!=null)
            {
                s.hasGraphic=true;
                s.color=graphic.color;
            }
            var text=rt.GetComponent<Text>();
            if(text!=null)
            {
                s.hasText=true;
                s.text=text.text;
            }
            var image=rt.GetComponent<Image>();
            if(image!=null)
            {
                s.hasImage=true;
                s.fill=image.fillAmount;
            }
            var slider=rt.GetComponent<Slider>();
            if(slider!=null)
            {
                s.hasSlider=true;
                s.sliderValue=slider.value;
            }
            return s;
        }

        public void Apply()
        {
            if(rt==null)return;
            rt.gameObject.SetActive(active);
            rt.anchoredPosition=pos;
            rt.localScale=scale;
            rt.localEulerAngles=euler;
            if(hasGroup)
            {
                var g=rt.GetComponent<CanvasGroup>();
                if(g!=null)
                {
                    g.alpha=alpha;
                    g.interactable=true;
                    g.blocksRaycasts=true;
                }
            }
            if(hasGraphic)
            {
                var graphic=rt.GetComponent<Graphic>();
                if(graphic!=null)graphic.color=color;
            }
            if(hasText)
            {
                var uiText=rt.GetComponent<Text>();
                if(uiText!=null)uiText.text=text;
            }
            if(hasImage)
            {
                var image=rt.GetComponent<Image>();
                if(image!=null)image.fillAmount=fill;
            }
            if(hasSlider)
            {
                var slider=rt.GetComponent<Slider>();
                if(slider!=null)slider.value=sliderValue;
            }
        }
    }
}
