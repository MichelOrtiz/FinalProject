using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMachine : MonoBehaviour
{
    PlayerManager player;
    public float radius = 1f;
    private void Start() {
        player = PlayerManager.instance;
    }
    void Update()
    {
        float distance = Vector2.Distance(player.GetPosition(),transform.position);
        if(Input.GetKeyDown(KeyCode.E) && distance<=radius){
            Load();
        }
    }
    void Load(){
        SaveFile partida = SaveFilesManager.instance.LoadSaveFile(Application.dataPath + "/Partida" + SaveFilesManager.instance.currentSaveSlot.slotFile);
        if(partida!=null){
            Inventory.instance.items.Clear();
            for(int i=0;i<partida.inventoy.Length;i++){
                Inventory.instance.Add(partida.inventoy[i]);
            }
            Debug.Log("...cosas se cargaron");
            Debug.Log("DELETE THIS");
        }
    }
}
