using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject[] _windows;

    [SerializeField]
    private Button[] _buttons;

    [SerializeField]
    private Slider[] _sliders;

    [SerializeField]
    public TMPro.TextMeshProUGUI moneyStatText;

    [SerializeField]
    private Button[] _options;

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
        _buttons[0].onClick.AddListener(() => openWindow(0));
        _buttons[1].onClick.AddListener(() => openWindow(1));
        _buttons[2].onClick.AddListener(() => openWindow(2));
        _buttons[3].onClick.AddListener(() => openWindow(3));
        _buttons[4].onClick.AddListener(() => openEventWindow(false));

        _options[0].onClick.AddListener(() => onClickOption(0));
        _options[1].onClick.AddListener(() => onClickOption(1));
        _options[2].onClick.AddListener(() => onClickOption(2));

        foreach (GameObject window in _windows)
        {
            window.SetActive(false);
        }

    }


    public void openWindow(WindowID windowID)
    {
        _windows[(int) windowID].SetActive(true);
    }

    public void openWindow(int windowID)
    {
        Debug.Log("Se abre la ventana " + windowID);
        _windows[windowID].transform.SetSiblingIndex(4);
        _windows[windowID].SetActive(true);

        if(windowID==0)
        {
            PauseGame();
        }
    }

    public void closeWindow(int windowID)
    {
        Debug.Log("Se cierra ventana " + windowID);
        _windows[windowID].SetActive(false);

        if (windowID == 0)
        {
            ResumeGame();
        }
    }

    public void openEventWindow(bool noClose)
    {
        //GameManager.Instance.currentEvent;
        //Set values in UI

        openWindow(4);
        
        var button = _windows[4].transform.GetChild(0).GetComponent<Button>();
        
        if(noClose)
        {
            button.interactable = false;
            PauseGame();
        }
    }

    public void closeEventWindow(bool noClose)
    {
        closeWindow(4);

        var button = _windows[4].transform.GetChild(0).GetComponent<Button>();

        if (noClose)
        {
            button.interactable = true;
            ResumeGame();
        }
    }

    public void onClickOption(int index)
    {
        //Change stats
        GameManager.Instance.onOptionSelected(index);

        //Close event Window
        closeEventWindow(true);

        //Animation

        //Update sliders and money counter
        _sliders[0].value = GameManager.Instance.work;
        _sliders[1].value = GameManager.Instance.health;
        _sliders[2].value = GameManager.Instance.socials;
        moneyStatText.text = GameManager.Instance.money.ToString();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }


}
