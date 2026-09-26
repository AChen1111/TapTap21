using System;
using System.Collections.Generic;
using AChen.Log;
using UnityEngine;
namespace Common.Timer
{
    public static class TimerUtil
    {
        private static Dictionary<string,Timer> _timers=new();

        /// <summary>
        /// 按名字启动倒计时。名字不存在时从对象池取出并记入字典；已存在时复用该实例。
        /// 会换成这次的回调。已走过的时间不会清零，要从 0 开始请先 <see cref="ResetTimer"/>。
        /// </summary>
        /// <param name="name">计时器名字。</param>
        /// <param name="time">倒计时秒数，按 <c>Time.deltaTime</c> 累加。</param>
        /// <param name="OnComplete">到点执行一次。传 null 表示清掉原回调。</param>
        public static void StartTimer(string name,float time,Action OnComplete=null)
        {
            Timer timer;
            if (!HasTimer(name))
            {
                timer=TimerPool.Instance.Get();
                _timers.Add(name,timer);
            }
            else
            {
                timer=_timers[name];
            }
            timer.OnComplete=OnComplete;
            
            timer.StartTimer(time);
        }
        /// <summary>字典里是否还有这个名字。已 <see cref="DeleteTimer"/> 的名字返回 false。</summary>
        /// <param name="name">计时器名字。</param>
        public static bool HasTimer(string name)
        {
            return _timers.ContainsKey(name);
        }
        /// <summary>暂停计时，保留已走过的时间。名字不存在时打错误日志并返回。</summary>
        /// <param name="name">计时器名字。</param>
        public static void PauseTimer(string name)
        {
            if (!HasTimer(name))
            {
                ALog.LogError("没有这个计时器对象: "+name);
                return;
            }
            _timers[name].PauseTimer();
            
        }
        /// <summary>已走时间清零，状态回到 Ready，不会继续走到回调。名字不存在时打错误日志并返回。</summary>
        /// <param name="name">计时器名字。</param>
        public static void ResetTimer(string name)
        {
            if (!HasTimer(name))
            {
                ALog.LogError("没有这个计时器对象: "+name);
                return;
            }
            _timers[name].ResetTimer();
        }
        /// <summary>先重置，再用当前总时长重新开始。不修改秒数和回调。名字不存在时打错误日志并返回。</summary>
        /// <param name="name">计时器名字。</param>
        public static void ReStartTimer(string name)
        {
            if (!HasTimer(name))
            {
                ALog.LogError("没有这个计时器对象: "+name);
                return;
            }
            _timers[name].ReStartTimer();            
        }
        /// <summary>把该实例归还对象池，并从字典删除。名字不存在时打错误日志并返回。</summary>
        /// <param name="name">计时器名字。</param>
        public static void DeleteTimer(string name)
        {
            if (!HasTimer(name))
            {
                ALog.LogError("没有这个计时器对象: "+name);
                return;
            }
            TimerPool.Instance.Release(_timers[name]);
            _timers.Remove(name);
        }
        /// <summary>
        /// 查询计时器状态。名字不存在时打错误日志。
        /// 当前实现无论名字是否存在都返回 <see cref="ETimerState.None"/>。
        /// </summary>
        /// <param name="name">计时器名字。</param>
        public static ETimerState GetTimerState(string name)
        {
            if (!HasTimer(name))
            {
                ALog.LogError("没有这个计时器对象: "+name);
            }
            return ETimerState.None;
        }
        /// <summary>已走过的秒数。名字不存在时打错误日志并返回 -1。到点重置后会变成 0。</summary>
        /// <param name="name">计时器名字。</param>
        /// <returns>已走秒数；名字不存在时为 -1。</returns>
        public static float GetTimerElapsed(string name)
        {
            if (!HasTimer(name))
            {
                ALog.LogError("没有这个计时器对象: "+name);
                return -1;
            }
            return _timers[name].Elapsed;
        }
    }

}
 