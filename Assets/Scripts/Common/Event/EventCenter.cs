using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AChen.Log;
using UnityEngine;

namespace AChen.Events
{
    /// <summary>进程内事件中心。事件用 <see cref="GameEvent"/> 里声明的 <see cref="EventId"/>，类型跟事件走。</summary>
    public static class EventCenter
    {
        static readonly Dictionary<string, Delegate> s_listeners = new Dictionary<string, Delegate>();
        static readonly Dictionary<string, Type> s_signatures = new Dictionary<string, Type>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetState()
        {
            s_listeners.Clear();
            s_signatures.Clear();
        }

        /// <summary>订阅无参数事件。通常在 <c>OnEnable</c> 中调用。</summary>
        /// <param name="evt">在 <see cref="GameEvent"/> 中定义的事件。</param>
        /// <param name="listener">事件触发时同步执行的回调。</param>
        /// <exception cref="ArgumentNullException"><paramref name="listener"/> 为空。</exception>
        public static void AddListener(EventId evt, Action listener) =>
            Add(evt.Name, evt.HandlerType, listener);

        /// <summary>订阅带一个强类型参数的事件。参数类型由事件定义推断。</summary>
        /// <typeparam name="T">事件参数类型。</typeparam>
        /// <param name="evt">在 <see cref="GameEvent"/> 中定义的事件。</param>
        /// <param name="listener">事件触发时同步执行的回调。</param>
        /// <exception cref="ArgumentNullException"><paramref name="listener"/> 为空。</exception>
        public static void AddListener<T>(EventId<T> evt, Action<T> listener) =>
            Add(evt.Name, evt.HandlerType, listener);

        /// <summary>订阅带两个强类型参数的事件。参数类型由事件定义推断。</summary>
        /// <typeparam name="T1">第一个事件参数类型。</typeparam>
        /// <typeparam name="T2">第二个事件参数类型。</typeparam>
        /// <param name="evt">在 <see cref="GameEvent"/> 中定义的事件。</param>
        /// <param name="listener">事件触发时同步执行的回调。</param>
        /// <exception cref="ArgumentNullException"><paramref name="listener"/> 为空。</exception>
        public static void AddListener<T1, T2>(EventId<T1, T2> evt, Action<T1, T2> listener) =>
            Add(evt.Name, evt.HandlerType, listener);

        /// <summary>取消无参数事件订阅。应与 <see cref="AddListener(EventId,Action)"/> 成对使用。</summary>
        /// <param name="evt">订阅时使用的事件。</param>
        /// <param name="listener">订阅时使用的同一个回调实例。</param>
        public static void RemoveListener(EventId evt, Action listener) =>
            Remove(evt.Name, evt.HandlerType, listener);

        /// <summary>取消单参数事件订阅。应在 <c>OnDisable</c> 或销毁前调用。</summary>
        /// <typeparam name="T">事件参数类型。</typeparam>
        /// <param name="evt">订阅时使用的事件。</param>
        /// <param name="listener">订阅时使用的同一个回调实例。</param>
        public static void RemoveListener<T>(EventId<T> evt, Action<T> listener) =>
            Remove(evt.Name, evt.HandlerType, listener);

        /// <summary>取消双参数事件订阅。应在 <c>OnDisable</c> 或销毁前调用。</summary>
        /// <typeparam name="T1">第一个事件参数类型。</typeparam>
        /// <typeparam name="T2">第二个事件参数类型。</typeparam>
        /// <param name="evt">订阅时使用的事件。</param>
        /// <param name="listener">订阅时使用的同一个回调实例。</param>
        public static void RemoveListener<T1, T2>(EventId<T1, T2> evt, Action<T1, T2> listener) =>
            Remove(evt.Name, evt.HandlerType, listener);

        /// <summary>同步派发无参数事件；没有监听器时为空操作。</summary>
        /// <param name="evt">要派发的事件。</param>
        public static void Dispatch(EventId evt)
        {
            EnsureSignature(evt.Name, evt.HandlerType);
            GetPublisher(out string publisher, out string triggerFunction);
            Invoke<Action>(evt.Name, publisher, triggerFunction, listener => listener());
        }

        /// <summary>同步派发带一个参数的事件。</summary>
        /// <typeparam name="T">事件参数类型。</typeparam>
        /// <param name="evt">要派发的事件。</param>
        /// <param name="arg">传给所有监听器的参数。</param>
        public static void Dispatch<T>(EventId<T> evt, T arg)
        {
            EnsureSignature(evt.Name, evt.HandlerType);
            GetPublisher(out string publisher, out string triggerFunction);
            Invoke<Action<T>>(evt.Name, publisher, triggerFunction, listener => listener(arg));
        }

        /// <summary>同步派发带两个参数的事件。</summary>
        /// <typeparam name="T1">第一个事件参数类型。</typeparam>
        /// <typeparam name="T2">第二个事件参数类型。</typeparam>
        /// <param name="evt">要派发的事件。</param>
        /// <param name="arg1">传给所有监听器的第一个参数。</param>
        /// <param name="arg2">传给所有监听器的第二个参数。</param>
        public static void Dispatch<T1, T2>(EventId<T1, T2> evt, T1 arg1, T2 arg2)
        {
            EnsureSignature(evt.Name, evt.HandlerType);
            GetPublisher(out string publisher, out string triggerFunction);
            Invoke<Action<T1, T2>>(evt.Name, publisher, triggerFunction, listener => listener(arg1, arg2));
        }

        static void Add(string eventName, Type signature, Delegate listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            EnsureSignature(eventName, signature);
            if (s_listeners.TryGetValue(eventName, out Delegate existing))
            {
                if (existing.GetType() != listener.GetType())
                {
                    throw new InvalidOperationException(
                        $"Event '{eventName}' is already registered with a different listener signature.");
                }

                s_listeners[eventName] = Delegate.Combine(existing, listener);
                Log("Subscribe", eventName, DescribeListener(listener));
                return;
            }

            s_listeners.Add(eventName, listener);
            Log("Subscribe", eventName, DescribeListener(listener));
        }

        static void Remove(string eventName, Type signature, Delegate listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            EnsureSignature(eventName, signature);
            if (!s_listeners.TryGetValue(eventName, out Delegate existing))
            {
                return;
            }

            if (existing.GetType() != listener.GetType())
            {
                throw new InvalidOperationException(
                    $"Event '{eventName}' is already registered with a different listener signature.");
            }

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

        static void EnsureSignature(string eventName, Type signature)
        {
            if (s_signatures.TryGetValue(eventName, out Type existing))
            {
                if (existing != signature)
                {
                    throw new InvalidOperationException(
                        $"Event '{eventName}' expects {FormatSignature(existing)}, got {FormatSignature(signature)}.");
                }

                return;
            }

            s_signatures.Add(eventName, signature);
        }

        static string FormatSignature(Type signature)
        {
            if (signature == typeof(Action))
            {
                return "no parameters";
            }

            Type[] args = signature.GenericTypeArguments;
            if (args.Length == 1)
            {
                return args[0].Name;
            }

            return args[0].Name + ", " + args[1].Name;
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
    }
}
