using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSlotManager : MonoBehaviour
{
    [SerializeField]private Transform itemsParent;
    private GameSlot[] gameSlots; 
    void Start()
    {
        gameSlots = itemsParent.GetComponentsInChildren<GameSlot>();
        for(int i=0;i<gameSlots.Length;i++){
            gameSlots[i].SetSlot(i);
        }
    }
}
