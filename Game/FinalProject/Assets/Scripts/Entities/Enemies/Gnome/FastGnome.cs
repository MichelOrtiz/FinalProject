using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastGnome : Gnome
{
    protected new void Start()
    {
        base.Start();
    }

    new void MainRoutine()
    {
        enemyMovement.DefaultPatrol();
    }
}
