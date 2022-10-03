using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    public Slider loadingBar;
    public GameObject loaderCanvas;
    public TMPro.TextMeshProUGUI textProgress;
    public string player;
    public AsyncOperation scene;

    public bool flag = false;

    private float progress;

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
    public async void LoadScene(string sceneName)
    {
        enabled = true;
        progress = 0;
        loadingBar.value = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(500);
            progress = scene.progress;
        } while (scene.progress < 0.9f);

        progress = 1;

        this.scene = scene;
        flag = true;
    }

    public void GoToMenu()
    {
        var scene = SceneManager.LoadSceneAsync("MenuScene");
        scene.allowSceneActivation = true;
    }

    public void StartGame()
    {
        if(flag)
        {
            scene.allowSceneActivation = true;
            loaderCanvas.SetActive(false);
            enabled = false;
        }
    }
    void Start()
    {
        flag = false;
        enabled = false;
    }
    void Update()
    {
        loadingBar.value = Mathf.MoveTowards(loadingBar.value, progress, 3 * Time.deltaTime);
        textProgress.text = Mathf.FloorToInt(Mathf.MoveTowards(loadingBar.value, progress, 3 * Time.deltaTime) * 100) + "%";
        if (textProgress.text == "100%")
        {
            MenuManager.Instance.textPlay.gameObject.SetActive(true);
        }
    }
}
