using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotterGnome : Enemy
{
    // To check if the player was being chased
    [Header("Self Additions")]
    [SerializeField] private float targetOffset;
    [SerializeField] private float waitTimeAfterReachedTarget;
    private float curWaitTime;
    [SerializeField] private float intervalBtwFlipInTarget;
    private float curInterval;
    [SerializeReference]private bool justChasedPlayer;
    private Vector3 lastSeenPlayerPosition;

    protected new void Start()
    {
        base.Start();
        curWaitTime = waitTimeAfterReachedTarget;
    }

    protected new void Update()
    {
        base.Update();
    }

    protected new void FixedUpdate()
    {
        if (justChasedPlayer)
        {
            currentState = StateNames.Other;
            if (Mathf.Abs(this.GetPosition().x - lastSeenPlayerPosition.x) > targetOffset)
            {
                if (!fieldOfView.inFrontOfObstacle)
                {
                    //rigidbody2d.position = Vector3.MoveTowards(this.GetPosition(), new Vector3(lastSeenPlayerPosition.x, 0), chaseSpeed * rigidbody2d.gravityScale * Time.deltaTime);
                    Debug.Log("chasing target direction");
                    enemyMovement.GoToInGround(lastSeenPlayerPosition, chasing: true, checkNearEdge: false);
                }
                else
                {
                    lastSeenPlayerPosition = this.GetPosition();
                }
            }
            else
            {
                enemyMovement.StopMovement();

                if (curWaitTime > 0)
                {
                    if (curInterval > intervalBtwFlipInTarget)
                    {
                        enemyMovement.ChangeFacingDirection();
                        curInterval = 0;
                    }
                    else
                    {
                        curInterval += Time.deltaTime;
                    }
                    curWaitTime -= Time.deltaTime;
                    return;
                }

                enemyMovement.ChangeFacingDirection();
                justChasedPlayer = false;
                curWaitTime = waitTimeAfterReachedTarget;
            }
        }
        base.FixedUpdate();
    }

    protected override void SetStates()
    {
        isChasing = fieldOfView.canSeePlayer || touchingPlayer;// || justChasedPlayer;
    }


    void LateUpdate()
    {
        // if ChasePlayer() was just called in update, checks if can not longer see player to update the boolean
        justChasedPlayer = justChasedPlayer && !fieldOfView.canSeePlayer;
    }
    protected override void ChasePlayer()
    {
        //if (!touchingPlayer)
        {
            //rigidbody2d.position = Vector3.MoveTowards(this.GetPosition(), player.GetPosition(), chaseSpeed * Time.deltaTime * rigidbody2d.gravityScale);
            enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: false);
        }

        if (!justChasedPlayer)
        {
            lastSeenPlayerPosition = player.GetPosition();
        }

        justChasedPlayer = true;
        curWaitTime = waitTimeAfterReachedTarget;
        curInterval = 0;
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }

    protected override void MainRoutine()
    {
        enemyMovement.DefaultPatrol();
    }
}
