using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSlot : Slot
{
    private HotbarUI hotbarUI;
    private InventoryUI inventoryUI;

    private void Start() {
        inventoryUI = InventoryUI.instance;
        hotbarUI = HotbarUI.instance;
    }
    public override void OnButtonPress()
    {
        if(inventoryUI.GetMoveItem()!=null){
            SetItem(inventoryUI.GetMoveItem());
            inventoryUI.RemoveMoveItem();
            inventoryUI.UpdateUI();
        }
    }
}
