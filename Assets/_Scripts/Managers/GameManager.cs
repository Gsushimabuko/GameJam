using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public  AnimationManagerScript animationScript;

    #region stats
    public float hapiness = 50f;
    public float money = 0;
    public float work = 50f;
    public float health = 100f;
    public float socials = 50f;
    public float events = 0;
    #endregion

    private int timeCont = 0;
    private float _timeWindow = 5f;
    private int rolledEvents = 0;
    private float _rollProbability = 81.67f;

    private int _gameDuration = 900;
    private IEnumerator _gameCoroutine = null;

    public int ending = -1;

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
        _gameCoroutine = WaitALife();

        StartCoroutine(_gameCoroutine);
    }

    public void GetEnding()
    {
        if(hapiness <= 25)
        {
            ending = 0;
        }
        else if (hapiness <= 50)
        {
            ending = 1;
        }
        else if (hapiness <= 75)
        {
            ending = 2;
        }
        else if (hapiness > 75)
        {
            ending = 3;
        }
        Debug.Log(hapiness);
    }

    public bool IsGameOver()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator WaitALife()
    {
        yield return new WaitForSeconds(_gameDuration);
        
        GetEnding();
        GameOver();
    }

    public void GameOver()
    {
        StopAllCoroutines();

        //Show window
        UIManager.Instance.ShowGameOverScreen(ending);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("rollEvent", 5, _timeWindow);
        InvokeRepeating("GetOld", 0, 5);
    }

    public void GetOld()
    {
        Debug.Log("a");
        changeStats(0, 0, 5, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //SOFT RESET
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void rollEvent()
    {
        float roll = Random.Range(0.0f, 100.0f);
        timeCont++;
        //Debug.Log("Intento " + timeCont + ": " + roll);

        if(roll > _rollProbability)
        {
            //Get event
            rolledEvents++;
            EventClass eventTriggered = DataManager.Instance.GetEvent();
            //Debug.Log("Eventos rolleados en " + timeCont + " intentos: " + rolledEvents);

            //Update currentEvents number stat
            UIManager.Instance.UpdateActiveEventsNumber(DataManager.Instance.activeEvents.Count);
            SoundManager.Instance.PlayNewEventSound();
            //Make sound
            //Make animation
        }
    }

    public float getMoney()
    {
        return money;
    }

    public void changeStats(float money, float work, float health, float socials, float hapiness)
    {
        //Affect stats
        this.money += money;
        this.work += work;
        this.health += health;
        this.socials += socials;
        this.hapiness += hapiness;

        this.money = Mathf.Max(this.money, 0);
        this.work = Mathf.Max(this.work, 0);
        this.health = Mathf.Max(this.health, 0);
        this.socials = Mathf.Max(this.socials, 0);
        this.hapiness = Mathf.Max(this.hapiness, 0);

        this.work = Mathf.Min(this.work, 100);
        this.health = Mathf.Min(this.health, 100);
        this.socials = Mathf.Min(this.socials, 100);
        this.hapiness = Mathf.Min(this.hapiness, 100);

        if(money > 0)
        {
            AnimationManagerScript.Instance.moneyUpCoroutine();
        }
        else if (money < 0)
        {
            AnimationManagerScript.Instance.moneyDownCoroutine();
        }
        AnimationManagerScript.Instance.moneyDownCoroutine();

        if (IsGameOver())
        {
            ending = 4;
            GameOver();
        }

        DataManager.Instance.UpdateSocialListEvents();
    }

    public void onOptionSelected(OptionClass option)
    {
        //Affect stats
        changeStats(option.money, option.work, option.health, option.social, option.hapiness);
    }

    public void rollCrypto()
    {

        if (money >= 1000)
        {
            int index = Random.Range(0, 10);

            if (index <= 3)
            {
                this.money = this.money + 1000;
                //animationScript.blinkBlueCoroutine();
                animationScript.moneyUpCoroutine();
                SoundManager.Instance.playMoneyWin();
            }
            else if (index >= 4)
            {
                this.money = this.money - 1000;
                // animationScript.blinkRedCoroutine();
                animationScript.moneyDownCoroutine();
                SoundManager.Instance.playMoneyLoss();
            }
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
        if(money >= 2000)
        {
            this.money = this.money - depositAmount;
            yield return new WaitForSeconds(60);
            int roll = Random.Range(1, 10);
            if (risk > roll)
            {
                //LOSS
                Debug.Log("Roll lost");
                animationScript.moneyDownCoroutine();
                SoundManager.Instance.playMoneyLoss();
            }
            else
            {
                //WIN 
                this.money = this.money + (depositAmount * interest);
                animationScript.moneyUpCoroutine();
                SoundManager.Instance.playMoneyWin();

            }
        }
        

    }

}
