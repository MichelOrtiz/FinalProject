using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMinigame : loadlevel
{
    bool isAvailable;
    [SerializeField] float cooldownTime;
    float curretCooldownTime;
    [SerializeField] private GameObject signCooldown;
    protected override void Start(){
        signCooldown?.SetActive(false);
        interSign?.SetActive(false);
        PlayerManager.instance.physics.ResetAll();
        isAvailable = true;
            if(PlayerManager.instance.isDeath){
                    Debug.Log("Cargando en el ultimo checkpoint");
                    PlayerManager.instance.gameObject.transform.position = SaveFilesManager.instance.currentSaveSlot.positionSpawn;
                    PlayerManager.instance.RestoreValuesForDead();
            }else{
                if(SceneController.instance.prevScene != 0 && SceneController.instance.prevScene == iLevelToLoad){
                        if(loadPosition!=null && !PlayerManager.instance.isDeath && SceneController.instance.altDoor == noDoor){
                            PlayerManager.instance.gameObject.transform.position = loadPosition.position;
                            isAvailable = false;
                            curretCooldownTime = 0;
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
    private void Update() {
        if(!isAvailable){
            curretCooldownTime += Time.deltaTime;
            if(curretCooldownTime >= cooldownTime){
                isAvailable = true;
                curretCooldownTime = 0;
            }
        }
    }

    protected new void OnTriggerEnter2D(Collider2D other)
    {
        if (isAvailable)
        {
            base.OnTriggerEnter2D(other);
            signCooldown?.SetActive(false);
        }
        else if (other.gameObject.tag == "Player")
        {
            signCooldown?.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isAvailable)
        {
            base.OnTriggerEnter2D(other);
            signCooldown?.SetActive(false);
        }
        else if (other.gameObject.tag == "Player")
        {
            signCooldown?.SetActive(true);
        }
    }

    protected new void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.gameObject.tag == "Player")
        {
            signCooldown?.SetActive(false);
        }
    }


    protected override void cargarEscena()
    {
        if(!isAvailable)return;
        base.cargarEscena();
    }
    void OnDestroy() {
        PlayerManager.instance.inputs.Interact -= cargarEscena; 
    }
}
