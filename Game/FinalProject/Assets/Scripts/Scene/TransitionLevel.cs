using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionLevel : MonoBehaviour
{
    public int iLevelToLoad;
    public string sLevelToLoad;

    public bool useIntrigerToLoadLevel = false;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "Player")
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
       if(useIntrigerToLoadLevel){
             UnityEngine.SceneManagement.SceneManager.LoadScene(iLevelToLoad);
        }
        else
        {
             UnityEngine.SceneManagement.SceneManager.LoadScene(sLevelToLoad);
        }
    }
}
