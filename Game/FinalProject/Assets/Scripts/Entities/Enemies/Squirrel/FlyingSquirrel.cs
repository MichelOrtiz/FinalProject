using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSquirrel : Enemy
{

    #region Unity stuff
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        isJumping = !isFalling && !isGrounded;
        base.Update();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion

    #region Behaviour methods
    protected override void ChasePlayer()
    {
        Vector3 playerPosition;
        if (isGrounded)
        {
            // Jumps
            //rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce * rigidbody2d.gravityScale);
            enemyMovement.Jump();
        }
        if (isFalling)
        {
            float playerY = player.GetPosition().y;
            
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
                //rigidbody2d.position = Vector3.MoveTowards(GetPosition(), playerPosition, chaseSpeed * Time.deltaTime * rigidbody2d.gravityScale);
                enemyMovement.GoTo(playerPosition, chasing: true, gravity: true);
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
        if(atackEffect != null){
            player.statesManager.AddState(atackEffect,this);
        }
    }
    #endregion
}