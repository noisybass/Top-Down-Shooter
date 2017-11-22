using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager> {

    public enum EventType
    {
        SHOOT,
        DEFAULT
    }

    public delegate void EventHandler(object sender, BaseEvent e);

    private Dictionary<EventType, List<EventHandler>> _eventHandlers;

    protected override void Awake()
    {
        base.Awake();

        _eventHandlers = new Dictionary<EventType, List<EventHandler>>();
    }

    public void AddHandler(EventType type, EventHandler handler)
    {
        if (!_eventHandlers.ContainsKey(type))
        {
            _eventHandlers.Add(type, new List<EventHandler>());
        }
        _eventHandlers[type].Add(handler);
    }

    public void DeleteHandler(EventType type, EventHandler handler)
    {
        List<EventHandler> handlers = _eventHandlers[type];

        if (handlers != null)
        {
            handlers.Remove(handler);
        }
    }

    public void OnEvent(object sender, BaseEvent e)
    {
        if (_eventHandlers.ContainsKey(e.type))
        {
            List<EventHandler> handlers = _eventHandlers[e.type];
            for (int i = 0; i < handlers.Count; i++)
            {
                handlers[i](sender, e);
            }
        }
    }
}
