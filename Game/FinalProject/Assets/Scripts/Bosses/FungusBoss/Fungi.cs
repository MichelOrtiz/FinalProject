using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungi : NormalType
{
    [SerializeField] private float baseTimeUntilDestroyed;
    [SerializeField] private float baseTimeUntilReset;
    private float timeUntilDestroyed;
    private float timeUntilReset;
    private bool facingRight;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        isJumping = isGrounded || !isFalling;
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
        if (CanSeePlayer() && !touchingPlayer)
        {
            facingRight = facingDirection == RIGHT;

            if ( (facingRight && GetPosition().x < player.GetPosition().x) || (!facingRight && GetPosition().x > player.GetPosition().x))
            {
                ChangeFacingDirection();
            }
        }
        else if (touchingPlayer)
        {
            Attack();
        }
        else
        {
            if (timeUntilReset > baseTimeUntilReset)
            {
                timeUntilDestroyed = 0;
                timeUntilReset = 0;
            }
            else
            {
                timeUntilReset += Time.deltaTime;
            }
        }
        base.Update();
    }

    new void FixedUpdate()
    {
        if (CanSeePlayer() && !touchingPlayer)
        {
            ChasePlayer();
        }
        if ((IsNearEdge() || InFrontOfObstacle()) && isGrounded)
        {
            Jump();
        }
        
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

    protected override void Attack()
    {
        if (player.facingDirection == RIGHT)
        {
            rigidbody2d.position = new Vector2 (player.GetPosition().x + 1f, player.GetPosition().y);
        }
        else
        {
            rigidbody2d.position = new Vector2 (player.GetPosition().x - 1f, player.GetPosition().y);
        }

        if (timeUntilDestroyed > baseTimeUntilDestroyed)
        {
            FungusBoss.defeatedFungus ++;
            Destroy(gameObject);
        }
        else
        {
            // play animation
            timeUntilDestroyed += Time.deltaTime;
        }
    }

    private void Jump()
    {
        rigidbody2d.AddForce(new Vector2((facingDirection == RIGHT? 1250f : -1250f), jumpForce * 300), ForceMode2D.Impulse);
    }
}