using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class loadlevel : MonoBehaviour
{
    public int iLevelToLoad;
    [SerializeField]public GameObject startPosition;
    [SerializeField]private Transform loadPosition;
    public static loadlevel instance = null;
    public Vector2 coords;
    protected PlayerManager player;

    private void Start() {
        if(SceneController.instance.prevScene == iLevelToLoad){
            if(loadPosition!=null){
                PlayerManager.instance.gameObject.transform.position = loadPosition.position;
            }
        }
        startPosition.gameObject.transform.position = coords;
    }
    private void Awake()
        {
            instance = this;
        }
    private void Update()
        {
            if (instance==null)
            {
                instance = this;
            }
        }
    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        /*if (collisionGameObject.tag == "Untagged")
        {
            PlayerManager.instance.dodgePerectCollider.gameObject.layer = LayerMask.NameToLayer("Default");
        }else{
            PlayerManager.instance.dodgePerectCollider.gameObject.layer = LayerMask.NameToLayer("Untagged");
        }*/
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
