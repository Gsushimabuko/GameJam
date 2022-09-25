using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public  AnimationManagerScript animationScript;

    #region stats
    public float hapiness = 50f;
    public float money;
    public float work = 50f;
    public float health = 100f;
    public float socials = 50f;
    public float events = 0;
    #endregion

    //private List<EventClass> _unlockedEvents = new List<string> { };
    //private List<EventClass> _lockedEvents;
    //private List<EventClass> _usedEvents;

    private List<string> _unlockedEvents = new List<string>{ "a", "b", "c", "d", "e", "f" };
    private List<string> _lockedEvents = new List<string> { "x", "y", "z" };
    private List<string> _usedEvents = new List<string> { };

    private int timeCont = 0;
    private float _timeWindow = 1f;
    private int rolledEvents = 0;
    private float _rollProbability = 60;

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

        if(roll > _rollProbability)
        {
            _rollProbability = 111;
            rolledEvents++;
            Debug.Log("Eventos rolleados en " + timeCont + " intentos: " + rolledEvents);
            
            EventClass eventTriggered = DataManager.Instance.GetEvent();

            triggerEvent(eventTriggered);
        }
    }
    public float getMoney()
    {
        return this.money;
    }


    private void triggerEvent(EventClass eventObj)
    {
      UIManager.Instance.openEventWindow(false);
    }

    public void changeStats(int money, int work, int health, int socials, int hapiness)
    {
        //Affect stats
        this.money += money;
        this.work += work;
        this.health += health;
        this.socials += socials;
        this.hapiness += hapiness;
    }

    public void onOptionSelected(int index)
    {
        //Affect stats
        changeStats(100, 10, -10, 10, 10);
    }

    public void rollCrypto(){
        int index = Random.Range(0, 10);
        if(index  <=3)
        {
            this.money = this.money +1000;
          //animationScript.blinkBlueCoroutine();
            animationScript.moneyUpCoroutine();
        }
        else if (index>=4)
        {
            this.money = this.money -1000;
            // animationScript.blinkRedCoroutine();
            animationScript.moneyDownCoroutine();
        }

    }

    public void deposit(int depositType)
    {
        if(depositType == 1)
        {
            StartCoroutine(depositEnum(1));
        }
        else if(depositType == 2)
        {
            StartCoroutine(depositEnum(2));
        }
        else if (depositType ==3 )
        {
            StartCoroutine(depositEnum(5));
        }

        
        
    }
  IEnumerator depositEnum(int risk)
    {
        float interest = 1 + (risk * 0.1f);
        float depositAmount = 2000;
        this.money = this.money - depositAmount;
        yield return new WaitForSeconds(10);
        int roll = Random.Range(1, 10);
        if(risk > roll)
        {
            Debug.Log("Roll lost");
            animationScript.moneyDownCoroutine();
            //nimationScript.blinkRedCoroutine();
        }
        else
        {
            this.money = this.money + (depositAmount * interest);
            animationScript.moneyUpCoroutine();
            //nimationScript.blinkBlueCoroutine();

        }

    }

}
