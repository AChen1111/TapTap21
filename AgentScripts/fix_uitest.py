# -*- coding: utf-8 -*-
from pathlib import Path

p = Path(r"Assets/Scripts/Common/ZTween/UITest.cs")
text = p.read_text(encoding="utf-8", errors="replace")
start = text.index("    void Next()")
end = text.index("    void Add(string name,string desc,Action play)")
middle = r'''    void Next()
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

'''
rest = text[end:]
# fix leftover garbled UI strings in BuildUI
replacements = {
    'font,"ç©ºæ ¼ï¼šä¸‹ä¸€ä¸ªæ•ˆæž?);': 'font,"空格：下一个效果");',
    'font,"èŽ·å¾—é‡‘å¸ï¼?);': 'font,"获得金币！").raycastTarget=false;',
}
# do targeted line fixes after splice
out = text[:start] + middle + rest
out = out.replace('font,"ç©ºæ\xa0¼ï¼šä¸‹ä¸€ä¸ªæ•ˆæž?);', 'font,"空格：下一个效果");')
p.write_text(out, encoding="utf-8")
print("rewrote", p, "chars", len(out))
