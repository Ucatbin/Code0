using System;

public interface IEventBus
{
    void Subscribe<T>(Action<T> handler) where T : struct;
    void Subscribe<T>(Action<T> handler, object subscriber) where T : struct;
    void Unsubscribe<T>(Action<T> handler) where T : struct;
    void UnsubscribeAll(object subscriber);
    void Publish<T>(T eventData) where T : struct;
}
