using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventBus : IEventBus
{
    class Subscription
    {
        public Delegate Handler { get; }
        public object Subscriber { get; }
        public bool IsAlive => Subscriber == null || (Subscriber is UnityEngine.Object unityObj && unityObj != null);

        public Subscription(Delegate handler, object subscriber = null)
        {
            Handler = handler;
            Subscriber = subscriber;
        }
    }

    readonly Dictionary<Type, List<Subscription>> _eventSubscriptions = new Dictionary<Type, List<Subscription>>();
    readonly Queue<object> _eventQueue = new Queue<object>();
    readonly object _lockObject = new object();
    bool _isPublishing = false;
    private bool _enableLogging = false;

    public bool EnableLogging
    {
        get => _enableLogging;
        set => _enableLogging = value;
    }
    
    public void Subscribe<T>(Action<T> handler) where T : struct
    {
        Subscribe(handler, null);
    }

    public void Subscribe<T>(Action<T> handler, object subscriber) where T : struct
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        lock (_lockObject)
        {
            var eventType = typeof(T);

            if (!_eventSubscriptions.ContainsKey(eventType))
            {
                _eventSubscriptions[eventType] = new List<Subscription>();
            }

            // 检查是否已经订阅
            var existingSubscription = _eventSubscriptions[eventType]
                .FirstOrDefault(s => s.Handler == (Delegate)handler && s.Subscriber == subscriber);

            if (existingSubscription == null)
            {
                _eventSubscriptions[eventType].Add(new Subscription(handler, subscriber));

                if (_enableLogging)
                {
                    Debug.Log($"[EventBus] 订阅事件: {eventType.Name} (订阅者: {subscriber?.GetType().Name ?? "Anonymous"})");
                }
            }
            else if (_enableLogging)
            {
                Debug.LogWarning($"[EventBus] 重复订阅事件: {eventType.Name}");
            }
        }
    }

    // 取消订阅特定处理器
    public void Unsubscribe<T>(Action<T> handler) where T : struct
    {
        if (handler == null) return;

        lock (_lockObject)
        {
            var eventType = typeof(T);
            if (_eventSubscriptions.ContainsKey(eventType))
            {
                _eventSubscriptions[eventType].RemoveAll(s => s.Handler == (Delegate)handler);

                if (_eventSubscriptions[eventType].Count == 0)
                {
                    _eventSubscriptions.Remove(eventType);
                }

                if (_enableLogging)
                {
                    Debug.Log($"[EventBus] 取消订阅事件: {eventType.Name}");
                }
            }
        }
    }

    // 取消订阅者的所有订阅
    public void UnsubscribeAll(object subscriber)
    {
        if (subscriber == null) return;

        lock (_lockObject)
        {
            int removedCount = 0;
            var emptyEventTypes = new List<Type>();

            foreach (var eventType in _eventSubscriptions.Keys.ToList())
            {
                _eventSubscriptions[eventType].RemoveAll(s =>
                {
                    if (s.Subscriber == subscriber)
                    {
                        removedCount++;
                        return true;
                    }
                    return false;
                });

                if (_eventSubscriptions[eventType].Count == 0)
                {
                    emptyEventTypes.Add(eventType);
                }
            }

            foreach (var eventType in emptyEventTypes)
            {
                _eventSubscriptions.Remove(eventType);
            }

            if (_enableLogging && removedCount > 0)
            {
                Debug.Log($"[EventBus] 取消订阅者 {subscriber.GetType().Name} 的 {removedCount} 个订阅");
            }
        }
    }

    // 同步发布事件
    public void Publish<T>(T eventData) where T : struct
    {
        if (_isPublishing)
        {
            // 如果正在发布事件，将新事件加入队列
            lock (_eventQueue)
            {
                _eventQueue.Enqueue(eventData);
            }
            return;
        }

        _isPublishing = true;

        try
        {
            var eventType = typeof(T);
            List<Subscription> subscriptionsToInvoke = null;

            // 在锁内获取需要调用的订阅列表
            lock (_lockObject)
            {
                if (_eventSubscriptions.ContainsKey(eventType))
                {
                    // 清理无效的订阅并复制有效订阅
                    _eventSubscriptions[eventType].RemoveAll(s => !s.IsAlive);

                    if (_eventSubscriptions[eventType].Count > 0)
                    {
                        subscriptionsToInvoke = new List<Subscription>(_eventSubscriptions[eventType]);
                    }
                    else
                    {
                        _eventSubscriptions.Remove(eventType);
                    }
                }
            }

            // 在锁外调用处理器（避免死锁）
            if (subscriptionsToInvoke != null)
            {
                foreach (var subscription in subscriptionsToInvoke)
                {
                    try
                    {
                        if (subscription.IsAlive)
                        {
                            ((Action<T>)subscription.Handler)?.Invoke(eventData);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"[EventBus] 事件处理异常 ({eventType.Name}): {e.Message}\n{e.StackTrace}");
                    }
                }

                if (_enableLogging)
                {
                    Debug.Log($"[EventBus] 发布事件: {eventType.Name} (处理器: {subscriptionsToInvoke.Count})");
                }
            }
            else if (_enableLogging)
            {
                Debug.LogWarning($"[EventBus] 事件 {eventType.Name} 没有订阅者");
            }
        }
        finally
        {
            _isPublishing = false;

            // 处理队列中的事件
            ProcessEventQueue();
        }
    }

    // 处理事件队列
    private void ProcessEventQueue()
    {
        while (true)
        {
            object queuedEvent;
            lock (_eventQueue)
            {
                if (_eventQueue.Count == 0) break;
                queuedEvent = _eventQueue.Dequeue();
            }

            try
            {
                var eventType = queuedEvent.GetType();
                var method = typeof(EventBus).GetMethod(nameof(Publish));
                var genericMethod = method.MakeGenericMethod(eventType);
                genericMethod.Invoke(this, new[] { queuedEvent });
            }
            catch (Exception e)
            {
                Debug.LogError($"[EventBus] 处理队列事件异常: {e.Message}");
            }
        }
    }
}