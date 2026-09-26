using System;
using System.Collections.Generic;
using AChen.Log;
using UnityEngine;
namespace Common.Timer
{
    public static class TimerUtil
    {
        private static Dictionary<string,Timer> _timers=new();
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
        public static bool HasTimer(string name)
        {
            return _timers.ContainsKey(name);
        }
        public static void PauseTimer(string name)
        {
            if (!HasTimer(name))
            {
                ALog.LogError("没有这个计时器对象: "+name);
                return;
            }
            _timers[name].PauseTimer();
            
        }
        public static void ResetTimer(string name)
        {
            if (!HasTimer(name))
            {
                ALog.LogError("没有这个计时器对象: "+name);
                return;
            }
            _timers[name].ResetTimer();
        }
        public static void ReStartTimer(string name)
        {
            if (!HasTimer(name))
            {
                ALog.LogError("没有这个计时器对象: "+name);
                return;
            }
            _timers[name].ReStartTimer();            
        }
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
        public static ETimerState GetTimerState(string name)
        {
            if (!HasTimer(name))
            {
                ALog.LogError("没有这个计时器对象: "+name);
            }
            return ETimerState.None;
        }
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
 