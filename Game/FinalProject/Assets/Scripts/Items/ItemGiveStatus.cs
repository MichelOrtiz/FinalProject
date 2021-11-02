using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New ItemStatus", menuName = "Inventory/ItemStatus")]
public class ItemGiveStatus : ItemStamina
{
    public State statusForPlayer;

    public override void Use()
    {
        if(isInCooldown){
            Debug.Log("Objeto en cooldown");
            return;
        }
        base.Use();
        PlayerManager.instance.statesManager.AddState(statusForPlayer);
    }
}
