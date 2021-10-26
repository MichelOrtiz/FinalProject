using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalType : Enemy
{
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
        }
    }

    protected override void ChasePlayer()
    {
        if (!touchingPlayer)
        {
            enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: true);

            if (!groundChecker.isNearEdge)
            {
                animationManager?.ChangeAnimation("walk", enemyMovement.ChaseSpeed * 1 / enemyMovement.DefaultSpeed);
            }
        }
    }
    #endregion
}