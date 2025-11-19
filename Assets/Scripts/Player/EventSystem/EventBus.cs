using System;
using System.Collections.Generic;
using System.Reflection;

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
        public static void SubscribeByType(object owner, Type eventType)
        {
            var handlerMethod = FindEventHandlerMethod(owner, eventType);
            if (handlerMethod != null)
            {
                var subscribeMethod = typeof(EventBus).GetMethod("Subscribe", BindingFlags.Public | BindingFlags.Static);
                var genericMethod = subscribeMethod.MakeGenericMethod(eventType);
                
                var delegateType = typeof(Action<>).MakeGenericType(eventType);
                var handlerDelegate = Delegate.CreateDelegate(delegateType, owner, handlerMethod);
                
                genericMethod.Invoke(null, new object[] { owner, handlerDelegate });
            }
        }

        private static MethodInfo FindEventHandlerMethod(object owner, Type eventType)
        {
            var methods = owner.GetType().GetMethods(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var parameters = method.GetParameters();
                if (parameters.Length == 1 && parameters[0].ParameterType == eventType)
                {
                    return method;
                }
            }
            
            UnityEngine.Debug.LogWarning($"No event handler method found for {eventType} in {owner.GetType()}");
            return null;
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