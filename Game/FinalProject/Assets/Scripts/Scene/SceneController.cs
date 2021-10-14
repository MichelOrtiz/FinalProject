using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int prevScene { get; set; }
    public int currentScene { get; set; }
    public int altDoor;
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
        currentScene = scene;
        SceneManager.LoadScene(scene);
        
    }
    public void Load(SaveFile partida){
        //Instantiate(playerPrefab);
        LoadScene(partida.sceneToLoad);
        
    }
}
