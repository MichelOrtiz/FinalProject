using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Equipment", menuName = "Equipment/NegateState")]
public class NegateStateEquipment : Equipment
{
    [SerializeField]protected State negateThisState;
    public override void Rutina()
    {
        if(negateThisState != null){
            player.statesManager.RemoveState(negateThisState);
        }
    }
    public override void StartEquip()
    {
        base.StartEquip();
        if(negateThisState != null){
            player.statesManager.bannedStates.Add(negateThisState);
        }
    }
    public override void EndEquip()
    {
        base.EndEquip();
        if(negateThisState != null){
            player.statesManager.bannedStates.Remove(negateThisState);
        }
    }
}
