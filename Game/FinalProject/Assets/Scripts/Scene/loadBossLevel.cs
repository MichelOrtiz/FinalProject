using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadBossLevel : loadlevel
{
    public WorldState condition;
    SaveFilesManager saveFilesManager;

    new void OnTriggerEnter2D(Collider2D collision) {
        GameObject collisionGameObject = collision.gameObject;
        saveFilesManager = SaveFilesManager.instance;
        if(saveFilesManager != null){
            if(saveFilesManager.currentSaveSlot != null){
                if(saveFilesManager.currentSaveSlot.WorldStates.Contains(condition)){
                    WorldState c = saveFilesManager.currentSaveSlot.WorldStates.Find(s => s.id == condition.id);
                    if(c.state){
                        //Ejecutar algo cuando el jefe ya haya sido derrotado
                        Debug.Log("Jefe ya habia sido derrotado");
                        //hacer cosas siguiente zona?
                    }else{
                        //Ejecutar batalla normal
                        Debug.Log("Caso 1");
                        if (collisionGameObject.tag == "Player")LoadScene();
                    }
                }else{
                    Debug.Log("Caso 2");
                    saveFilesManager.currentSaveSlot.WorldStates.Add(condition);
                    if (collisionGameObject.tag == "Player")LoadScene();
                }
            }else{
                Debug.Log("No hay una partida seleccionada... de seguro estar testeando pase buen hombre");
                if (collisionGameObject.tag == "Player")LoadScene();
            }
        }else{
            Debug.Log("OMG where's the lamb sauce (no hay SaveFilesManager)");
        }
    }
}
