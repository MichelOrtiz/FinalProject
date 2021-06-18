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
            cofreUI.MoveItems(this.index);
        }
        if(cofreUI.GetFocusSlot()!=this){
            cofreUI.FocusSlot(this);
        }else{
            cofreUI.ShowMenuDesp();
        }
        
    }
    public override void UseItem(){
        item.Use();
        cofreUI.RemoveFocusSlot();
    }
    public override void RemoveItem(){
        Cofre.instance.RemoveItem(this.item);
        cofreUI.RemoveFocusSlot();
    }
}
