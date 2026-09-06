using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace AChen.Events
{
    /// <summary>进程内事件中心。事件名使用 <see cref="GameEvent"/> 中定义的字符串常量。</summary>
    public static class EventCenter
    {
        static readonly Dictionary<string, Delegate> s_listeners = new();

        public static void AddListener(string eventName, Action listener) => Add(eventName, listener);
        public static void AddListener<T>(string eventName, Action<T> listener) => Add(eventName, listener);
        public static void AddListener<T1, T2>(string eventName, Action<T1, T2> listener) => Add(eventName, listener);

        public static void RemoveListener(string eventName, Action listener) => Remove(eventName, listener);
        public static void RemoveListener<T>(string eventName, Action<T> listener) => Remove(eventName, listener);
        public static void RemoveListener<T1, T2>(string eventName, Action<T1, T2> listener) => Remove(eventName, listener);

        /// <summary>发布一个无参数事件。</summary>
        public static void Dispatch(string eventName)
        {
            GetPublisher(out string publisher, out string triggerFunction);
            Invoke<Action>(eventName, publisher, triggerFunction, listener => listener());
        }

        /// <summary>发布一个带一个强类型参数的事件。</summary>
        public static void Dispatch<T>(string eventName, T arg)
        {
            GetPublisher(out string publisher, out string triggerFunction);
            Invoke<Action<T>>(eventName, publisher, triggerFunction, listener => listener(arg));
        }

        /// <summary>发布一个带两个强类型参数的事件。</summary>
        public static void Dispatch<T1, T2>(string eventName, T1 arg1, T2 arg2)
        {
            GetPublisher(out string publisher, out string triggerFunction);
            Invoke<Action<T1, T2>>(eventName, publisher, triggerFunction, listener => listener(arg1, arg2));
        }

        static void Add(string eventName, Delegate listener)
        {
            Validate(eventName, listener);
            if (s_listeners.TryGetValue(eventName, out Delegate existing))
            {
                EnsureSameSignature(eventName, existing, listener);
                s_listeners[eventName] = Delegate.Combine(existing, listener);
                Log("Subscribe", eventName, DescribeListener(listener));
                return;
            }

            s_listeners.Add(eventName, listener);
            Log("Subscribe", eventName, DescribeListener(listener));
        }

        static void Remove(string eventName, Delegate listener)
        {
            Validate(eventName, listener);
            if (!s_listeners.TryGetValue(eventName, out Delegate existing))
            {
                return;
            }

            EnsureSameSignature(eventName, existing, listener);
            Delegate remaining = Delegate.Remove(existing, listener);
            if (remaining == null)
            {
                s_listeners.Remove(eventName);
            }
            else
            {
                s_listeners[eventName] = remaining;
            }

            Log("Unsubscribe", eventName, DescribeListener(listener));
        }

        static void Invoke<TDelegate>(
            string eventName,
            string publisher,
            string triggerFunction,
            Action<TDelegate> invoke)
            where TDelegate : Delegate
        {
            if (!s_listeners.TryGetValue(eventName, out Delegate listeners))
            {
                LogDispatch(eventName, publisher, triggerFunction, 0);
                return;
            }

            if (listeners is not TDelegate typedListeners)
            {
                throw new InvalidOperationException(
                    $"Event '{eventName}' was dispatched with parameters that differ from its listeners.");
            }

            Delegate[] subscribers = typedListeners.GetInvocationList();
            LogDispatch(eventName, publisher, triggerFunction, subscribers.Length);
            foreach (Delegate listener in subscribers)
            {
                Log(
                    "Invoke",
                    eventName,
                    $"Publisher={publisher}; Trigger={triggerFunction}; {DescribeListener(listener)}");
                invoke((TDelegate)listener);
            }
        }

        static void GetPublisher(out string publisher, out string triggerFunction)
        {
            if (!ALog.Enabled)
            {
                publisher = "Disabled";
                triggerFunction = "Disabled";
                return;
            }

            MethodBase method = new StackTrace(2, false).GetFrame(0)?.GetMethod();
            string typeName = method?.DeclaringType?.FullName ?? "Unknown";
            string methodName = method?.Name ?? "Unknown";
            publisher = typeName;
            triggerFunction = typeName + "." + methodName;
        }

        static string DescribeListener(Delegate listener)
        {
            string subscriber = listener.Target?.GetType().FullName ??
                                listener.Method.DeclaringType?.FullName ??
                                "Unknown";
            return $"Subscriber={subscriber}; Handler={listener.Method.Name}";
        }

        static void LogDispatch(string eventName, string publisher, string triggerFunction, int subscriberCount) =>
            Log(
                "Dispatch",
                eventName,
                $"Publisher={publisher}; Trigger={triggerFunction}; Subscribers={subscriberCount}");

        static void Log(string action, string eventName, string detail)
        {
            if (ALog.Enabled)
            {
                ALog.Log($"[{action}] Event={eventName}; {detail}", ALogCategories.Event);
            }
        }

        static void Validate(string eventName, Delegate listener)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException("Event name cannot be empty.", nameof(eventName));
            }

            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }
        }

        static void EnsureSameSignature(string eventName, Delegate existing, Delegate listener)
        {
            if (existing.GetType() != listener.GetType())
            {
                throw new InvalidOperationException(
                    $"Event '{eventName}' is already registered with a different listener signature.");
            }
        }
    }
}
