using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBehaviour : MonoBehaviour
{
    public string player;
    public TMPro.TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return)) {
            StoreName();
            //Debug.Log(player);

            if(!string.IsNullOrWhiteSpace(player))
            {
                MenuManager.Instance.player = player;
                MenuManager.Instance.ShowLoadingScreen();
            }
        }
    }

    public void StoreName()
    {
        player = inputField.text;
    }
}
