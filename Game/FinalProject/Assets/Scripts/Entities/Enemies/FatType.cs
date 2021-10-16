using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatType : Enemy
{
    [SerializeField] private int amount = 1;
    [SerializeField] private Item wishedItem;
    [SerializeField] private GameObject wishedItemIcon;
    [SerializeField] private WorldState worldState = new WorldState();

    protected new void Start()
    {
        base.Start();
        //iconSprite = GetComponent<SpriteRenderer>();
        //wishedItemSprite = wishedItem.icon;
        wishedItemIcon.GetComponent<SpriteRenderer>().sprite = wishedItem.icon;
        if(SaveFilesManager.instance != null && SaveFilesManager.instance.currentSaveSlot != null){
            SaveFile partida = SaveFilesManager.instance.currentSaveSlot;
            foreach(WorldState w in partida.WorldStates){
                if(w.id == worldState.id){
                    worldState = w;
                    if(w.state){
                        Destroy(gameObject);
                        return;
                    }else{
                        worldState = w;
                        return;
                    }
                }
            }
            partida.WorldStates.Add(worldState);
        }
    }

    public override void ConsumeItem(Item item)
    {
        //base.ConsumeItem(item);
        if (item == wishedItem)
        {
            amount--;
            Debug.Log("he liked that");
            if (amount != 0)
            {
                updateVisual();
                return;
            }
            worldState.state = true;
            List<MapSlot> map = FindObjectOfType<MapUI>().mapitas;
            foreach (MapSlot slot in map)
            {
                if (slot.Scene == SceneController.instance.currentScene)
                {
                    slot.isObtained = true;
                    slot.UpdateUI();
                }
            }
            DestroyEntity();
        }
        else
        {
            Debug.Log("he didn't like that...");
        }
    }
    public void updateVisual(){
        
    }
}