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

        startWaitTime = 2f;
        waitTime = startWaitTime;
        /*viewDistance = 3f;
        damageAmount = 20;
        waitTime = 2f;
        chaseSpeed = normalSpeed;*/
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
            //rigidbody2d.velocity = new Vector2();
            enemyMovement.StopMovement();
        }
        base.FixedUpdate();
    }
    #endregion

    #region Behaviour methods
    protected override void MainRoutine()
    {
        enemyMovement.DefaultPatrol();
        /*if (fieldOfView.inFrontOfObstacle || groundChecker.isNearEdge)
        {
            rigidbody2d.velocity = new Vector2();
            if (waitTime > 0)
            {
                isWalking = false;
                waitTime -= Time.deltaTime;
                return;
            }
            ChangeFacingDirection();
            waitTime = startWaitTime;
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * normalSpeed);
            isWalking = true;
        }*/
    }


    protected override void ChasePlayer()
    {
        enemyMovement.FollowPlayerInGround();
        /*Vector2 direction = (Vector2) transform.position + (player.GetPosition().x > transform.position.x ? Vector2.right : Vector2.left);
        if (!IsNearEdge() && !touchingPlayer && isGrounded)
        {
            //rigidbody2d.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y), chaseSpeed * Time.deltaTime);
            rigidbody2d.position = Vector2.MoveTowards(transform.position, direction, chaseSpeed * Time.deltaTime);
        }
        else
        {
            isWalking = false;
        }*/
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