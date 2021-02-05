using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSquirrel : Squirrel
{

    Vector3 jump;
    float lastY; // last y pos of the enemy (before jumping)
    float playerY; // when player sighted
    float initialY;

    #region Unity stuff
    new void Start()
    {
        base.Start();
        jump = new Vector3(0f, jumpForce, 0f);   
        chaseSpeed = averageSpeed*2;
        damageAmount = 10f;
    }

    new void Update()
    {
        isChasing = PlayerSighted();
        if (isChasing)
        {
            if ((player.GetPosition().x < this.GetPosition().x && facingDirection == RIGHT)
            || (player.GetPosition().x > this.GetPosition().x && facingDirection == LEFT)
            || InFrontOfObstacle()) {
                ChangeFacingDirection();
            }
        }
        UpdateState();
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
                Fear();
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
            isJumping = true;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
            isJumping = false;

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
                rigidbody2d.position = Vector3.MoveTowards(GetPosition(), playerPosition, chaseSpeed * Time.deltaTime);
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
        player.Captured(nTaps: 6, damagePerSecond: damageAmount);
    }

    public void UpdateDirection()
    {
        throw new System.NotImplementedException();
    }

    public override bool CanSeePlayerLinearFov(float distance)
    {
        Vector3 endPos = Vector3.MoveTowards(fovOrigin.position, player.GetPosition(), distance);// * ;;
        RaycastHit2D hit = Physics2D.Linecast(fovOrigin.position, endPos, 1 << LayerMask.NameToLayer("Action"));
    
        Debug.DrawLine(fovOrigin.position, endPos, (Vector3.Angle(fovOrigin.position, endPos) <= 180? Color.red : Color.green));
    
        if (hit.collider == null)
        {
            return false;
        }
        Debug.Log($"Can see player:{hit.collider.gameObject.CompareTag("Player")}");
        return hit.collider.gameObject.CompareTag("Player") && Vector3.Angle(fovOrigin.position, endPos) <= 180;
    }
    #endregion

    
}