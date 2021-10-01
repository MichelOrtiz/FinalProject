using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : ItemSlot
{
    private InventoryUI inventoryUI;
    private void Start() {
        inventoryUI = InventoryUI.instance;
        index = 0;
    }
    public override void OnButtonPress(){
        if(PlayerManager.instance.inputs.leftShift){
            Debug.Log("YES");
            foreach(HotbarSlot slot in HotbarUI.instance.slotsHotbar0){
                if(slot.GetItem()==null){
                    slot.SetItem(this.item);
                    return;
                }
            }
            return;
        }else{
            Debug.Log("No");
        }
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
