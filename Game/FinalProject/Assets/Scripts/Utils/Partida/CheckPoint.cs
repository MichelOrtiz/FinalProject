using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerManager player;
    //public int forcedSlot;//DELETE THIS
    public float radius = 1f;
    public GameObject signInter;
    public GameObject popUp;
    private void Start() {
        player = PlayerManager.instance;
    }
    void Update()
    {
        float distance = Vector2.Distance(player.GetPosition(),transform.position);
        if(distance<=radius){
            player.inputs.Interact -= Save;
            player.inputs.Interact += Save;
            signInter?.SetActive(true);
        }else{
            player.inputs.Interact -= Save;
            signInter?.SetActive(false);
        }
    }
    void Save(){
        Show();
        Invoke("Hide", 2f);
        SaveFile progress = SaveFilesManager.instance.currentSaveSlot;
        progress.inventory = Inventory.instance.items.ToArray();
        progress.money = Inventory.instance.GetMoney();
        progress.controlBindsKeys = KeybindManager.instance.controlbinds.Keys.ToList<string>();
        progress.controlBindsValues = KeybindManager.instance.controlbinds.Values.ToList<KeyCode>();
        if(Cofre.instance.savedItems != null){
            progress.chestItems = Cofre.instance.savedItems.ToArray();
        }else{
            progress.chestItems = null;
        }
        progress.sceneToLoad = SceneController.instance.currentScene;
        progress.positionSpawn = transform.position;
        SaveFilesManager.instance.SaveProgress();
        Debug.Log("Guardando partida");
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void Show(){
        popUp.SetActive(true);
    }
    void Hide(){
        popUp.SetActive(false);
    }
}
