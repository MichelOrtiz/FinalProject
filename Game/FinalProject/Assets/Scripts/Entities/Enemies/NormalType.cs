using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalType : Enemy
{
    // Waiting patrolling time
    

    #region Unity stuff

    protected new void Update()
    {
        isChasing = fieldOfView.canSeePlayer;
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
            //enemyMovement.GoTo(GetPosition() + Vector3.up, chasing: false, gravity: false);
        }
    }

    protected override void ChasePlayer()
    {
        if (!touchingPlayer)
        {
            enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: true);
        }
    }

    /*protected override void Attack()
    {
        if(atackEffect != null){
            player.statesManager.AddState(atackEffect,this);
        }
        player.TakeTirement(damageAmount);
    }*/
    #endregion
}