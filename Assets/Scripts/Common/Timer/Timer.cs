using UnityEngine;
using System.Diagnostics;
using AChen.Pooling;
using TMPro.EditorUtilities;
using AChen.Log;
using System;
namespace Common.Timer
{
    public class Timer : MonoBehaviour, IPoolable
    {
        float _totalTime=1;
        float _curTime=0;
        ETimerState _state;
        public float TotalTime
        {
            get => _totalTime; 
            set {
                if(value>0)
                _totalTime=value;
                else
                {
                    ALog.LogError("不可将负值赋给计时器!");
                }
            }
        }
        public ETimerState State=>_state;
        public float Elapsed=>_curTime;
        public Action OnComplete;
        void Update()
        {
            if (_state != ETimerState.Started)return;
            
            _curTime+=Time.deltaTime;
            if (_curTime >= TotalTime)
            {
                OnComplete?.Invoke();
                ResetTimer();
            }
        }
        public void PauseTimer()
        {
            _state=ETimerState.Paused;
        }
        public void StartTimer(float time)
        {
            TotalTime=time;
            _state=ETimerState.Started;
        }
        public void ResetTimer()
        {
            _state=ETimerState.Ready;
            _curTime=0;
        }
        public void ReStartTimer()
        {
            ResetTimer();
            StartTimer(_totalTime);
        }
        public void OnReturnedToPool()
        {
            ResetTimer();
        }

        public void OnTakenFromPool()
        {
            ResetTimer();
        }
    }

}
 