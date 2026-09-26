using AChen.Pooling;
using Common.Singleton;
using UnityEngine;
using Common.Timer;
public class TimerPool : Singleton<TimerPool>
{
    [SerializeField]private Timer _prefab;
    /// <summary>
    /// 预热数量
    /// </summary>
    [SerializeField,Range(1,100)]private int _warmCount;
    /// <summary>
    /// 自动预热
    /// </summary>
    [SerializeField]private bool _isAutoWarm=true;
    GameObjectPool _pool;
    void Awake()
    {
        _pool=GameObjectPool.Instance;
        if(_isAutoWarm)Warm();
    }
    public Timer Get()
    {
        return _pool.Get(_prefab);
    }
    public void Release(Timer timer)
    {
        _pool.Release(timer);
    }
    public void Warm()
    {
        Timer[] timers=new Timer[_warmCount];

        for(int i = 0; i < _warmCount; ++i)
        {
            timers[i]=Get();
        }
        
        for(int i = 0; i < _warmCount; ++i)
        {
            Release(timers[i]);
        }
    }
    

}
