# -*- coding: utf-8 -*-
from pathlib import Path

p = Path(r"Assets/Scripts/Common/ZTween/UITest.cs")
lines = p.read_text(encoding="utf-8", errors="replace").splitlines(True)
out = []
for line in lines:
    if 'EnsureText(root,"DemoHint"' in line:
        line = '        hintText=EnsureText(root,"DemoHint",new Vector2(0f,-480f),new Vector2(1200f,48f),24,font,"空格：下一个效果");\n'
    elif 'EnsureText(toast,"Label"' in line:
        line = '        EnsureText(toast,"Label",Vector2.zero,new Vector2(360f,80f),22,font,"获得金币！").raycastTarget=false;\n'
    elif 'EnsureText(modalPanel,"Label"' in line:
        line = '        EnsureText(modalPanel,"Label",Vector2.zero,new Vector2(380f,220f),26,font,"确认删除？").raycastTarget=false;\n'
    elif 'EnsureText(tutorialStep.transform,"Label"' in line:
        line = '        EnsureText(tutorialStep.transform,"Label",Vector2.zero,new Vector2(420f,90f),22,font,"点击这个按钮").raycastTarget=false;\n'
    out.append(line)
p.write_text("".join(out), encoding="utf-8")
print("ok")
