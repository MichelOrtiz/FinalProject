using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Ability
{
    public override void UseAbility()
    {
        base.UseAbility();
        if(player.currentStamina < staminaCost + 0.1f)return;
        player.isFlying = !player.isFlying;
    }
}
