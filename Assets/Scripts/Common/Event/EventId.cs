using System;

namespace AChen.Events
{
    /// <summary>无参数事件。</summary>
    public readonly struct EventId
    {
        /// <summary>用于日志和运行时签名检查的唯一事件名。</summary>
        public readonly string Name;

        /// <summary>创建无参数事件标识。业务事件应集中声明在 <see cref="GameEvent"/>。</summary>
        /// <param name="name">非空的唯一事件名，建议使用“模块.事件”格式。</param>
        public EventId(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Event name cannot be empty.", nameof(name));
            }

            Name = name;
        }

        /// <summary>该事件要求的监听器委托类型。</summary>
        public Type HandlerType => typeof(Action);
    }

    /// <summary>一个强类型参数的事件。</summary>
    public readonly struct EventId<T>
    {
        /// <summary>用于日志和运行时签名检查的唯一事件名。</summary>
        public readonly string Name;

        /// <summary>创建单参数事件标识。业务事件应集中声明在 <see cref="GameEvent"/>。</summary>
        /// <param name="name">非空的唯一事件名。</param>
        public EventId(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Event name cannot be empty.", nameof(name));
            }

            Name = name;
        }

        /// <summary>该事件要求的监听器委托类型。</summary>
        public Type HandlerType => typeof(Action<T>);
    }

    /// <summary>两个强类型参数的事件。</summary>
    public readonly struct EventId<T1, T2>
    {
        /// <summary>用于日志和运行时签名检查的唯一事件名。</summary>
        public readonly string Name;

        /// <summary>创建双参数事件标识。业务事件应集中声明在 <see cref="GameEvent"/>。</summary>
        /// <param name="name">非空的唯一事件名。</param>
        public EventId(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Event name cannot be empty.", nameof(name));
            }

            Name = name;
        }

        /// <summary>该事件要求的监听器委托类型。</summary>
        public Type HandlerType => typeof(Action<T1, T2>);
    }
}
