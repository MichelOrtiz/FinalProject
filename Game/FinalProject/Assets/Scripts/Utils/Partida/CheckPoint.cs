using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerManager player;
    //public int forcedSlot;//DELETE THIS
    public float radius = 1f;
    private void Start() {
        player = PlayerManager.instance;
    }
    void Update()
    {
        float distance = Vector2.Distance(player.GetPosition(),transform.position);
        if(Input.GetKeyDown(KeyCode.E) && distance<=radius){
            Save();
        }
    }
    void Save(){
        SaveFile progress = SaveFilesManager.instance.currentSaveSlot;
        progress.inventory = Inventory.instance.items.ToArray();
        //progress.controlbinds = KeybindManager.instance.controlbinds;
        progress.controlBindsKeys = KeybindManager.instance.controlbinds.Keys.ToList<string>();
        progress.controlBindsValues = KeybindManager.instance.controlbinds.Values.ToList<KeyCode>();
        if(Cofre.instance.savedItems != null){
            progress.chestItems = Cofre.instance.savedItems.ToArray();
        }else{
            progress.chestItems = null;
        }
        
        progress.prefab = null;
        progress.sceneToLoad = SceneController.instance.currentScene;
        progress.positionSpawn = transform.position;
        SaveFilesManager.instance.SaveProgress();
        Debug.Log("Guardando partida");
    }
}
