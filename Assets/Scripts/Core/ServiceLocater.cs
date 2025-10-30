using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new();
    
    public static void Register<T>(T service) => _services[typeof(T)] = service;
    public static T Get<T>() => (T)_services[typeof(T)];
    public static bool TryGet<T>(out T service)
    {
        if (_services.ContainsKey(typeof(T)))
        {
            service = (T)_services[typeof(T)];
            return true;
        }
        service = default;
        return false;
    }
}