using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatType : Enemy
{
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
            Debug.Log("he liked that");
            worldState.state = true;
            DestroyEntity();
        }
        else
        {
            Debug.Log("he didn't like that...");
        }
    }
}