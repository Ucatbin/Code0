using System;
using System.Collections.Generic;

namespace ThisGame.Core
{
    public static class EventBus
    {
        static readonly Dictionary<Type, List<Delegate>> _eventHandlers = new();
        static readonly Dictionary<object, List<Delegate>> _ownerHandlers = new();
        public static void Subscribe<T>(object owner, Action<T> handler) where T : struct
        {
            var eventType = typeof(T);
            if (!_eventHandlers.ContainsKey(eventType))
                _eventHandlers[eventType] = new List<Delegate>();

            _eventHandlers[eventType].Add(handler);

            if (!_ownerHandlers.ContainsKey(owner))
                _ownerHandlers[owner] = new List<Delegate>();
            
            _ownerHandlers[owner].Add(handler);
        }

        public static void Unsubscribe<T>(Action<T> handler) where T : struct
        {
            var eventType = typeof(T);
            if (_eventHandlers.ContainsKey(eventType))
                _eventHandlers[eventType].Remove(handler);
        }
        public static void UnsubscribeAll(object owner)
        {
            if (_ownerHandlers.ContainsKey(owner))
            {
                foreach (var handler in _ownerHandlers[owner])
                {
                    foreach (var eventList in _eventHandlers.Values)
                    {
                        eventList.Remove(handler);
                    }
                }
                _ownerHandlers.Remove(owner);
            }
        }

        public static void Publish<T>(T eventData) where T : struct
        {
            var eventType = typeof(T);
            if (_eventHandlers.ContainsKey(eventType))
            {
                var handlers = _eventHandlers[eventType].ToArray();
                foreach (var handler in handlers)
                {
                    (handler as Action<T>)?.Invoke(eventData);
                }
            }
        }

        public static void Clear()
        {
            _eventHandlers.Clear();
        }
    }
}