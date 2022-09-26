using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameManager gameManager;

    public static UIManager Instance;

    [SerializeField]
    private GameObject[] _windows;

    [SerializeField]
    private GameObject[] bankWindows;

    [SerializeField]
    private Button[] _buttons;

    [SerializeField]
    private GameObject _minigameWindow;

    [SerializeField]
    private Button[] bankButtons;

    [SerializeField]
    private Slider[] _sliders;

    [SerializeField]
    private TMPro.TextMeshProUGUI[] _slidersTexts;

    [SerializeField]
    public TMPro.TextMeshProUGUI moneyStatText;

    [SerializeField]
    private Button[] _options;

    [SerializeField]
    private Button startMinigameButton;

    [SerializeField]
    private GameObject eventPanel;

    [SerializeField]
    private Button[] _navigationEventButtons;

    [SerializeField]
    private TMPro.TextMeshProUGUI eventsNumber;
    public int eventNavigationNumber = 0;

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
        startMinigameButton.onClick.AddListener(() =>
        {
            StartMinigame();
        });

        _navigationEventButtons[0].onClick.AddListener(() =>
        {
            GoToPreviousEvent();
        });

        _navigationEventButtons[1].onClick.AddListener(() =>
        {
            GoToNextEvent();
        });

        _buttons[0].onClick.AddListener(() => openWindow(0));
        _buttons[1].onClick.AddListener(() => openWindow(1));
        _buttons[2].onClick.AddListener(() => openWindow(2));
        _buttons[3].onClick.AddListener(() => openWindow(3));
        _buttons[4].onClick.AddListener(() => openEventWindow(false));

        _options[0].onClick.AddListener(() => onClickOption(0));
        _options[1].onClick.AddListener(() => onClickOption(1));
        _options[2].onClick.AddListener(() => onClickOption(2));

        //BANK WINDOWS
        bankButtons[0].onClick.AddListener(() => openBankWindow(0));

        foreach (GameObject window in _windows)
        {
            window.SetActive(false);
        }

        foreach (GameObject window in bankWindows)
        {
            window.SetActive(false);
        }

        //WORK WINDOWS / MINIGAME
        _minigameWindow.SetActive(false);

        UpdateSliders();
    }

    public void UpdateActiveEventsNumber(int number)
    {
        eventsNumber.text = number.ToString();
    }

    public void GoToNextEvent()
    {
        eventNavigationNumber = Mathf.Min(DataManager.Instance.activeEvents.Count - 1, eventNavigationNumber + 1);
        Debug.Log(eventNavigationNumber);
        UpdateEventWindow(eventNavigationNumber);
    }

    public void GoToPreviousEvent()
    {
        eventNavigationNumber = Mathf.Max(0, eventNavigationNumber-1);
        Debug.Log(eventNavigationNumber);
        UpdateEventWindow(eventNavigationNumber);
    }

    public void StartMinigame()
    {
        _minigameWindow.SetActive(true);
        MinigameManager.Instance.OnGameStarted();
    }

    public void EndMinigame()
    {
        _minigameWindow.SetActive(false);
    }

    private void Update()
    {
        renderStat();
    }
    public void renderStat()
    {
        moneyStatText.text = "S/." + GameManager.Instance.getMoney() + ".00";
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
    public void openBankWindow(int windowID)
    {
        Debug.Log("Se abre la ventana " + windowID);
        bankWindows[windowID].SetActive(true);
    }

    public void closeBankWindow(int windowID)
    {
        Debug.Log("Se cierra ventana " + windowID);
        bankWindows[windowID].SetActive(false);

        if (windowID == 0)
        {
            ResumeGame();
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
        //Set values in UI
        if(DataManager.Instance.activeEvents.Count != 0)
        {
            eventNavigationNumber = 0;
            UpdateEventWindow(eventNavigationNumber);
            openWindow(4);

            if (noClose)
            {
                var button = _windows[4].transform.GetChild(0).GetComponent<Button>();
                button.interactable = false;
                PauseGame();
            }
        }
    }

    public void UpdateEventWindow(int index)
    {
        eventPanel.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.GetCurrentEvent(index).title;
        eventPanel.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.GetCurrentEvent(index).body;

        _options[0].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.GetCurrentEvent(index).title1;
        _options[0].transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.GetCurrentEvent(index).body1;

        _options[1].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.GetCurrentEvent(index).title2;
        _options[1].transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.GetCurrentEvent(index).body2;

        _options[2].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.GetCurrentEvent(index).title3;
        _options[2].transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.GetCurrentEvent(index).body3;

        Debug.Log("Evento: " + DataManager.Instance.GetCurrentEvent(0).title);
    }

    public void closeEventWindow(bool noClose)
    {
        closeWindow(4);

        if (noClose)
        {
            var button = _windows[4].transform.GetChild(0).GetComponent<Button>();
            button.interactable = true;
            ResumeGame();
        }
    }

    public void onClickOption(int index)
    {
        OptionClass option = GetOption(index);
        Debug.Log("Se eligio " + option.ID);

        //Change stats
        GameManager.Instance.onOptionSelected(option);
        DataManager.Instance.RemoveEventFromActiveList(eventNavigationNumber);
        UpdateActiveEventsNumber(DataManager.Instance.activeEvents.Count);
        //Show result window

        //Close event Window and result window
        closeEventWindow(true);

        //Animation

        //Update sliders and money counter
        UpdateSliders();

        moneyStatText.text = "S/." + GameManager.Instance.money.ToString() + ".00";

    }

    public void UpdateSliders()
    {
        _sliders[0].value = GameManager.Instance.work;
        _sliders[1].value = GameManager.Instance.health;
        _sliders[2].value = GameManager.Instance.socials;

        _slidersTexts[0].text = GameManager.Instance.work.ToString() + "%";
        _slidersTexts[1].text = GameManager.Instance.work.ToString() + "%";
        _slidersTexts[2].text = GameManager.Instance.work.ToString() + "%";
    }

    public OptionClass GetOption(int index)
    {
        OptionClass option = new OptionClass();

        if(index == 0)
        {
            option.ID = index;
            option.title = DataManager.Instance.GetCurrentEvent(0).title1;
            option.body = DataManager.Instance.GetCurrentEvent(0).body1;

            option.hapiness = DataManager.Instance.GetCurrentEvent(0).hapiness1;
            option.work = DataManager.Instance.GetCurrentEvent(0).work1;
            option.health = DataManager.Instance.GetCurrentEvent(0).health1;
            option.social = DataManager.Instance.GetCurrentEvent(0).social1;
            option.money = DataManager.Instance.GetCurrentEvent(0).money1;
        }
        else if(index == 1)
        {
            option.ID = index;
            option.title = DataManager.Instance.GetCurrentEvent(0).title2;
            option.body = DataManager.Instance.GetCurrentEvent(0).body2;

            option.hapiness = DataManager.Instance.GetCurrentEvent(0).hapiness2;
            option.work = DataManager.Instance.GetCurrentEvent(0).work2;
            option.health = DataManager.Instance.GetCurrentEvent(0).health2;
            option.social = DataManager.Instance.GetCurrentEvent(0).social2;
            option.money = DataManager.Instance.GetCurrentEvent(0).money2;
        }
        else if(index == 2)
        {
            option.ID = index;
            option.title = DataManager.Instance.GetCurrentEvent(0).title3;
            option.body = DataManager.Instance.GetCurrentEvent(0).body3;

            option.hapiness = DataManager.Instance.GetCurrentEvent(0).hapiness3;
            option.work = DataManager.Instance.GetCurrentEvent(0).work3;
            option.health = DataManager.Instance.GetCurrentEvent(0).health3;
            option.social = DataManager.Instance.GetCurrentEvent(0).social3;
            option.money = DataManager.Instance.GetCurrentEvent(0).money3;
        }

        return option;
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
