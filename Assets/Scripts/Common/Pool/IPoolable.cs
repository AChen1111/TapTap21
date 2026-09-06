namespace AChen.Pooling
{
    /// <summary>由通用对象池管理的 Prefab 根组件。</summary>
    public interface IPoolable
    {
        /// <summary>实例被对象池取出并激活后调用。用于重置速度、计时器和临时状态。</summary>
        void OnTakenFromPool();

        /// <summary>实例归还对象池、禁用前调用。用于解绑事件、停止特效和清理引用。</summary>
        void OnReturnedToPool();
    }
}
