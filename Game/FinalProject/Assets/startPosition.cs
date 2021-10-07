using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class startPosition : MonoBehaviour
{
    
    public string  currentScene;
    void Start()
    {
        if (SaveFilesManager.instance != null && SaveFilesManager.instance.currentSaveSlot != null && SaveFilesManager.instance.currentSaveSlot.positionSpawn == new Vector2(0f, 0f))
        {
            //SaveFilesManager.instance.currentSaveSlot.positionSpawn = transform.position;
        }
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
