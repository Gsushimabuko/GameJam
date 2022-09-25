using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagerScript : MonoBehaviour
{


    public static AnimationManagerScript Instance;

    [SerializeField]
    private Animator screenFilterAnimator;

    public Animator moneyTextAnimator;

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
