using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyFox : DogFox
{
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        isChasing = CanSeePlayer();
        isJumping = !isGrounded && !isFalling;
        
        if (isChasing)
        {
            if ((player.GetPosition().x < this.GetPosition().x && facingDirection == RIGHT)
            || (player.GetPosition().x > this.GetPosition().x && facingDirection == LEFT)) {
                ChangeFacingDirection();
            }
        }
        base.Update();
    }

    new void FixedUpdate()
    {
        switch (currentState)
        {
            case StateNames.Chasing:
                ChasePlayer();
                break;
            case StateNames.Falling:
                //ChasePlayer();
                break;
            case StateNames.Paralized:
                //Paralized();
                break;
            case StateNames.Fear:
                //Fear();
                break;
            case StateNames.Patrolling:
                // Uses the same routine than normal type
                MainRoutine();
                break;
            //default:
                //MainRoutine();
                //break;
        }
    }

    protected override void ChasePlayer()
    {
        //float maxJumpDistance = 5f;
        Vector3 playerPosition = new Vector3(player.GetPosition().x, 0);
        
        if ( ((this.GetPosition().x < playerPosition.x) && (player.facingDirection == RIGHT))
            || ((this.GetPosition().x > playerPosition.x) && (player.facingDirection == LEFT)) &&
            !isJumping && !touchingPlayer)
        {
            isWalking = true;
            if (!isJumping) rigidbody2d.position = Vector3.MoveTowards(GetPosition(), playerPosition, chaseSpeed * Time.deltaTime);
            //Debug.Log($"In Front Of Obstacle: {InFrontOfObstacle()}");
            if (InFrontOfObstacle() && isGrounded)
            {
                //rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x +
                   // facingDirection == RIGHT? 8f:-8f, jumpForce * rigidbody2d.gravityScale);
                //rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x, jumpForce * 200* rigidbody2d.gravityScale), ForceMode2D.Impulse);
            }
        }
        else
        {
            isWalking = false;
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), GetPosition(), chaseSpeed * Time.deltaTime);
        }
    }

    protected override void Attack()
    {
        player.Captured(nTaps: 12, damagePerSecond: damageAmount,this);
    }
}
