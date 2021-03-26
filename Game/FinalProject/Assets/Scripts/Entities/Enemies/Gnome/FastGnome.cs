using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastGnome : Gnome
{
    protected new void Start()
    {
        base.Start();
        damageAmount = 10;
        normalSpeed = 1.5f * averageSpeed;
        chaseSpeed = normalSpeed;
    }

    protected override void Attack()
    {
        //player.Captured(10, damageAmount,this);
    }
}
