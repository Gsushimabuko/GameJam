using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject startScreen;
    public Button pressToStart;
    public GameObject nameScreen;
    public GameObject loadingScreen;

    public string player;

    public static MenuManager Instance;

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
        startScreen.gameObject.SetActive(true);
        nameScreen.SetActive(false);
        loadingScreen.SetActive(false);

        pressToStart.onClick.AddListener(() =>
        {
            nameScreen.SetActive(true);
            startScreen.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLoadingScreen()
    {
        startScreen.SetActive(false);
        nameScreen.SetActive(false);
        loadingScreen.SetActive(true);
        ScenesManager.Instance.LoadScene("SampleScene");
    }
}
