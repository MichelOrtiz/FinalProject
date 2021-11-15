using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatType : Enemy
{
    [SerializeField] private int amount = 1;
    [SerializeField] private Item wishedItem;
    [SerializeField] private Bubble wishedItemBubble;
    [SerializeField] private WorldState worldState = new WorldState();
    [SerializeField] private float bubbleRadius = 5f;

    protected new void Start()
    {
        base.Start();
        //iconSprite = GetComponent<SpriteRenderer>();
        //wishedItemSprite = wishedItem.icon;
        wishedItemBubble.SetImage(wishedItem.icon,amount);
        if(SaveFilesManager.instance != null && SaveFilesManager.instance.currentSaveSlot != null)
        {
            SaveFile partida = SaveFilesManager.instance.currentSaveSlot;
            foreach(WorldState w in partida.WorldStates)
            {
                if(w.id == worldState.id)
                {
                    worldState = w;
                    if(w.state)
                    {
                        Destroy(gameObject);
                        return;
                    }
                    else
                    {
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

    protected new void Update()
    {
        base.Update();
        float distance = Vector2.Distance(player.GetPosition(), transform.position);
        if(distance <= bubbleRadius){
            wishedItemBubble.gameObject.SetActive(true);
            wishedItemBubble.InFrontOfPlayer();
        }else{
            wishedItemBubble.gameObject.SetActive(false);
        }
    }

    public void updateVisual()
    {
        wishedItemBubble.UpdateAmount(amount);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,bubbleRadius);
    }
}