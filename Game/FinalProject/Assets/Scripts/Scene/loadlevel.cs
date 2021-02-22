using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadlevel : MonoBehaviour
{
    public int iLevelToLoad;
    public static loadlevel instance = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    /*void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }*/
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            LoadScene();
        }
    }

    void LoadScene(){
        SceneManager.LoadScene(iLevelToLoad);
        //Camera.instance.gameObject.SetActive(true);
    }
}
