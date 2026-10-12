using GamePlay.Core;
using UnityEngine;

namespace GamePlay.Scene
{
    /// <summary>
    /// 可以被铲子回收至物品栏
    /// </summary>
    public interface ICanRecycle
    {
        public Item OnRecycle();
    }

}
