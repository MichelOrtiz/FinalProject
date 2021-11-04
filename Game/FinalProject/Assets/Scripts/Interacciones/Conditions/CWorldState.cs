using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newCondition", menuName ="Interaction/Conditions/HasWorldState")]
public class CWorldState : InterCondition
{
    [SerializeField] WorldState worldState;
    protected override bool checkIsDone()
    {
        if(SaveFilesManager.instance.currentSaveSlot == null) return false;
        List<WorldState> ws = SaveFilesManager.instance.currentSaveSlot.WorldStates;
        return ws.Exists(x => x.id == worldState.id && x.state);
    }
}
