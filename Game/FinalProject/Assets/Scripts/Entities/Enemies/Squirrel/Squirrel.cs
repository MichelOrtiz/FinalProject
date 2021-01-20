using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Enemy
{
    
    

    new void Start()
    {
        facingDirection = LEFT;
    }

    // Update is called once per frame
    new void Update()
    {
        //groundInfo = Physics2D.Raycast(groundChecker.position, Vector2.down, whatIsGround);
        //inFrontOfObstacle = !collisionInfo.collider;
        //collisionInfo = Physics2D.Raycast(collisionChecker.position, Vector2.zero, collisionCheckRadius);
        //inFrontOfObstacle = collisionInfo.collider && !groundInfo.collider;
        //UpdateAnimationState();

        UpdateAnimation();

        //animator.SetBool("Is Walking", isWalking);
    
        if (CanSeePlayer(agroRange))
        {
            Debug.Log("Can see player indeed");
            ChasePlayer();
        }
        else
        {
            isChasing = false;
            StartCoroutine(MainRoutine());
            Debug.Log("Can't see player indeed");
        }

        /*if (TouchingPlayer())
        {
            SceneManager.Instance.player.Captured(10, 10);
        }*/
    }


    void FixedUpdate()
    {
    }

    protected override IEnumerator MainRoutine()
    {
        if (InFrontOfObstacle() || IsNearEdge())
        {
            isWalking = false;
            yAngle = movingRight? 180: 0;
            yield return new WaitForSeconds(2);
            ChangeFacingDirection();
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * normalSpeed);
            isWalking = true;
        }
    }

    protected override void ChasePlayer()
    {
        isChasing = true;
        if (!IsNearEdge())
        {
            transform.Translate(Vector3.left * Time.deltaTime * chaseSpeed);
        }
        else
        {
            isWalking = false;
        }
    }
}