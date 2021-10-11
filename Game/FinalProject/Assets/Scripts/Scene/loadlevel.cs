using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class loadlevel : MonoBehaviour
{
    public int iLevelToLoad;
    [SerializeField]private Transform loadPosition;
    public static loadlevel instance = null;
    protected PlayerManager player;

    private void Start() {
        if (SceneController.instance != null)
        {
            if(PlayerManager.instance.isDeath){
                    Debug.Log("Cargando en el ultimo checkpoint");
                    PlayerManager.instance.physics.ResetAll();

                    PlayerManager.instance.isDeath = false;
                    PlayerManager.instance.gameObject.transform.position = SaveFilesManager.instance.currentSaveSlot.positionSpawn;

            }else{
                if(SceneController.instance.prevScene != 34 && SceneController.instance.prevScene == iLevelToLoad){
                        if(loadPosition!=null && !PlayerManager.instance.isDeath){
                            PlayerManager.instance.gameObject.transform.position = loadPosition.position;
                        }
                    }
                    else{
                        //if loading from 34 spawnpoint = startPosition
                        if(SceneController.instance.prevScene == 34){
                            Debug.Log("Cargando desde main menu");
                            PlayerManager.instance.gameObject.transform.position = SaveFilesManager.instance.currentSaveSlot.positionSpawn;
                        }
                    }
            }
            
        }

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
    protected void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        /*if (collisionGameObject.tag == "Untagged")
        {
            PlayerManager.instance.dodgePerectCollider.gameObject.layer = LayerMask.NameToLayer("Default");
        }else{
            PlayerManager.instance.dodgePerectCollider.gameObject.layer = LayerMask.NameToLayer("Untagged");
        }*/
        if (collisionGameObject.tag == "Player")
        {
            RestoreValuesForOtherScene();
            LoadScene();
        }
    }

    protected void LoadScene(){
        SceneController.instance.LoadScene(iLevelToLoad);
        //Camera.instance.gameObject.SetActive(true);
    }
    protected void RestoreValuesForOtherScene(){
        PlayerManager.instance.walkingSpeed = PlayerManager.defaultwalkingSpeed;
        PlayerManager.instance.currentGravity = PlayerManager.defaultGravity;
        PlayerManager.instance.isInWater = false;
        PlayerManager.instance.isInDark = false;
        PlayerManager.instance.isInSnow = false;  
        PlayerManager.instance.isInIce = false;
        PlayerManager.instance.ResetAnimations();
    }
}
