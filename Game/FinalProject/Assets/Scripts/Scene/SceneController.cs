using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; 
using TMPro;
using FinalProject.Assets.Scripts.Utils.Sound;
using FinalProject.Assets.Scripts.Scene;

public class SceneController : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;
    public GameObject loadingScreen;
    public GameObject mainCanvas;

    public int prevScene { get; set; }
    public int currentScene { get; set; }
    public int altDoor;
    [SerializeField]private GameObject playerPrefab;
    SceneManager manager;
    public delegate void onSceneChange();
    public onSceneChange SceneChanged;
    public static SceneController instance;


    private SceneTitle sceneTitle;

    private void Awake() {
        if(instance == null) instance = this;
        else if (instance != this) Destroy(this);
        DontDestroyOnLoad(this);
    }
    private void Start() {
        if(currentScene==0)
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneChanged?.Invoke();

        sceneTitle = FindObjectOfType<SceneTitle>();
        //Debug.Log("Start scenecontroller");

    }
    public void LoadScene(int scene){
        sceneTitle?.gameObject?.SetActive(false);

        prevScene = SceneManager.GetActiveScene().buildIndex;
        currentScene = scene;
        //SceneChanged?.Invoke();
        //SceneManager.LoadScene(scene);
        StartCoroutine(LoadAsynchronously(scene));



    }
    public void Load(SaveFile partida){
        //Instantiate(playerPrefab);
        LoadScene(partida.sceneToLoad);
    }



    IEnumerator LoadAsynchronously (int sceneInd)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneInd);

        loadingScreen.SetActive(true);
        //mainCanvas.SetActive(false);
        while (!operation.isDone)
        {
            float progress = Mathf. Clamp01(operation.progress / .9f);
            slider.value = progress;
            text. text = progress * 100f + "%";
            yield return null;
        }
        loadingScreen.SetActive(false);
        
        sceneTitle = FindObjectOfType<SceneTitle>();
        SceneChanged?.Invoke();
        //mainCanvas.SetActive(true);
    }
}
