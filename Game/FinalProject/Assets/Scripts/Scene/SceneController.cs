using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int prevScene { get; set; }
    public int currentScene { get; set; }
    [SerializeField]private GameObject playerPrefab;
    SceneManager manager;

    public static SceneController instance;

    private void Awake() {
        if(instance == null) instance = this;
        else if (instance != this) Destroy(this);
        DontDestroyOnLoad(this);
    }
    private void Start() {
        if(currentScene==0)
        currentScene = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Start scenecontroller");
    }
    public void LoadScene(int scene){
        prevScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
        currentScene = scene;
    }
    public void Load(SaveFile partida){
        LoadScene(partida.sceneToLoad);
    }
}
