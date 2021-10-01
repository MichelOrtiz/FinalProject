using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Item", menuName = "Equipment/BackPack")]
public class BackPack : Item
{
    [SerializeField]
    private int addSpace;
    public override void Use(){
        if(!isConsumable)return;
        Inventory.instance.capacidad += addSpace;
        RemoveFromInventory();
    }
}
