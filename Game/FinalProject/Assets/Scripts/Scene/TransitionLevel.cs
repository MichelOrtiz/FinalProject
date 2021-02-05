using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionLevel : MonoBehaviour
{
    public int iLevelToLoad;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(iLevelToLoad);
    }
}
