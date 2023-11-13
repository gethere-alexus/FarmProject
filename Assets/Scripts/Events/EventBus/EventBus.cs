using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    private Dictionary<Type, List<EventHandler>> _subscribersByType;

    public EventBus()
    {
        _subscribersByType = new Dictionary<Type, List<EventHandler>>();
    }

    public void Subscribe<T>(EventHandler eventHandler) where T : EventArgs
    {
        var type = typeof(T);
        if (!_subscribersByType.TryGetValue(type, out var subscribers))
        {
            _subscribersByType.Add(type, new List<EventHandler>());
        }
        
        _subscribersByType[type].Add(eventHandler);
    }

    public void Unsubscribe<T>(EventHandler eventHandler) where T : EventArgs
    {
        var type = typeof(T);
        if (_subscribersByType.TryGetValue(type, out var subscribers))
        {
            _subscribersByType[type].Remove(eventHandler);
        }
    }

    public void Publish(object sender, EventArgs eventArgs)
    {
        if(_subscribersByType.TryGetValue(eventArgs.GetType(),out var subscribers))
        {
            foreach (var subscriber in subscribers)
            {
                subscriber(sender, eventArgs);
            }
        }
    }
}
