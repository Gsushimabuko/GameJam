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

    private IEnumerator _triggerCoroutine = null;
    private IEnumerator _waitCoroutine = null;

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
            Debug.Log("1");
            OnButtonClicked(0);
        });
        _options[1].onClick.AddListener(() =>
        {
            Debug.Log("1");
            OnButtonClicked(1);
        });
        _options[2].onClick.AddListener(() =>
        {
            Debug.Log("1");
            OnButtonClicked(2);
        });
        _options[3].onClick.AddListener(() =>
        {
            Debug.Log("1");
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

        //State 0
        //Animation 3..2..1
        //GO!

        //State 1
        StartGame();
    }

    void StartGame()
    {
        //State 1 - Starting waiting time
        _options[_type].transform.GetChild(0).gameObject.SetActive(false);
        _windowTime = (Random.Range(20, 80))/10f;
        _type = Random.Range(0, 4);
        Debug.Log(_windowTime + " - " + _type);
        gameStarted = true;
        enableButtons(true);

        _waitCoroutine = WaitTime();
        _triggerCoroutine = TriggerItem();

        //State 2
        StartCoroutine(_triggerCoroutine);
    }

    void EndGame()
    {
        //Activate retry button
        _retryButton.interactable = true;
        _closeButton.interactable = true;
        gameStarted = false;
        enableButtons(false);

        StopCoroutine(_triggerCoroutine);
        StopCoroutine(_waitCoroutine);
    }

    void OnButtonClicked(int type)
    {
        _options[_type].transform.GetChild(0).gameObject.SetActive(true);

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
        //State 2 - Waiting trigger seconds
        yield return new WaitForSeconds(_windowTime);
        Debug.Log("BANG");

        if(gameStarted)
        {
            //State 3 - Playing sound
            SoundManager.Instance.PlayMinigameSound(_type);
            Debug.Log(_type);
            type.text = _type.ToString();
            StartCoroutine(_waitCoroutine);
        }
        

    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(_reactionTime);
        if (gameStarted)
        {
            //LOSE
            Debug.Log("LOSE");
            EndGame();
        }
    }
}
