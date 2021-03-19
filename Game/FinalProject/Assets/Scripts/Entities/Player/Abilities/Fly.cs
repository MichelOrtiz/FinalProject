using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Ability
{
    public override void UseAbility()
    {
        base.UseAbility();
        player.isFlying = !player.isFlying;
    }
}
