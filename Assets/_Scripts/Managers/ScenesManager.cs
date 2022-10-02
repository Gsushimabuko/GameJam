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
            await Task.Delay(100);
            progress = scene.progress;
        } while (scene.progress < 0.9f);

        progress = 1;

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
        enabled = false;
    }
    void Start()
    {
        enabled = false;
    }
    void Update()
    {
        loadingBar.value = Mathf.MoveTowards(loadingBar.value, progress, 3 * Time.deltaTime);
        textProgress.text = Mathf.FloorToInt(Mathf.MoveTowards(loadingBar.value, progress, 3 * Time.deltaTime) * 100) + "%";
    }
}
