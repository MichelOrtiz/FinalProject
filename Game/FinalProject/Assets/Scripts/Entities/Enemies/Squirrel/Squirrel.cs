using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Enemy
{
    new void Start()
    {
        base.Start();
    }
    new void Update()
    {
        base.Update();
        
        if (!isParalized)
        {
            if (!player.isCaptured)
            {
                if (CanSeePlayer(agroRange))
                {
                    //Debug.Log("Can see player indeed");
                    isChasing = true;
                    ChasePlayer();
                }
                else
                {
                    isChasing = false;
                    StartCoroutine(MainRoutine());
                    //Debug.Log("Can't see player indeed");
                }
            }
        }
    }


    void FixedUpdate()
    {
    }

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
    }

}