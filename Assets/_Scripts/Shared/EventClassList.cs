using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventClassList
{

    public EventClass[] events;
    public List<EventClass> eventList;

    public void generateEventList()
    {
        eventList = new List<EventClass>(events);
    }
}
