using System.Collections.Generic;
using System;

public static class Broadcast
{
    private static Dictionary<Type, List<Delegate>> _listeners = new();

    public static void Subscribe<T>(Action<T> listener) where T : IMessage
    {
        Type type = typeof(T);

        if (!_listeners.ContainsKey(type))
        {
            _listeners[type] = new List<Delegate>();
        }

        _listeners[type].Add(listener);
    }

    public static void Unsubscribe<T>(Action<T> listener) where T : IMessage
    {
        Type type = typeof(T);

        if (_listeners.ContainsKey(type))
        {
            _listeners[type].Remove(listener);
        }
    }

    public static void Send<T>(T message) where T : IMessage
    {
        Type type = typeof(T);

        if (_listeners.ContainsKey(type))
        {
            foreach (var listener in _listeners[type])
            {
                ((Action<T>)listener)(message);
            }
        }
    }
}
