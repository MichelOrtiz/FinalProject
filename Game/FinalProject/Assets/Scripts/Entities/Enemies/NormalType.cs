using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalType : Enemy
{
    // Waiting patrolling time
    float timer = 1f;

    #region Unity stuff
    protected new void Start()
    {
        normalSpeed = averageSpeed/2;
        chaseSpeed = normalSpeed;
        base.Start();
    }

    protected new void Update()
    {
        isChasing = PlayerSighted();
        base.Update();
    }

    new protected void FixedUpdate()
    {
        
        base.FixedUpdate();
    }
    #endregion

    #region Behaviour methods
    protected override void MainRoutine()
    {
        if (InFrontOfObstacle() || IsNearEdge())
        {
            if (timer > 0)
            {
                isWalking = false;
                timer -= Time.deltaTime;
                return;
            }
            ChangeFacingDirection();
            timer = 1f;
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * normalSpeed);
            isWalking = true;
        }
    }

    protected override void ChasePlayer()
    {
        if (!IsNearEdge() && !touchingPlayer)
        {
            rigidbody2d.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y), chaseSpeed * Time.deltaTime);
        }
        else
        {
            isWalking = false;
        }
    }

    protected override void Attack()
    {
        player.Captured(nTaps: 9, damagePerSecond: 0);
    }
    #endregion
}