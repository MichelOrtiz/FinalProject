using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class loadlevel : MonoBehaviour
{
    public int iLevelToLoad;
    public int noDoor = 1;
    [SerializeField]protected Transform loadPosition;
    protected PlayerManager player;

    protected virtual void Start() {
        if (SceneController.instance != null)
        {
            PlayerManager.instance.physics.ResetAll();
            if(PlayerManager.instance.isDeath){
                    Debug.Log("Cargando en el ultimo checkpoint");
                    PlayerManager.instance.gameObject.transform.position = SaveFilesManager.instance.currentSaveSlot.positionSpawn;
                    PlayerManager.instance.RestoreValuesForDead();
            }else{
                if(SceneController.instance.prevScene != 0 && SceneController.instance.prevScene == iLevelToLoad){
                        if(loadPosition!=null && !PlayerManager.instance.isDeath && SceneController.instance.altDoor == noDoor){
                            PlayerManager.instance.gameObject.transform.position = loadPosition.position;
                        }
                    }
                    else{
                        //if loading from 0 spawnpoint = startPosition
                        if(SceneController.instance.prevScene == 0){
                            Debug.Log("Cargando desde main menu");
                            PlayerManager.instance.gameObject.transform.position = SaveFilesManager.instance.currentSaveSlot.positionSpawn;
                        }
                    }
            }
            
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
            PlayerManager.instance.inputs.Interact -= cargarEscena;
            PlayerManager.instance.inputs.Interact += cargarEscena;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            PlayerManager.instance.inputs.Interact -= cargarEscena;
        }
    }

    protected void LoadScene(){
        SceneController.instance.altDoor = noDoor;
        SceneController.instance.LoadScene(iLevelToLoad);

        //Camera.instance.gameObject.SetActive(true);
    }
    protected void RestoreValuesForOtherScene(){
        PlayerManager.instance.isInWater = false;
        PlayerManager.instance.isInDark = false;
        PlayerManager.instance.isInSnow = false;  
        PlayerManager.instance.isInIce = false;
        PlayerManager.instance.walkingSpeed = PlayerManager.defaultwalkingSpeed;
        PlayerManager.instance.currentGravity = PlayerManager.defaultGravity;
        PlayerManager.instance.ResetAnimations();
        //PlayerManager.instance.statesManager.StopAll();
    }
    protected virtual void cargarEscena(){
        
        PlayerManager.instance.inputs.Interact -= cargarEscena; 
        LoadScene();
        RestoreValuesForOtherScene();
    }
}
