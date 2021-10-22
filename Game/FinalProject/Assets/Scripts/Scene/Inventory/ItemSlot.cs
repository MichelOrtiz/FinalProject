using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSlot : MonoBehaviour
{
    public Image icon;
    protected Item item;
    protected int index;
    public void SetItem (Item newItem){
        item = newItem;
        if(newItem!=null){
            icon.sprite = item.icon;
            icon.color = Color.white;
        }
        else{
            icon.color = Color.clear;
            icon.sprite = null;
        }
        icon.enabled = true;
    }
    public Item GetItem(){
        return item;
    }
    public void SetIndex(int i){
        index = i;
        return;
    }
    public int GetIndex(){
        return index;
    }
    public void ClearSlot(){
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        icon.color = Color.clear;
    }

    public virtual void UseItem(){
        item.Use();
    }
    public virtual void RemoveItem(){
        item.RemoveFromInventory();
    }
    public virtual void MoveItem(){
    }
    public abstract void OnButtonPress();
}
