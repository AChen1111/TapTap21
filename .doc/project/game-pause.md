# 游戏内暂停、设置和退出

`UI.Manager.GamePausePanel` 由 UIManager 在创建游戏 UI 时注册到现有 UIFrame，名称 GamePausePanel，只创建一次并随 PersistentScene 保留。

- 游戏内 Esc：打开暂停菜单；再次按 Esc 直接恢复游戏。
- 右上角设置：暂停并打开设置。
- 右上角退出：暂停并显示退出确认，取消返回暂停菜单。
- 继续游戏：隐藏遮罩并恢复原来的 Time.timeScale 和 PlayerInput.enabled。
- 确认退出：派发现有 OnGameExitBtnClicked，打包版本退出程序，Unity 编辑器停止 Play Mode。

设置提供总音量、音乐、音效、语音、环境、界面六个滑块，通过现有 AudioSettingsService 调整，沿用原服务的 PlayerPrefs 保存与 Mixer 应用。全屏 Toggle 调整当前 Screen.fullScreen。全屏选项当前不单独持久化。

过场加载期间不能打开暂停菜单，避免干扰切图。暂停遮罩阻挡点击，角色 PlayerInput 停用且跳跃／交互增加暂停检查，防止 Time.timeScale=0 时仍触发 F 切图或跳跃。暂停不会停止背景音乐。

界面目前由 GamePausePanel.Awake 生成；工具栏和面板的锚点、尺寸、颜色与文案在该脚本中定义，字体优先使用本机 Microsoft YaHei。使用现有 EventSystem 和 UIFrame，不额外生成重复的 EventSystem 或游戏 Canvas。

已在 Unity 中验证暂停时间、输入禁用、恢复、六个滑块生成、退出确认面板和 Esc 打开。未执行打包后关闭程序的验证。
