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

    [SerializeField]
    private Button _retryButton;

    [SerializeField]
    private Button _closeButton;

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

        _retryButton.onClick.AddListener(() =>
        {
            OnGameStarted();
        });

        _closeButton.onClick.AddListener(() =>
        {
            CloseGame();
        });

        enableButtons(false);
        _retryButton.interactable = false;
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
        _retryButton.interactable = false;
        _closeButton.interactable = false;

        //Disable other inputs
        //Countdown
        //Start game
        StartGame();
    }

    void StartGame()
    {
        _windowTime = (Random.Range(20, 80))/10f;
        _type = Random.Range(0, 4);
        Debug.Log(_windowTime + " - " + _type);
        gameStarted = true;
        enableButtons(true);

        StartCoroutine(TriggerItem());
    }

    void EndGame()
    {
        //Activate retry button
        _retryButton.interactable = true;
        _closeButton.interactable = true;
        _type = -1;
        gameStarted = false;
        enableButtons(false);
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

        EndGame();
    }

    void CloseGame()
    {
        UIManager.Instance.EndMinigame();
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

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(_reactionTime);
        Debug.Log("aa");
        if (gameStarted)
        {
            enableButtons(false);
            Debug.Log("LOSE");
        }
    }
}
