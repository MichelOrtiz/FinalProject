using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Enemy
{
    new void Start()
    {
        base.Start();
    }
    new void Update()
    {
        base.Update();
        
        if (CanSeePlayer(agroRange))
        {
            //Debug.Log("Can see player indeed");
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            isChasing = false;
            StartCoroutine(MainRoutine());
            //Debug.Log("Can't see player indeed");
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
        if (!IsNearEdge())
        {
            transform.Translate(Vector3.left * Time.deltaTime * chaseSpeed);
        }
        else
        {
            isWalking = false;
        }
    }

   
   
    /*void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collision witdfad");
            
        }
    }*/

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            
        }
    }

}