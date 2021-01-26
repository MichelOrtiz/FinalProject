using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Enemy
{

    #region Unity stuff
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        if (!isParalized && !player.isCaptured)
        {
            if (CanSeePlayer(agroRange))
            {
                isChasing = true;
                ChasePlayer();
            }
            else
            {
                isChasing = false;
                StartCoroutine(MainRoutine());
            }
        }
    }
    #endregion

    #region Behaviour methods
    protected override IEnumerator MainRoutine()
    {
        if (InFrontOfObstacle() || IsNearEdge())
        {
            isWalking = false;
            yAngle = movingRight? 180: 0;
            yield return new WaitForSeconds(2);
            ChangeFacingDirection();
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * normalSpeed);
            isWalking = true;
        }
    }

    protected override void ChasePlayer()
    {
        if (!IsNearEdge())
        {
            transform.Translate(Vector3.left * Time.deltaTime * chaseSpeed);
        }
        else
        {
            isWalking = false;
        }
    }

    protected override void Attack()
    {
        player.Captured(nTaps: 6, damagePerSecond: 0);
        player.transform.position = this.transform.position;
    }
    #endregion
}