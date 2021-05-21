using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int prevScene { get; set; }
    public int currentScene { get; set; }
    SceneManager manager;

    public static SceneController instance;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
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
}
