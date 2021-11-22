using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HotbarSlot : ItemSlot
{
    private HotbarUI hotbarUI;
    protected InventoryUI inventoryUI;
    public Slider cooldownBar;
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
    private void Update() {
        if(item == null || cooldownBar == null) return;
        if(item.isInCooldown){
            if(item.currentCooldownTime <= cooldownBar.maxValue && item.currentCooldownTime >= cooldownBar.minValue)
                cooldownBar.value = item.currentCooldownTime;
        }else{
            cooldownBar.value = 0;
        }
    }
}
