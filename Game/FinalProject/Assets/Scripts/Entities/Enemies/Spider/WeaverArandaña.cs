using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaverArandaña : Arandaña
{
    #region Unity stuff
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
    }

    new void FixedUpdate()
    {
    }
    #endregion

    #region Behaviour methods
    protected override void ChasePlayer()
    {
    }

    protected override void MainRoutine()
    {
        /*if (isGrounded)
        {
            rigidbody2d.Sleep();
        }
        else
        {
            rigidbody2d.WakeUp();
        }*/
    }

    protected override void Attack()
    {
    }
    #endregion
}