using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSquirrel : Enemy
{
    Vector3 jump;
    float lastY; // last y pos of the enemy (before jumping)
    float playerY; // when player sighted

    new void Start()
    {
        base.Start();

        jump = new Vector3(0f, jumpForce, 0f);
        facingDirection = transform.eulerAngles.y == 0? LEFT:RIGHT;
    }
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

    protected override IEnumerator MainRoutine()
    {
        return null;
    }

    new void Update()
    {
        base.Update();
        isChasing = false;
    
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);
        isFalling = rigidbody2d.velocity.y < - fallingCriteria;
        UpdateAnimation();

        isResting = !isChasing;
        
    }

    void FixedUpdate()
    {
        if (CanSeePlayer(agroRange))
        {
            isChasing = true;
            playerY = player.transform.position.y;
            ChasePlayer();
        }
    }
}
