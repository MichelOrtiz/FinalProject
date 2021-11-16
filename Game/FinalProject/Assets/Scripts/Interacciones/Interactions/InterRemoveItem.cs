using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/RemoveItem")]
public class InterRemoveItem : Interaction
{
    [SerializeField] Item removeItem;
    [SerializeField] int cantidad = 1;
    public override void DoInteraction()
    {
        if(condition!=null){
            if(condition.isDone){
                RemoveItem();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            RemoveItem();
        }
    }
    void RemoveItem(){
        for(int i=0;i < cantidad; i++){
            Inventory.instance.Remove(removeItem);
        }
        onEndInteraction?.Invoke();
    }
}
