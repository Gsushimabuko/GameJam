using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region stats
    public float hapiness = 50f;
    public float money = 10000f;
    public float work = 50f;
    public float health = 100f;
    public float socials = 50f;
    public float events = 0;
    #endregion

    public Stack<string> currentEvent = new Stack<string>();
    //private EventClass[] _unlockedEvents;
    //private EventClass[] _lockedEvents;
    //private EventClass[] _usedEvents;

    private List<string> _unlockedEvents = new List<string>{ "a", "b", "c", "d", "e", "f" };
    private List<string> _lockedEvents = new List<string> { "x", "y", "z" };
    private List<string> _usedEvents = new List<string> { };

    private Coroutine _eventsCoroutine = null;
    private float waited = 0;
    private int timeCont = 0;
    private float _timeWindow = 0.5f;
    private int rolledEvents = 0;

    //IEnumerator rollEvent()
    //{
    //    timeCont++;

    //    if (waited == 0)
    //    {
    //        yield return new WaitForSeconds(_timeWindow);
    //        Debug.Log("Rolleando " + timeCont);
    //    }

    //    while (waited < _timeWindow)
    //    {
    //        waited += Time.deltaTime;
    //        yield return null;
    //    }

    //    if (waited >= 5)
    //    {
    //        waited = 0;
    //        _eventsCoroutine = StartCoroutine("rollEvent");
    //    }
    //}

    //IEnumerator _rollEvent()
    //{
    //    yield return new WaitForSeconds(5f);

    //    Debug.Log("Rolleando " + timeCont);
    //    timeCont++;
    //}
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
        //_eventsCoroutine = StartCoroutine("rollEvent");
        InvokeRepeating("rollEvent", 0, _timeWindow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void rollEvent()
    {
        float roll = Random.Range(0.0f, 100.0f);
        timeCont++;
        //Debug.Log("Intento " + timeCont + ": " + roll);

        if(roll > 90)
        {
            rolledEvents++;
            Debug.Log("Eventos rolleados en " + timeCont + " intentos: " + rolledEvents);
            triggerEvent(getEvent());
        }
    }

    private string getEvent()
    {
        int index = Random.Range(0, _unlockedEvents.Count-1);
        var eventObj = _unlockedEvents[index];

        _unlockedEvents.RemoveAt(index);
        _usedEvents.Add(eventObj);

        Debug.Log(string.Join(", ", _unlockedEvents));
        Debug.Log(string.Join(", ", _usedEvents));
        Debug.Log("Event: " + eventObj + " - Index: " + index);

        currentEvent.Push(eventObj);

        return eventObj;
    }

    private void triggerEvent(string eventObj)
    {
        UIManager.Instance.openEventWindow(true);
    }

    public void onOptionSelected(int money, int work, int health, int socials, int hapiness)
    {
        //Affect stats
        this.money += money;
        this.work += work;
        this.health += health;
        this.socials += socials;
        this.hapiness += hapiness;
    }
}
