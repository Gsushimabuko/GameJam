using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    [SerializeField]
    private Button[] _options;

    public TMPro.TextMeshProUGUI type;

    private float _reactionTime = 2f;
    private float _windowTime;
    private int _type;
    private bool gameStarted = false;
    private bool win = false;

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
        _options[0].onClick.AddListener(() =>
        {
            OnButtonClicked(0);
        });
        _options[1].onClick.AddListener(() =>
        {
            OnButtonClicked(1);
        });
        _options[2].onClick.AddListener(() =>
        {
            OnButtonClicked(2);
        });
        _options[3].onClick.AddListener(() =>
        {
            OnButtonClicked(3);
        });

        enableButtons(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void enableButtons(bool state)
    {
        _options[0].interactable = state;
        _options[1].interactable = state;
        _options[2].interactable = state;
        _options[3].interactable = state;
    }

    public void OnGameStarted()
    {
        Debug.Log("GAME STAATO");
        

        //Disable other inputs
        //Countdown
        //Start game
        StartGame();

        //Play sound

        
        //Wait for input
        //Result
    }

    void StartGame()
    {
        _windowTime = (Random.Range(20, 80))/10f;
        _type = Random.Range(0, 3);
        Debug.Log(_windowTime + " - " + _type);
        gameStarted = true;
        enableButtons(true);

        StartCoroutine(TriggerItem());
    }

    void EndGame()
    {
        _type = -1;
        gameStarted = false;
        enableButtons(false);
        UIManager.Instance.EndMinigame();
    }

    void OnButtonClicked(int type)
    {
        if(type == _type)
        {
            //WIN
            Debug.Log("Subidon");
            gameStarted = false;
            enableButtons(false);
            
        }
        else
        {
            //LOSE
            Debug.Log("Bajon");
            gameStarted = false;
            enableButtons(false);
        }
    }

    IEnumerator TriggerItem()
    {
        yield return new WaitForSeconds(_windowTime);
        Debug.Log("BANG");

        if(gameStarted)
        {
            SoundManager.Instance.PlayMinigameSound(_type);
            Debug.Log(_type);
            type.text = _type.ToString();
            StartCoroutine(WaitTime());
        }
        

    }

    void QuickTimeEvent()
    {
        Task.Run(() => { StartCoroutine(WaitTime()); });
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(_reactionTime);
        Debug.Log("aa");
        if (gameStarted)
        {
            enableButtons(false);
            win = false;
            Debug.Log("LOSE");
        }
    }
}
