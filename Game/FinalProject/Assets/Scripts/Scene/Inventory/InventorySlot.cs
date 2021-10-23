using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : ItemSlot,IPointerEnterHandler,IPointerExitHandler
{
    private InventoryUI inventoryUI;
    private void Start() {
        inventoryUI = InventoryUI.instance;
    }
    public override void OnButtonPress()
    {
        if(item == null) return;
        if(Input.GetKey(PlayerManager.instance.inputs.controlBinds["MENUFASTASSIGN"])){
            for(int i=0;i<HotbarUI.instance.slotsHotbar0.Length;i++){
                if(HotbarUI.instance.slotsHotbar0[i].GetItem() == null){
                    HotbarUI.instance.slotsHotbar0[i].SetItem(item);
                    HotbarUI.instance.UpdateHotbar0UI();
                    return;
                }
            }
            return;
        }
        if(InventoryUI.instance.moveItem != null){
            MoveItem();
            return;
        }
        //Desplegar menu con opciones
        inventoryUI.OpenDesMenu(this);
    }
    public override void MoveItem(){
        if(InventoryUI.instance.moveItem == item){
            InventoryUI.instance.moveItem = null;
            Debug.Log("Same move item");
        }else if(InventoryUI.instance.moveItem == null){
            Debug.Log("New move item");
            InventoryUI.instance.moveItem = (ItemSlot)this;
        }else if(InventoryUI.instance.moveItem != null){
            Debug.Log("Cambiando de lugar");
            //cambiar de lugar objetos dentro del inv?
            Inventory.instance.SwapItems(InventoryUI.instance.moveItem.GetIndex(),index);
            InventoryUI.instance.moveItem = null;
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
