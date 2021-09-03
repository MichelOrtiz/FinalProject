using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreSlot : ItemSlot
{
    public enum Holder {Cofre, Inventario}
    private CofreUI cofreUI;
    public Holder origen;
    private void Start() {
        cofreUI = CofreUI.instance;
        index = 0;
    }
    public override void OnButtonPress(){
        if(item!=null){
            Debug.Log("Press");
            if(origen == Holder.Inventario){
                Debug.Log("InvSlot.CofreUI");
                Cofre.instance.AddItem(item);
                Inventory.instance.Remove(item);
            }
            if(origen == Holder.Cofre){
                Debug.Log("CofSlot.CofreUI");
                Inventory.instance.Add(item);
                Cofre.instance.RemoveItem(item);
            }
        }
    }
}
