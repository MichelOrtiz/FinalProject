using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSlot : MonoBehaviour
{
    public Image icon;
    public Image background;
    protected Item item;
    [SerializeField]
    protected int index;
    public void SetItem (Item newItem){
        item = newItem;
        if(newItem!=null){
            icon.sprite = item.icon;
        }
        else{
            icon.sprite = null;
        }
        icon.enabled = true;
        background.color = Color.white;
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
        background.color = Color.clear;
    }
    public virtual void UseItem(){
        item.Use();
    }
    public virtual void RemoveItem(){
        item.RemoveFromInventory();
    }
    public abstract void OnButtonPress();
}
