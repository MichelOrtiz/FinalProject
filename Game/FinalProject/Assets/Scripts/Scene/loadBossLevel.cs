using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadBossLevel : loadlevel
{
    public WorldState worldState = new WorldState();
    SaveFilesManager saveFilesManager;

    protected override void Start() {
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
            if(!partida.WorldStates.Exists(x => x.id == worldState.id)){
                partida.WorldStates.Add(worldState);
            }
        }
        if (SceneController.instance != null)
        {
            PlayerManager.instance.physics.ResetAll();
            if(SceneController.instance.prevScene != 0 && SceneController.instance.prevScene == iLevelToLoad){
                if(loadPosition!=null && !PlayerManager.instance.isDeath){
                    PlayerManager.instance.gameObject.transform.position = loadPosition.position;
                    //Jugador salio de la sala del jefe... entonces lo derroto no?
                    //worldState.state = true;
                    //Destroy(gameObject);
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
