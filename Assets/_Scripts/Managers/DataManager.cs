using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public EventClassList events = new EventClassList();

    public List<EventClass> defaultTree;
    public List<EventClass> tree1;
    public List<EventClass> tree2;
    public List<EventClass> tree3;
    public List<EventClass> tree4;

    //public List<EventClass> _unlockedEvents; //Unlocked events
    //public List<EventClass> _socialCategory; //Current state
    //public List<EventClass> _socialCat5; //Always available

    //public List<EventClass> _socialCat1; //De 0 a 25
    //public List<EventClass> _socialCat2; //De 25 a 50
    //public List<EventClass> _socialCat3; //De 50 a 75
    //public List<EventClass> _socialCat4; //De 75 a 100

    //public List<EventClass> _lockedEvents;
    public List<EventClass> _usedEvents;
    public TextAsset textJSON;

    public List<EventClass> activeEvents = new();
    public EventClass currentEvent;

    [Header("Event tree")]
    public int _eventIndex = 0;
    public int _treeIndex = 0;
    public List<List<EventClass>> _trees;

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
        events.GenerateEventList();
        events.GenerateTrees();
        events.PrintEvents(1);

        _trees = events._trees;

        defaultTree = events.defaultTree;
        tree1 = events.tree1;
        tree2 = events.tree2;
        tree3 = events.tree3;
        tree4 = events.tree4;

        //_socialCat1 = events._socialCat1; //De 0 a 25
        //_socialCat2 = events._socialCat2; //De 25 a 50
        //_socialCat3 = events._socialCat3; //De 50 a 75
        //_socialCat4 = events._socialCat4; //De 75 a 100
        //_socialCat5 = events._socialCat5; //De 0 a 100
        //_lockedEvents = events._lockedEvents;
    }

    //public EventClass GetEvent()
    //{
    //    List<EventClass> eventList = ChooseEventList();

    //    int index = Random.Range(0, eventList.Count);

    //    var eventObj = new EventClass();

    //    if (eventList.Count > 0)
    //    {
    //        eventObj = eventList[index];

    //        Debug.Log("Index: " + eventObj.ID);

    //        if (eventObj == null)
    //        {
    //            return null;
    //        }

    //        eventList.Remove(eventObj);
    //        _usedEvents.Add(eventObj);
    //        activeEvents.Add(eventObj);

    //        eventObj.Print();
    //    }

    //    return eventObj;
    //}

    public EventClass GetEvent()
    {
        List<EventClass> eventList = _trees[_treeIndex];
        EventClass eventObj = eventList[_eventIndex];

        _eventIndex++;

        if(_eventIndex >= eventList.Count)
        {
            _eventIndex = 0;

            if (GameManager.Instance.socials <= 33)
            {
                //weeb route
                _treeIndex = 3;
            }
            else if (GameManager.Instance.socials <= 67)
            {
                _treeIndex = 2;
            }
            else if (GameManager.Instance.socials <= 100)
            {
                //manyado route
                _treeIndex = 1;
            }
        }

        _usedEvents.Add(eventObj);
        activeEvents.Add(eventObj);

        return eventObj;
    }

    public void RemoveEventFromActiveList(int index)
    {
        activeEvents.RemoveAt(index);
    }

    //public List<EventClass> ChooseEventList()
    //{
    //    int index = Random.Range(0, 3);

    //    List<EventClass> list = null;

    //    if(index == 0)
    //    {
    //        list = _unlockedEvents;

    //        if(list.Count == 0)
    //        {
    //            list = _socialCat5;
    //        }
    //    }
    //    else if (index == 1)
    //    {
    //        list = _socialCategory;

    //        if (list.Count == 0)
    //        {
    //            list = _socialCat5;
    //        }
    //    }
    //    else if (index == 2)
    //    {
    //        list = _socialCat5;
    //    }
    //    Debug.Log("Index de lista: " + index);
    //    return list;

    //}

    //public void UpdateSocialListEvents()
    //{
    //    if(GameManager.Instance.socials < 25)
    //    {
    //        _socialCategory = _socialCat1;
    //    }
    //    else if (GameManager.Instance.socials < 50)
    //    {
    //        _socialCategory = _socialCat2;
    //    }
    //    else if (GameManager.Instance.socials < 75)
    //    {
    //        _socialCategory = _socialCat3;
    //    }
    //    else if (GameManager.Instance.socials <= 100)
    //    {
    //        _socialCategory = _socialCat4;
    //    }
    //}

    //public void UnlockEvent(int ID)
    //{
    //    if(!(ID == 0 || ID == -1))
    //    {
    //        foreach (EventClass eventclass in _lockedEvents)
    //        {
    //            if (eventclass.ID == ID)
    //            {
    //                _unlockedEvents.Add(eventclass);
    //                _lockedEvents.Remove(eventclass);
    //            }
    //        }
    //    }

    //}

    public EventClass GetCurrentEvent(int index)
    {
        currentEvent = activeEvents[index];
        return currentEvent;
    }
}
