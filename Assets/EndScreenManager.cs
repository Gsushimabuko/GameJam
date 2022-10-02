using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private bool fadeIn = false;
    private bool fadeOut = false;

    public void ShowUI()
    {
        fadeIn = true; 
    }
    public void HideUI()
    {
        fadeOut = true;
    }

    void Update()
    {
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
        }
    }
}


  