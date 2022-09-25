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
    public TMPro.TextMeshProUGUI moneyStatText;
    public Text moneyStat;

    [SerializeField]
    private Button[] _options;

    [SerializeField]
    private Button startMinigameButton;

    [SerializeField]
    private GameObject eventPanel;

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
        this.moneyStatText.text = gameManager.getMoney().ToString();
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
        //GameManager.Instance.currentEvent;
        //Set values in UI
        UpdateEventWindow();
        openWindow(4);
        
        if(noClose)
        {
            var button = _windows[4].transform.GetChild(0).GetComponent<Button>();
            button.interactable = false;
            PauseGame();
        }
    }

    public void UpdateEventWindow()
    {
        eventPanel.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.currentEvent.title;
        eventPanel.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.currentEvent.body;

        _options[0].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.currentEvent.title1;
        _options[0].transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.currentEvent.body1;

        _options[1].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.currentEvent.title2;
        _options[1].transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.currentEvent.body2;

        _options[2].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.currentEvent.title3;
        _options[2].transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = DataManager.Instance.currentEvent.body3;

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
