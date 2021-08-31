using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyFox : Enemy
{

    protected override void ChasePlayer()
    {
        //float maxJumpDistance = 5f;
        Vector3 playerPosition = new Vector3(player.GetPosition().x, 0);
        
        if ( (((this.GetPosition().x < playerPosition.x) && (player.facingDirection == RIGHT))
            || ((this.GetPosition().x > playerPosition.x) && (player.facingDirection == LEFT))) &&
             !touchingPlayer)
        {
            enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: true);
            animationManager.ChangeAnimation("walk", enemyMovement.ChaseSpeed * 1 / enemyMovement.DefaultSpeed);

            //isWalking = true;
           // if (!isJumping) rigidbody2d.position = Vector3.MoveTowards(GetPosition(), playerPosition, chaseSpeed * Time.deltaTime);
            //Debug.Log($"In Front Of Obstacle: {InFrontOfObstacle()}");
            /*if (InFrontOfObstacle() && isGrounded)
            {
                //rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x +
                   // facingDirection == RIGHT? 8f:-8f, jumpForce * rigidbody2d.gravityScale);
                //rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x, jumpForce * 200* rigidbody2d.gravityScale), ForceMode2D.Impulse);
            }*/
        }
        else
        {
            //Debug.Log("staaaaahp");
            enemyMovement.StopMovement();
            /*isWalking = false;
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), GetPosition(), chaseSpeed * Time.deltaTime);*/
        }
    }

    protected override void MainRoutine()
    {
        enemyMovement.DefaultPatrol();
    }

    new void Attack()
    {
        base.Attack();
        enemyMovement.StopMovement();
    }
}
