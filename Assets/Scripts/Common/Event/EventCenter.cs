using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AChen.Log;

namespace AChen.Events
{
    /// <summary>进程内事件中心。事件用 <see cref="GameEvent"/> 里声明的 <see cref="EventId"/>，类型跟事件走。</summary>
    public static class EventCenter
    {
        static readonly Dictionary<string, Delegate> s_listeners = new Dictionary<string, Delegate>();
        static readonly Dictionary<string, Type> s_signatures = new Dictionary<string, Type>();

        public static void AddListener(EventId evt, Action listener) =>
            Add(evt.Name, evt.HandlerType, listener);

        public static void AddListener<T>(EventId<T> evt, Action<T> listener) =>
            Add(evt.Name, evt.HandlerType, listener);

        public static void AddListener<T1, T2>(EventId<T1, T2> evt, Action<T1, T2> listener) =>
            Add(evt.Name, evt.HandlerType, listener);

        public static void RemoveListener(EventId evt, Action listener) =>
            Remove(evt.Name, evt.HandlerType, listener);

        public static void RemoveListener<T>(EventId<T> evt, Action<T> listener) =>
            Remove(evt.Name, evt.HandlerType, listener);

        public static void RemoveListener<T1, T2>(EventId<T1, T2> evt, Action<T1, T2> listener) =>
            Remove(evt.Name, evt.HandlerType, listener);

        public static void Dispatch(EventId evt)
        {
            EnsureSignature(evt.Name, evt.HandlerType);
            GetPublisher(out string publisher, out string triggerFunction);
            Invoke<Action>(evt.Name, publisher, triggerFunction, listener => listener());
        }

        public static void Dispatch<T>(EventId<T> evt, T arg)
        {
            EnsureSignature(evt.Name, evt.HandlerType);
            GetPublisher(out string publisher, out string triggerFunction);
            Invoke<Action<T>>(evt.Name, publisher, triggerFunction, listener => listener(arg));
        }

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
