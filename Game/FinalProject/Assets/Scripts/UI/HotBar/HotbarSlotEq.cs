using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class HotbarSlotEq : HotbarSlot
{
    public override void OnButtonPress()
    {
        if(inventoryUI.moveItem == null) return;
        Type equipmentType = typeof(Equipment);
        Type itemType = inventoryUI.moveItem.GetItem().GetType();
        bool isEquipment = equipmentType.IsAssignableFrom(itemType);
        if(!isEquipment){
            Debug.Log("Not equipment: " + equipmentType.ToString() + " : " + itemType.ToString());
            inventoryUI.moveItem = null;
            inventoryUI.UpdateUI();
            return;
        }
        SetItem(inventoryUI.moveItem.GetItem());
        inventoryUI.moveItem = null;
        inventoryUI.UpdateUI();
    }
}
