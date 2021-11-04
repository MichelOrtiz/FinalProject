using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/GiveWorldState")]
public class InterGWorldState : Interaction
{
    [SerializeField] WorldState giveWorldState;
    public override void DoInteraction(){
        if(condition != null){
            if(condition.isDone){
                GiveWorldState();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            GiveWorldState();
        }
    }
    void GiveWorldState(){
        SaveFile partida = SaveFilesManager.instance.currentSaveSlot;
        if(partida.WorldStates.Exists(x => x.id == giveWorldState.id)){
            partida.WorldStates.Find(x => x.id == giveWorldState.id).state = true;
        }else{
            partida.WorldStates.Add(giveWorldState);
        }
        onEndInteraction?.Invoke();
    }
}
