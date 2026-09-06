using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.ComponentModel.Design;


namespace ZZ.ZTween
{
    public class ZTween
    {
        public Tween tween{get;}            //内部Tween对象
        public bool active=>tween.active;   //对应内部Tween对象的active
        public ZTween(Tween tween)
        {
            this.tween=tween;
        }

    }
}

