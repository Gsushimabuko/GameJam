using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public int currentState = 1;
    public GameManager gameManager;
    public Animator endingAnimator;
    public DataManager eventScript;
    public GameObject statPanel;
    public TMPro.TextMeshProUGUI hapinessUI;
    public TMPro.TextMeshProUGUI routeUI;
    public TMPro.TextMeshProUGUI workUI;
    public TMPro.TextMeshProUGUI socialUI;
    public TMPro.TextMeshProUGUI moneyUI;


    public void nextAnimation()
    {
        currentState++;
        endingAnimator.SetInteger("state", currentState);
    }

    /*  public CanvasGroup canvasGroup;
      private bool fadeIn = false;
      private bool fadeOut = false;*/

    /*   public void ShowUI()
       {
           fadeIn = true; 
       }
       public void HideUI()
       {
           fadeOut = true;
       }
   */

    void Start()
    {
        
        hapinessUI.text  = gameManager.hapiness.ToString();
        workUI.text = gameManager.work.ToString();
        socialUI.text = gameManager.socials.ToString();
        moneyUI.text = gameManager.money.ToString();
        //bug
        if (eventScript._treeIndex == 0)
        {
            routeUI.text = "???: no llegaste a ninguna ruta";
        }
        else if(eventScript._treeIndex == 1)
        {
            routeUI.text = "Manyado: no eres el público objetivo de este juego, pero gracias por jugar";
        }
        //resultados esperados
        else if(eventScript._treeIndex == 2)
        {
            routeUI.text = "Persona normie: no desbordas personalidad";
        }
        else if (eventScript._treeIndex == 3)
        {
            routeUI.text = "Weeb: báñate";
        }
        
    }
    void Update()
    {
        if(currentState >= 3)
        {
            statPanel.SetActive(true);
        }
        /*
        if (fadeIn)
        {
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime;
                if(canvasGroup.alpha >= 1)
                {
                    Debug.Log("FADE IN COMPLETADO");
                    fadeIn = false;
                }
                     
            }
        }
        if (fadeOut)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    fadeOut = false;
                }

            }
        }*/
    }
}


  