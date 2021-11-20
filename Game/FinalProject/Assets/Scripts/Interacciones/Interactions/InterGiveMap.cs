
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/GiveMap")]
public class InterGiveMap : Interaction
{
    [SerializeField] WorldState worldState;
    [SerializeField] int mapZone;
    public override void DoInteraction()
    {
        if(condition != null){
            if(condition.isDone){
                GiveMap();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            GiveMap();
        }
    }

    void GiveMap(){
        SaveFile partida = SaveFilesManager.instance.currentSaveSlot;
        if(partida.WorldStates.Exists(x => x.id == worldState.id)){
            partida.WorldStates.Find(x => x.id == worldState.id).state = true;
        }else{
            partida.WorldStates.Add(worldState);
        }
        List<MapSlot> map = FindObjectOfType<MapUI>().mapitas;
        foreach (MapSlot slot in map)
        {
            if (slot.Scene == mapZone)
            {
                slot.isObtained = true;
                slot.UpdateUI();
            }
        }
        onEndInteraction?.Invoke();
    }
}
