using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Item", menuName = "Equipment/GiveStatus")]
public class GiveStatusEq : Equipment
{
    [SerializeField] State giveState;
    public override void Rutina(){
        base.Rutina();
        if(!PlayerManager.instance.statesManager.currentStates.Contains(giveState)){
            PlayerManager.instance.statesManager.AddState(giveState);
        }
    }
}
