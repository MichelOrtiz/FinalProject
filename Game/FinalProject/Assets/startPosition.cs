using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class startPosition : MonoBehaviour
{
    
    public string  currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        StartPointsMove();
    }

    void Update()
    {
        
    }
    
    void StartPointsMove(){
        Debug.Log(currentScene);
    }
}
