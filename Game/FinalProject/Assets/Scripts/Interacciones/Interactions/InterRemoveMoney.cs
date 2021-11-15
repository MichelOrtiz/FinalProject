using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/RemoveMoney")]
public class InterRemoveMoney : Interaction{
    [SerializeField] int removeAmount;
    public override void DoInteraction()
    {
        if(condition != null){
            if(condition.isDone){
                RemoveMoney();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            RemoveMoney();
        }
    }
    void RemoveMoney(){
        Inventory.instance.RemoveMoney(removeAmount);
        onEndInteraction?.Invoke();
    }
}
