using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSlot : ItemSlot
{
    private HotbarUI hotbarUI;
    private InventoryUI inventoryUI;

    private void Start() {
        inventoryUI = InventoryUI.instance;
        hotbarUI = HotbarUI.instance;
    }
    public override void OnButtonPress()
    {
        if(inventoryUI.moveItem!=null){
            SetItem(inventoryUI.moveItem.GetItem());
            inventoryUI.moveItem = null;
            inventoryUI.UpdateUI();
        }
    }
}
