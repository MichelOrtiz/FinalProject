using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSquirrel : Squirrel
{
    float playerY; // when player sighted
    float initialY;

    #region Unity stuff
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        isJumping = !isFalling && !isGrounded;
        isChasing = CanSeePlayer();
        if (!isGrounded)
        {
            if ((player.GetPosition().x < this.GetPosition().x && facingDirection == RIGHT) // player is left to the enemy
            || (player.GetPosition().x > this.GetPosition().x && facingDirection == LEFT)) // player is right
            {
                ChangeFacingDirection();
            }
        }
        base.Update();
    }

    new void FixedUpdate()
    {
        switch (state)
        {
            case State.Chasing:
                ChasePlayer();
                break;
            case State.Falling:
                //ChasePlayer();
                break;
            case State.Paralized:
                //Paralized();
                break;
            case State.Fear:
                //Fear();
                break;
            case State.Patrolling:
                MainRoutine();
                break;
            default:
                MainRoutine();
                break;
        } 
    }
    #endregion

    #region Behaviour methods
    protected override void ChasePlayer()
    {
        Vector3 playerPosition;
        if (isGrounded)
        {
            // Jumps
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce * rigidbody2d.gravityScale);
        }
        if (isFalling)
        {
            playerY = player.GetPosition().y;
            
            // player is below
            if (playerY < GetPosition().y)
            {
                playerPosition = player.GetPosition();
            }
            else
            {
                playerPosition = new Vector3(player.GetPosition().x, this.GetPosition().y);
            }

            if (!touchingPlayer)
            {
                rigidbody2d.position = Vector3.MoveTowards(GetPosition(), playerPosition, chaseSpeed * Time.deltaTime * rigidbody2d.gravityScale);
            }
        }
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
        player.Captured(nTaps: 9, damagePerSecond: damageAmount);
    }
    #endregion
}