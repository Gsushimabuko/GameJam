using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuitScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button myButton;
    public Button myButton1;
    public Button myButton2;
    public Button myButton3;
    public Button myButton4;
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(ExitFunction);

        myButton1 = GetComponent<Button>();
        myButton1.onClick.AddListener(ExitFunction);

        myButton2 = GetComponent<Button>();
        myButton2.onClick.AddListener(ExitFunction);

        myButton3 = GetComponent<Button>();
        myButton3.onClick.AddListener(ExitFunction);

        myButton4 = GetComponent<Button>();
        myButton4.onClick.AddListener(ExitFunction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ExitFunction()
    {
        Application.Quit();
    }
}
