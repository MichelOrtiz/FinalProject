using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class HotbarSlotEq : HotbarSlot
{
    [SerializeField] GameObject equipedSign;
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
        if(!isEquipment){
            Debug.Log("Not equipment: " + equipmentType.ToString() + " : " + itemType.ToString());
            inventoryUI.moveItemIndex = -1;
            inventoryUI.UpdateUI();
            return;
        }
        SetItem(newAssign);
        Equipment equipment = (Equipment) newAssign;
        equipment.reference = this;
        inventoryUI.moveItemIndex = -1;
        inventoryUI.UpdateUI();
    }
    private void Update() {
        if(item == null) return;
        Equipment equipment = (Equipment) item;
        if(EquipmentManager.instance.currentEquipment[(int)equipment.equipmentSlot] == equipment){
            equipedSign.SetActive(true);
        }else{
            equipedSign.SetActive(false);
        }
    }
}
