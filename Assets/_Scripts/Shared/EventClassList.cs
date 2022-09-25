using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventClassList
{

    public EventClass[] events;
    public List<EventClass> eventList;

    public List<EventClass> _socialCat1; //De 0 a 25
    public List<EventClass> _socialCat2; //De 25 a 50
    public List<EventClass> _socialCat3; //De 50 a 75
    public List<EventClass> _socialCat4; //De 75 a 100
    public List<EventClass> _socialCat5;
    public List<EventClass> _lockedEvents;

    public void GenerateEventList()
    {
        eventList = new List<EventClass>(events);
    }

    public void GenerateSocialLists()
    {
        foreach(EventClass eventClass in eventList)
        {
            if(eventClass.tsocial == 0)
            {
                if (eventClass.trsocial == 100)
                {
                    _socialCat5.Add(eventClass);
                }
                else
                {
                    _socialCat1.Add(eventClass);
                }
            }
            else if(eventClass.tsocial == 25)
            {
                _socialCat2.Add(eventClass);
            }
            else if (eventClass.tsocial == 50)
            {
                _socialCat3.Add(eventClass);
            }
            else if (eventClass.tsocial == 75)
            {
                _socialCat4.Add(eventClass);
            }
            else
            {
                _lockedEvents.Add(eventClass);
            }
        }
    }
}
