using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EventClass[] _unlockedEvents;
    private EventClass[] _lockedEvents;

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

        if(roll > 91.67)
        {
            rolledEvents++;
            Debug.Log("Eventos rolleados en" + timeCont + "intentos: " + rolledEvents);
        }
    }
}
