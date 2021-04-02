using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisItem : MonoBehaviour
{
    PlayerManager player;
    Inventory inventory;
    [SerializeField] private Item item;
    void Start()
    {
        player = PlayerManager.instance;
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.items.Contains(item))
        {
            
        }
    }
}
