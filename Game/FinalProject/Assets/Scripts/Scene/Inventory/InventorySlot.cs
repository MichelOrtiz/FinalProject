using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : ItemSlot,IPointerEnterHandler,IPointerExitHandler
{
    private InventoryUI inventoryUI;
    private void Start() {
        inventoryUI = InventoryUI.instance;
        icon.color = Color.clear;
    }
    public override void OnButtonPress()
    {
        //Desplegar menu con opciones

    }
    public override void MoveItem(){
        ItemSlot mItem = InventoryUI.instance.moveItem;
        if(mItem == item){
            mItem = null;
        }else if(mItem == null){
            mItem = this;
        }else if(mItem != null){
            //cambiar de lugar objetos dentro del inv?
            Inventory.instance.SwapItems(mItem.GetIndex(),index);
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
}
