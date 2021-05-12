using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : Slot
{
    private InventoryUI inventoryUI;
    private void Start() {
        inventoryUI = InventoryUI.instance;
        index = 0;
    }
    public override void OnButtonPress(){
        if(inventoryUI.GetMoveItem()!=null){
            inventoryUI.MoveItems(this.index);
        }
        if(inventoryUI.GetFocusSlot()!=this){
            inventoryUI.FocusSlot(this);
        }else{
            inventoryUI.ShowMenuDesp();
        }
        
    }
    public override void UseItem(){
        item.Use();
        inventoryUI.RemoveFocusSlot();
    }
    public override void RemoveItem(){
        item.RemoveFromInventory();
        inventoryUI.RemoveFocusSlot();
    }
}
