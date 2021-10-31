using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/GiveItem")]
public class InterGG : Interaction
{
    [SerializeField] Item giveItem;
    [SerializeField] int cantidad = 1;
    public override void DoInteraction()
    {
        if(condition!=null){
            if(condition.isDone){
                GenerateAndGive();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            GenerateAndGive();
        }
    }
    void GenerateAndGive(){
        for(int i=0;i < cantidad; i++){
            Inventory.instance.Add(giveItem);
        }
        onEndInteraction?.Invoke();
    }
}
