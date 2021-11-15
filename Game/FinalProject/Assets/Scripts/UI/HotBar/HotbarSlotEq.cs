using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class HotbarSlotEq : HotbarSlot
{
    public override void OnButtonPress()
    {
        if(inventoryUI.moveItemIndex == -1) return;
        Type equipmentType = typeof(Equipment);
        Item newAssign = Inventory.instance.items[inventoryUI.moveItemIndex];
        Type itemType = newAssign.GetType();
        bool isEquipment = equipmentType.IsAssignableFrom(itemType);
        if(!isEquipment){
            Debug.Log("Not equipment: " + equipmentType.ToString() + " : " + itemType.ToString());
            inventoryUI.moveItemIndex = -1;
            inventoryUI.UpdateUI();
            return;
        }
        SetItem(newAssign);
        inventoryUI.moveItemIndex = -1;
        inventoryUI.UpdateUI();
    }
}
