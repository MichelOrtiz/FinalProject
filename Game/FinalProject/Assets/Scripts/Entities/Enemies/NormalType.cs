using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalType : Enemy
{
    // Waiting patrolling time
    

    #region Unity stuff
    protected new void Start()
    {
        base.Start();
    }

    protected new void Update()
    {
        isChasing = CanSeePlayer();
        base.Update();
    }

    new protected void FixedUpdate()
    {
        if ( (fieldOfView.inFrontOfObstacle || groundChecker.isNearEdge) && !isFalling)
        {
            enemyMovement.StopMovement();
        }
        base.FixedUpdate();
    }
    #endregion

    #region Behaviour methods
    protected override void MainRoutine()
    {
        if (!touchingPlayer)
        {
            enemyMovement.DefaultPatrol();
        }
    }

    protected override void ChasePlayer()
    {
        if (!touchingPlayer)
        {
            enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: true);
        }
    }

    protected override void Attack()
    {
        if(atackEffect != null){
            player.statesManager.AddState(atackEffect,this);
        }
        player.TakeTirement(damageAmount);
    }

    public override void ConsumeItem(Item item)
    {
        Debug.Log("Consumiendo "+ item.nombre);
    }
    #endregion
}