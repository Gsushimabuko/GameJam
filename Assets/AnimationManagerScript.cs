using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagerScript : MonoBehaviour
{
    public static AnimationManagerScript Instance;

    public GameManager gameManager;

    [SerializeField]
    private Animator screenFilterAnimator;

    public Animator moneyTextAnimator;

    public Animator CounterAnimator;

    public Animator healthBarAnimator;

    public Animator socialBarAnimator;

    // Start is called before the first frame update
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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkHealthAnimation();
    }

    public void checkHealthAnimation()
    {
        if(gameManager.getHealth() < 100)
        {
            healthBarAnimator.SetInteger("play", 1);
        }
        else
        {
            healthBarAnimator.SetInteger("play", 0);
        }
    }

    public void blinkRedCoroutine()
    {
        StartCoroutine(blinkRed());
    }

    public void blinkBlueCoroutine()
    {
        StartCoroutine(blinkBlue());
    }

    public void moneyUpCoroutine()
    {
        StartCoroutine(moneyUp());
    }

    public void moneyDownCoroutine()
    {
        StartCoroutine(moneyDown());
    }


    public void setCounterState(int state)
    {
        CounterAnimator.SetInteger("state", state);
    }


    public void socialBarBlinkTrigger()
    {
        StartCoroutine(socialBarBlink());
    }

    public void healthBarBlinkTrigger()
    {
        StartCoroutine(healthBarBlink());
    }


    IEnumerator healthBarBlink()
    {
        healthBarAnimator.SetInteger("play", 1);
        yield return new WaitForSeconds(1f);
        healthBarAnimator.SetInteger("play", 0);

    }

    IEnumerator socialBarBlink()
    {
        socialBarAnimator.SetInteger("play", 1);
        yield return new WaitForSeconds(1f);
        socialBarAnimator.SetInteger("play", 0);

    }


    IEnumerator moneyUp()
    {
        moneyTextAnimator.SetInteger("moneyState", 2);
        yield return new WaitForSeconds(0.5f);
        moneyTextAnimator.SetInteger("moneyState", 1);

    }

    IEnumerator moneyDown()
    {
        moneyTextAnimator.SetInteger("moneyState", 0);
        yield return new WaitForSeconds(0.5f);
        moneyTextAnimator.SetInteger("moneyState", 1);

    }



    IEnumerator blinkRed()
    {
        screenFilterAnimator.SetInteger("state", 1);
        yield return new WaitForSeconds(0.5f);
        screenFilterAnimator.SetInteger("state", 0);
    }


    IEnumerator blinkBlue()
    {
        screenFilterAnimator.SetInteger("state", 2);
        yield return new WaitForSeconds(0.5f);
        screenFilterAnimator.SetInteger("state", 0);
    }

}
