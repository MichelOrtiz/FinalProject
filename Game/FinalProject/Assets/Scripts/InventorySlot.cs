using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image background;
    public Item item;
    public void SetItem (Item newItem){
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void OnButtonPress(){
        if(InventoryUI.instance.focusedSlot!=this){
            InventoryUI.instance.FocusSlot(this);
        }else{
            InventoryUI.instance.RemoveFocusSlot();
            item.Use();
        }
        
    }
    public void RemoveItem(){
        Inventory.instance.Remove(item);
    }

    public void ClearSlot(){
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }


}
