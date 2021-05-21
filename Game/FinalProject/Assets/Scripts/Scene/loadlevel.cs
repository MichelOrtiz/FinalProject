using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class loadlevel : MonoBehaviour
{
    public int iLevelToLoad;
    [SerializeField]private GameObject startPosition;
    [SerializeField]private Transform loadPosition;
    public static loadlevel instance = null;
    private void Start() {
        if(SceneController.instance.prevScene == iLevelToLoad){
            if(loadPosition!=null){
                startPosition.transform.position = loadPosition.position;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            LoadScene();
        }
    }

    void LoadScene(){
        SceneController.instance.LoadScene(iLevelToLoad);
        //Camera.instance.gameObject.SetActive(true);
    }
}
