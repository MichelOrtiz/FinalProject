using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/RemoveWorldState")]
public class InterNotGWorldState : Interaction
{
    [SerializeField] WorldState removeWorldState;
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
        if(partida.WorldStates.Exists(x => x.id == removeWorldState.id)){
            partida.WorldStates.Find(x => x.id == removeWorldState.id).state = false;
        }
        onEndInteraction?.Invoke();
    }
}
