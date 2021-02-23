using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image background;
    public Item item;
    private InventoryUI inventoryUI;

    public int inventoryIndex;

    private void Start() {
        inventoryUI = InventoryUI.instance;
        inventoryIndex = 0;
    }
    public void SetItem (Item newItem){
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        background.color = Color.white;
    }

    public void OnButtonPress(){
        if(inventoryUI.moveItem!=null){
            inventoryUI.MoveItems(this.inventoryIndex);
        }
        if(inventoryUI.GetFocusSlot()!=this){
            inventoryUI.FocusSlot(this);
        }else{
            inventoryUI.ShowMenuDesp();
        }
        
    }
    public void UseItem(){
        item.Use();
        inventoryUI.RemoveFocusSlot();
    }
    public void RemoveItem(){
        Inventory.instance.Remove(item);
        inventoryUI.RemoveFocusSlot();
    }

    public void ClearSlot(){
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        background.color = Color.clear;
    }


}
