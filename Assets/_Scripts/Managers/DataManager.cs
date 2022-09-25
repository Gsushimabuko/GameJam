using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public EventClassList events = new EventClassList();
    public List<EventClass> _unlockedEvents;
    public List<EventClass> _lockedEvents;
    public List<EventClass> _usedEvents;
    public TextAsset textJSON;

    public List<EventClass> activeEvents = new();
    public EventClass currentEvent;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        events = JsonUtility.FromJson<EventClassList>("{\"events\":" + textJSON.text + "}");
        events.generateEventList();

        _unlockedEvents = events.eventList;
    }

    public EventClass GetEvent()
    {
        int index = Random.Range(0, _unlockedEvents.Count - 1);
        var eventObj = _unlockedEvents[index];

        Debug.Log("Index: " + eventObj.ID);

        if (eventObj == null)
        {
            return null;
        }

        _unlockedEvents.RemoveAt(index);
        _usedEvents.Add(eventObj);
        activeEvents.Add(eventObj);
        
        eventObj.Print();
        return eventObj;
    }

    public EventClass GetCurrentEvent(int index)
    {
        currentEvent = activeEvents[index];
        return currentEvent;
    }
}
