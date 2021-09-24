using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName = "Equipment/HeavyBoots")]
public class BotasPesadas : Equipment
{
    [SerializeField]State negateThisState;
    public override void Rutina()
    {
        player.statesManager.RemoveState(negateThisState);
    }
    public override void StartEquip()
    {
        base.StartEquip();
        player.currentGravity = PlayerManager.defaultGravity * 5f;
    }
    public override void EndEquip()
    {
        base.EndEquip();
        player.currentGravity = PlayerManager.defaultGravity;
    }
}
