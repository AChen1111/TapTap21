using System;

namespace AChen.Events
{
    /// <summary>无参数事件。</summary>
    public readonly struct EventId
    {
        public readonly string Name;

        public EventId(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Event name cannot be empty.", nameof(name));
            }

            Name = name;
        }

        public Type HandlerType => typeof(Action);
    }

    /// <summary>一个强类型参数的事件。</summary>
    public readonly struct EventId<T>
    {
        public readonly string Name;

        public EventId(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Event name cannot be empty.", nameof(name));
            }

            Name = name;
        }

        public Type HandlerType => typeof(Action<T>);
    }

    /// <summary>两个强类型参数的事件。</summary>
    public readonly struct EventId<T1, T2>
    {
        public readonly string Name;

        public EventId(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Event name cannot be empty.", nameof(name));
            }

            Name = name;
        }

        public Type HandlerType => typeof(Action<T1, T2>);
    }
}
