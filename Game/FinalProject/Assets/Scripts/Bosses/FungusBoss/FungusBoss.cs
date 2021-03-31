using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusBoss : NormalType
{
    private bool facingRight;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        /*if (OnEdge())
        {
            if (timeUntilJump > baseTimeUntilJump)
            {
                canJump = true;
                timeUntilJump = 0;
            }
            else
            {
                canJump = false;
                timeUntilJump += Time.deltaTime;
            }
        }*/
        if (CanSeePlayer())
        {
            facingRight = facingDirection == RIGHT;

            if ( (facingRight && GetPosition().x < player.GetPosition().x) || (!facingRight && GetPosition().x > player.GetPosition().x))
            {
                ChangeFacingDirection();
            }
        }
        base.Update();
    }

    new void FixedUpdate()
    {
        if (IsNearEdge() && isGrounded)
        {
            rigidbody2d.AddForce(new Vector2((facingDirection == RIGHT? 1250f : -1250f), jumpForce * 300), ForceMode2D.Impulse);
        }

        base.FixedUpdate();
    }
    protected override void MainRoutine()
    {
        return;
        /*if (!OnEdge())
        {
            base.MainRoutine();
        }*/
        //base.MainRoutine();
    }

    protected override void ChasePlayer()
    {
        
        float  distanceFromPlayer = GetDistanceFromPlayerFov();
        //Vector3 moveDirection = new Vector2(GetPosition().x - player.GetPosition().x, 0f);
        Vector2 moveDirection = GetPosition() - player.GetPosition();
        //rigidbody2d.position = Vector2.MoveTowards(GetPosition(), moveDirection.normalized * viewDistance, chaseSpeed * Time.deltaTime);

        if (facingRight)
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), moveDirection.normalized, -chaseSpeed * Time.deltaTime);
        }
        else
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), moveDirection.normalized, chaseSpeed * Time.deltaTime);
        }

        
    }

    
}