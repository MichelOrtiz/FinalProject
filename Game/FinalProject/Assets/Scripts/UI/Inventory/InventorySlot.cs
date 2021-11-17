using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Reflection;
using System;

public class InventorySlot : ItemSlot,IPointerEnterHandler,IPointerExitHandler
{
    private InventoryUI inventoryUI;
    private void Start() {
        inventoryUI = InventoryUI.instance;
    }
    public override void OnButtonPress()
    {
        if(item == null) return;
        if(item.type == Item.ItemType.Basura) return;
        if(Input.GetKey(PlayerManager.instance.inputs.controlBinds["MENUFASTASSIGN"])){
            Type equipmentType = typeof(Equipment);
            Type itemType = item.GetType();
            bool isEquipment = equipmentType.IsAssignableFrom(itemType);
            if(isEquipment){
                for(int i=0; i < EquipmentManager.instance.slotsEquipment.Length; i++){
                    if(EquipmentManager.instance.slotsEquipment[i].GetItem() == null){
                        EquipmentManager.instance.slotsEquipment[i].SetItem(item);
                        //UpdateUI
                        return;
                    }
                }
                return;
            }
            for(int i=0;i<HotbarUI.instance.slotsHotbar0.Length;i++){
                if(HotbarUI.instance.slotsHotbar0[i].GetItem() == null){
                    HotbarUI.instance.slotsHotbar0[i].SetItem(item);
                    HotbarUI.instance.UpdateHotbar0UI();
                    return;
                }
            }
            return;
        }
        if(InventoryUI.instance.moveItemIndex != -1){
            MoveItem();
            return;
        }
        //Desplegar menu con opciones
        inventoryUI.OpenDesMenu(this);
    }
    public override void MoveItem(){
        if(InventoryUI.instance.moveItemIndex == index){
            InventoryUI.instance.moveItemIndex = -1;
            //Debug.Log("Same move item");
        }else if(InventoryUI.instance.moveItemIndex == -1){
            Debug.Log("New move item");
            InventoryUI.instance.moveItemIndex = index;
        }else if(InventoryUI.instance.moveItemIndex != -1){
            //Debug.Log("Cambiando de lugar");
            //cambiar de lugar objetos dentro del inv?
            Inventory.instance.SwapItems(InventoryUI.instance.moveItemIndex,index);
            InventoryUI.instance.moveItemIndex = -1;
        }
    }
    public void OnPointerEnter(PointerEventData eventData){
        if(item == null) return;
        inventoryUI.focusedSlot = this;
        inventoryUI.UpdateText();
    }
    public void OnPointerExit(PointerEventData eventData){
        if(item == null) return;
        inventoryUI.focusedSlot = null;
        inventoryUI.UpdateText();
    }
    public InventorySlot(InventorySlot copy){
        item = copy.item;
        index = copy.index;
    }
}
