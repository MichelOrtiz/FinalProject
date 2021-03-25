using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New ItemStatus", menuName = "Inventory/ItemStatus")]
public class ItemGiveStatus : Item
{
    public State statusForPlayer;

    public override void Use()
    {
        //base.Use();
        PlayerManager.instance.statesManager.AddState(statusForPlayer);
    }
}
