using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
public class HotbarSlot : ItemSlot
{
    private HotbarUI hotbarUI;
    protected InventoryUI inventoryUI;

    private void Start() {
        inventoryUI = InventoryUI.instance;
        hotbarUI = HotbarUI.instance;
    }
    public override void OnButtonPress()
    {
        if(inventoryUI.moveItem == null) return;
        Type equipmentType = typeof(Equipment);
        Type itemType = inventoryUI.moveItem.GetItem().GetType();
        bool isEquipment = equipmentType.IsAssignableFrom(itemType);
        if(isEquipment){
            Debug.Log("Equipment: " + equipmentType.ToString() + " : " + itemType.ToString());
            inventoryUI.moveItem = null;
            inventoryUI.UpdateUI();
            return;
        }

        SetItem(inventoryUI.moveItem.GetItem());
        inventoryUI.moveItem = null;
        inventoryUI.UpdateUI();
    }
}
