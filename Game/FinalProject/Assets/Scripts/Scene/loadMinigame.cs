using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMinigame : loadlevel
{
    bool isAvailable;
    [SerializeField] float cooldownTime;
    float curretCooldownTime;
    protected override void Start(){
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
    protected override void cargarEscena()
    {
        if(!isAvailable)return;
        base.cargarEscena();
    }
}
