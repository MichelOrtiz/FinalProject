using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    EquipmentManager equipmentManager;
    InventoryUI inventoryUI;
    private void Start() {
        inventoryUI = InventoryUI.instance;
        equipmentManager = EquipmentManager.instance;
    }
    public override void OnButtonPress(){
        if(inventoryUI.moveItemIndex != -1){
            Item newAssign = Inventory.instance.items[inventoryUI.moveItemIndex];
            SetItem(newAssign);
            inventoryUI.moveItemIndex = -1;
            inventoryUI.UpdateUI();
        }
    }
    public override void UseItem(){
        item.Use();
    }
}
