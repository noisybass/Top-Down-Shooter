using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEvent {

    public EventManager.EventType type;

    public BaseEvent(EventManager.EventType type)
    {
        this.type = type;
    }
}
