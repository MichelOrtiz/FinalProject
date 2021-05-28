using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerManager player;
    public int forcedSlot;//DELETE THIS
    public float radius = 1f;
    private void Start() {
        player = PlayerManager.instance;
    }
    void Update()
    {
        float distance = Vector2.Distance(player.GetPosition(),transform.position);
        if(Input.GetKeyDown(KeyCode.E) && distance<=radius){
            SaveProgres();
        }
    }
    void SaveProgres(){
        SaveFile progress = new SaveFile();
        progress.slotFile = forcedSlot;
        progress.chestItems = null;
        progress.inventoy = Inventory.instance.items.ToArray();
        progress.prefab = null;
        progress.sceneToLoad = SceneController.instance.currentScene;
        progress.positionSpawn = transform.position;
        SaveFilesManager.instance.WriteSaveFile(progress,Application.dataPath + "/Partida" +forcedSlot);
    }
}
