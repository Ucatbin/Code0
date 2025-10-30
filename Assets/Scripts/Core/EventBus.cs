using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : IEventBus
{
    private readonly Dictionary<Type, Delegate> _eventHandlers = new Dictionary<Type, Delegate>();

    public void Subscribe<T>(Action<T> handler) where T : struct
    {
        var eventType = typeof(T);
        if (_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] = Delegate.Combine(_eventHandlers[eventType], handler);
        }
        else
        {
            _eventHandlers[eventType] = handler;
        }
    }

    public void Unsubscribe<T>(Action<T> handler) where T : struct
    {
        var eventType = typeof(T);
        if (_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] = Delegate.Remove(_eventHandlers[eventType], handler);
            
            if (_eventHandlers[eventType] == null)
            {
                _eventHandlers.Remove(eventType);
            }
        }
    }

    public void Publish<T>(T eventData) where T : struct
    {
        var eventType = typeof(T);
        if (_eventHandlers.ContainsKey(eventType) && _eventHandlers[eventType] != null)
        {
            ((Action<T>)_eventHandlers[eventType])?.Invoke(eventData);
        }
    }
}
