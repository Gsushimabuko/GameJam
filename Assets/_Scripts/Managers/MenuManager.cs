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

    public Button logIn;

    public GameObject bgImage;
    public GameObject input;

    public string player;

    public string[] hints = { "Si eres de los que no le daban bola a los ferrocarriles y las utilidades en Monopolio, deberías considerar tenerle un poquito más de fe a la segurudad que te dan los ingresos pasivos en la vida.", "Cuando una fuente de ingresos es pasiva, significa que requiere muy poco o ningún mantenimiento para hacer que el dinero siga fluyendo. Esto no significa que puedas ignorarlo. Al contrario, es importante que lleves un registro de todas tus fuentes de ingreso pasivo y que las monitorees como halcón, sin importar cuán automatizado esté.", "Tú no lo sabías, pero cuando tu mamá hacía su lista para el mercado semanal, en realidad era una referencia a que un día la vida te iba a pedir llevar un registro de tus gastos fijos y prescindibles. Por eso no te compraba tus papas Pringles todas las semanas.", "Si te sentías culpable por aceptar siempre los términos y condiciones sin leer las letras chiquitas, que el momento de cambiar sea cuando tomes un préstamo. Vivir endeudado es caer más profundo que Alianza en la Libertadores.",
    "Quizás ninguno de tus amigos te lo dijo antes, pero apostar todo al negro en el casino no es una inversión."};

    public TMPro.TextMeshProUGUI textPlay;
    public TMPro.TextMeshProUGUI hintText;
    public Button pressPlay;

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
        bgImage.SetActive(false);

        textPlay.gameObject.SetActive(false);
        pressPlay.onClick.AddListener(() =>
        {
            ScenesManager.Instance.StartGame();
        });

        pressToStart.onClick.AddListener(() =>
        {
            FadeOutStart();
        });

        logIn.onClick.AddListener(() =>
        {
            input.GetComponent<InputBehaviour>().StoreName();
            player = input.GetComponent<InputBehaviour>().player;
            if (!string.IsNullOrWhiteSpace(player))
            {
                ShowLoadingScreen();
            }
        });
        
        startScreen.GetComponent<CanvasGroup>().alpha = 0;
        nameScreen.GetComponent<CanvasGroup>().alpha = 0;
        loadingScreen.GetComponent<CanvasGroup>().alpha = 0;
        bgImage.GetComponent<CanvasGroup>().alpha = 0;
        FadeInStart();
    }

    public void OnStartScreenFaded()
    {
        bgImage.SetActive(true);
        nameScreen.SetActive(true);
        startScreen.gameObject.SetActive(false);

        StartCoroutine("FadeInNameC");
    }

    public void FadeInStart()
    {
        StartCoroutine("FadeInStartC");
    }

    public void FadeOutStart()
    {
        StartCoroutine("FadeOutStartC");
    }

    IEnumerator FadeInStartC()
    {
        startScreen.GetComponent<CanvasGroup>().alpha = 0;

        do
        {
            startScreen.GetComponent<CanvasGroup>().alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        } 
        while (startScreen.GetComponent<CanvasGroup>().alpha < 1);

        startScreen.GetComponent<CanvasGroup>().alpha = 1;
    }
    IEnumerator FadeOutStartC()
    {
        startScreen.GetComponent<CanvasGroup>().alpha = 1;

        do
        {
            startScreen.GetComponent<CanvasGroup>().alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        while (startScreen.GetComponent<CanvasGroup>().alpha > 0);

        startScreen.GetComponent<CanvasGroup>().alpha = 0;

        OnStartScreenFaded();
    }

    IEnumerator FadeInNameC()
    {
        nameScreen.GetComponent<CanvasGroup>().alpha = 0;
        bgImage.GetComponent<CanvasGroup>().alpha += 0;

        do
        {
            nameScreen.GetComponent<CanvasGroup>().alpha += 0.01f;
            bgImage.GetComponent<CanvasGroup>().alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        while (nameScreen.GetComponent<CanvasGroup>().alpha < 1);

        nameScreen.GetComponent<CanvasGroup>().alpha = 1;
        bgImage.GetComponent<CanvasGroup>().alpha += 1;
    }

    public void FadeOutName()
    {
        StartCoroutine("FadeOutNameC");
    }
    IEnumerator FadeInLoadingC()
    {
        loadingScreen.GetComponent<CanvasGroup>().alpha = 0;
        hintText.text = hints[Random.Range(0, hints.Length)];
        do
        {
            loadingScreen.GetComponent<CanvasGroup>().alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        while (loadingScreen.GetComponent<CanvasGroup>().alpha < 1);

        loadingScreen.GetComponent<CanvasGroup>().alpha = 1; 

        ScenesManager.Instance.LoadScene("SampleScene");
    }

    IEnumerator FadeOutNameC()
    {
        nameScreen.GetComponent<CanvasGroup>().alpha = 1;

        do
        {
            nameScreen.GetComponent<CanvasGroup>().alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        while (nameScreen.GetComponent<CanvasGroup>().alpha > 0);

        nameScreen.GetComponent<CanvasGroup>().alpha = 0;

        StartCoroutine("FadeInLoadingC");
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
        
        FadeOutName();
    }
}
