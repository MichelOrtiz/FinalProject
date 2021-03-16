using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiveStatus : Item
{
    public State statusForPlayer;

    public override void Use()
    {
        base.Use();
        statusForPlayer.ActivateEffect(PlayerManager.instance);
    }
}
