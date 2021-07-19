using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungi : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float selfKnockBackDuration;
    [SerializeField] private float selfKnockBackForce;
    [SerializeField] private Vector2 selfKnockBackDir;
    private bool inKnockback;
    [SerializeField] private float baseTimeUntilDestroyed;
    [SerializeField] private float baseTimeUntilReset;
    private float timeUntilDestroyed;
    private float timeUntilReset;
    private bool facingRight;

    new void Update()
    {
        isJumping = isGrounded || !isFalling;
        facingRight = facingDirection == RIGHT;
        base.Update();
    }

    new void FixedUpdate()
    {
        if (isChasing && !touchingPlayer)
        {
            ChasePlayer();
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
    }

    protected override void ChasePlayer()
    {
        if (!fieldOfView.inFrontOfObstacle)
        {
            enemyMovement.Translate(rigidbody2d.transform.right, chasing: true);
        }
        if (groundChecker.isNearEdge || fieldOfView.inFrontOfObstacle)
        {
            enemyMovement.Jump();
        }
    }

    protected override void Attack()
    {
        if (eCollisionHandler.touchingGround)
        {
            transform.position = player.GetPosition();
        }
        else
        {
            transform.position = new Vector2 ( player.GetPosition().x + (player.facingDirection == RIGHT ? 0.5f : - 0.5f) , player.GetPosition().y );
            Knockback(selfKnockBackDuration, selfKnockBackForce, selfKnockBackDir);
        }
    }

    protected override void eCollisionHandler_TouchingPlayer()
    {
        if (timeUntilDestroyed > baseTimeUntilDestroyed)
        {
            Destroy(gameObject);
        }
        else
        {
            // play animation
            timeUntilDestroyed += Time.deltaTime;
        }
    }

    protected override void eCollisionHandler_StoppedTouchingPlayer()
    {
        if (eCollisionHandler.touchingGround)
        {
            transform.position = player.GetPosition();
        }
    }

    void OnDestroy()
    {
        try
        {
            FindObjectOfType<FungusBoss>().defeatedFungus++;
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("FungusBoss null");
        }
    }
}