using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadBossLevel : loadlevel
{
    public WorldState worldState = new WorldState();
    SaveFilesManager saveFilesManager;

    new private void Start() {
        if(SaveFilesManager.instance != null && SaveFilesManager.instance.currentSaveSlot != null){
            SaveFile partida = SaveFilesManager.instance.currentSaveSlot;
            foreach(WorldState w in partida.WorldStates){
                if(w.id == worldState.id){
                    worldState = w;
                    if(w.state){
                        Destroy(gameObject);
                        break;
                    }else{
                        worldState = w;
                        break;
                    }
                }
            }
            partida.WorldStates.Add(worldState);
        }
        if (SceneController.instance != null)
        {
            PlayerManager.instance.physics.ResetAll();
            if(PlayerManager.instance.isDeath){
                    Debug.Log("Cargando en el ultimo checkpoint");

                    PlayerManager.instance.isDeath = false;
                    PlayerManager.instance.gameObject.transform.position = SaveFilesManager.instance.currentSaveSlot.positionSpawn;

            }else{
                if(SceneController.instance.prevScene != 34 && SceneController.instance.prevScene == iLevelToLoad){
                        if(loadPosition!=null && !PlayerManager.instance.isDeath){
                            PlayerManager.instance.gameObject.transform.position = loadPosition.position;
                            //Jugador salio de la sala del jefe... entonces lo derroto no?
                            worldState.state = true;
                            Destroy(gameObject);
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

}
