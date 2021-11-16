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
        if(inventoryUI.moveItemIndex == -1){
            SetItem(null);
            return;
        }
        Type equipmentType = typeof(Equipment);
        Item newAssign = Inventory.instance.items[inventoryUI.moveItemIndex];
        Type itemType = newAssign.GetType();
        bool isEquipment = equipmentType.IsAssignableFrom(itemType);
        if(isEquipment){
            Debug.Log("Equipment: " + equipmentType.ToString() + " : " + itemType.ToString());
            inventoryUI.moveItemIndex = -1;
            inventoryUI.UpdateUI();
            return;
        }

        SetItem(newAssign);
        inventoryUI.moveItemIndex = -1;
        inventoryUI.UpdateUI();
    }
}
