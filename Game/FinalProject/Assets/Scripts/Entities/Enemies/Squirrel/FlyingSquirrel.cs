using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSquirrel : Squirrel
{

    Vector3 jump;
    float lastY; // last y pos of the enemy (before jumping)
    float playerY; // when player sighted


    #region Unity stuff
    new void Start()
    {
        base.Start();
        jump = new Vector3(0f, jumpForce, 0f);
        facingDirection = transform.eulerAngles.y == 0? LEFT:RIGHT;

    }

    new void Update()
    {
        base.Update();
        isChasing = false;
    
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);
        isFalling = rigidbody2d.velocity.y < - fallingCriteria;
        UpdateAnimation();

        if (meshFov != null)
        {
            meshFov.SetOrigin(transform.position);
            meshFov.SetAimDirection(transform.eulerAngles);
        }
        

        isResting = !isChasing;
        
    }

    new void FixedUpdate()
    {
        if (!isParalized && !player.isCaptured)
        {
            if (PlayerSighted())
            {
                rigidbody2d.WakeUp();
                isChasing = true;
                playerY = player.transform.position.y;
                ChasePlayer();
            }
            else
            {
                rigidbody2d.Sleep();
            }
        }
    }
    #endregion

    #region Behaviour methods
    protected override void ChasePlayer()
    {
        if (isGrounded)
        {
            isJumping = true;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
        }
        if (isFalling)
        {
            isJumping = false;
            Vector3 playerPosition = new Vector3(player.transform.position.x - transform.position.x, 0f);
            rigidbody2d.AddForce(playerPosition * chaseSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    protected override void MainRoutine()
    {
        return;
    }

    protected override void Attack()
    {
        player.Captured(nTaps: 6, damagePerSecond: 10);
        player.transform.position = this.transform.position;
    }

    public void UpdateDirection()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    
}