using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Equipment", menuName = "Equipment/NegateState")]
public class NegateStateEquipment : Equipment
{
    [SerializeField]protected State negateThisState;
    public override void Rutina()
    {
        player.statesManager.RemoveState(negateThisState);
    }
    public override void StartEquip()
    {
        base.StartEquip();
        player.statesManager.bannedStates.Add(negateThisState);
    }
    public override void EndEquip()
    {
        base.EndEquip();
        player.statesManager.bannedStates.Remove(negateThisState);
    }
}
