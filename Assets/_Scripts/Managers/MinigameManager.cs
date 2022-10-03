using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    public AnimationManagerScript animationScript;

    [SerializeField]
    private Button[] _options;

    public TMPro.TextMeshProUGUI type;

    private float _reactionTime = 1.5f;
    private float _windowTime;
    private int _type;
    private bool gameStarted = false;

    [SerializeField]
    private Button _retryButton;

    [SerializeField]
    private Button _closeButton;

    //private bool win = false;
    private float prize = 0;

    private IEnumerator _triggerCoroutine = null;
    private IEnumerator _waitCoroutine = null;

    private float _effort = 0;

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
        _reactionTime = 1.5f;
        animationScript.setCounterState(0);
        _options[0].onClick.AddListener(() =>
        {
            //Debug.Log("1");
            OnButtonClicked(0);
        });
        _options[1].onClick.AddListener(() =>
        {
            //Debug.Log("1");
            OnButtonClicked(1);
        });
        _options[2].onClick.AddListener(() =>
        {
            //Debug.Log("1");
            OnButtonClicked(2);
        });
        _options[3].onClick.AddListener(() =>
        {
            //Debug.Log("1");
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
        enableButtons(false);

        //State 0

        //Animation 3..2..1
        //GO!
        SoundManager.Instance.FadeInBGM();
        //State 1
        StartGame();
    }

    void StartGame()
    {
        CalculatePrize();

        //Debug.Log("ANIMATOR: ", animationScript.CounterAnimator);
        //State 1 - Starting waiting time

        //Animation count down
        animationScript.setCounterState(0);
        foreach(Button option in _options)
        {
            option.transform.GetChild(0).gameObject.SetActive(false);
        }
        
        //_windowTime = (Random.Range(40, 80))/10f;
        _windowTime = 3.5f;
        _reactionTime = 1.5f;
        _type = Random.Range(0, 4);

        //Debug.Log(_windowTime + " - " + _type);

        gameStarted = true;
        

        _waitCoroutine = WaitTime();
        _triggerCoroutine = TriggerItem();

        //State 2
        //MakeSound();
        StartCoroutine(_triggerCoroutine);
    }

    public void CalculatePrize()
    {
        _effort = UIManager.Instance._workSlider.value;
        Debug.Log("Esfuerzo: " + _effort);
        int prob = Random.Range(0, 100);

        if (_effort < 0.25)
        {
            prize = 600;
        }
        else if(_effort < 0.50)
        {
            prize = 1025;
            if (prob > 75)
            {
                GameManager.Instance.changeStats(0, 0, -10, 0, 0);
            }
        }
        else if(_effort < 0.75)
        {
            prize = 5000;
            if (prob < 10)
            {
                GameManager.Instance.changeStats(0, 0, -10, 0, 0);
            }
            else if(prob < 40)
            {
                GameManager.Instance.changeStats(0, 0, -15, 0, 0);
            }
            else if(prob < 60)
            {
                GameManager.Instance.changeStats(0, 0, -20, 0, 0);
            }
        }
        else if(_effort <= 1)
        {
            prize = 10000;

            if (prob < 25)
            {
                GameManager.Instance.changeStats(0, 0, -30, 0, 0);
            }
            else if (prob < 60)
            {
                GameManager.Instance.changeStats(0, 0, -35, 0, 0);
            }
            else if (prob < 85)
            {
                GameManager.Instance.changeStats(0, 0, -40, 0, 0);
            }
            else if (prob < 94)
            {
                GameManager.Instance.changeStats(0, 0, -40, 0, 0);
            }
            else if (prob < 95)
            {
                GameManager.Instance.changeStats(0, 0, -50, 0, 0);
            }
        }

        UIManager.Instance.UpdateSliders();
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
        SoundManager.Instance.FadeOutBGM();
    }

    void OnButtonClicked(int type)
    {
        _options[type].transform.GetChild(0).gameObject.SetActive(true);
        _options[_type].transform.GetChild(0).gameObject.SetActive(true);

        if (type == _type)
        {
            _options[type].transform.GetChild(0).GetComponent<Image>().color = new Color32(55, 107, 164, 255);
            
            //WIN
            animationScript.setCounterState(3);
            Debug.Log("Subidon");
            SoundManager.Instance.playMinigameWin();
            gameStarted = false;
            enableButtons(false);
            //win = true;
            GameManager.Instance.changeStats(prize, 0, 0, 0, 0);
            prize = 0;
        }
        else
        {
            _options[type].transform.GetChild(0).GetComponent<Image>().color = new Color32(242, 86, 6, 255);
            _options[_type].transform.GetChild(0).GetComponent<Image>().color = new Color32(55, 107, 164, 255);
            //LOSE
            animationScript.setCounterState(4);
            SoundManager.Instance.playMiniGameLoss();
            gameStarted = false;
            enableButtons(false);
            //win = false;
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
        enableButtons(true);
        //State 2 - Waiting trigger seconds
        //Debug.Log("BANG");

        if(gameStarted)
        {
            //State 3 - Playing sound
            SoundManager.Instance.PlayMinigameSound(_type);
            animationScript.setCounterState(2);
            Debug.Log(_type);
            type.text = _type.ToString();
            StartCoroutine(_waitCoroutine);
        }
    }

    private void MakeSound()
    {
        if (gameStarted)
        {
            //State 3 - Playing sound
            SoundManager.Instance.PlayMinigameSound(_type);
            animationScript.setCounterState(2);
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
            SoundManager.Instance.playMiniGameLoss();
            animationScript.setCounterState(4);
            Debug.Log("LOSE");
            //win = false;
            EndGame();
        }
    }
}
