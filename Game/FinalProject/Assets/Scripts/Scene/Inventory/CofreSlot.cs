using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreSlot : Slot
{
    private CofreUI cofreUI;
    private void Start() {
        cofreUI = CofreUI.instance;
        index = 0;
    }
    public override void OnButtonPress(){
        if(cofreUI.GetMoveItem()!=null){
            cofreUI.MoveItems(this);
        }
        if(cofreUI.GetFocusSlot()!=this){
            cofreUI.FocusSlot(this);
        }else{
            if(item==null)return;
            cofreUI.ShowMenuDesp();
        }
        
    }
    public override void UseItem(){
        if(item==null)return;
        item.Use();
        cofreUI.RemoveFocusSlot();
    }
    public override void RemoveItem(){
        if(item==null)return;
        Cofre.instance.RemoveItem(this.item);
        cofreUI.RemoveFocusSlot();
    }
}
