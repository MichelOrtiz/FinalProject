using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantis : Enemy
{
    [SerializeField] protected float xPushForce;
    [SerializeField] protected float yPushForce;
    [SerializeField] protected float baseTimeBeforeChase;
    [SerializeField] protected Item itemToGive;
    [SerializeField] protected float decreaseSpeedMultiplier;
    [SerializeField] protected float increaseDamage;

    protected float timeBeforeChase;
    protected bool canChasePlayer;
    protected bool touchingGround;
    public int timesItemGiven;

    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if (touchingGround)
        {
            rigidbody2d.velocity = new Vector2();
            touchingGround = false;
        }
        if (touchingPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Fake");
        }
        base.Update();
    }

    new protected void FixedUpdate()
    {
        
        if (CanSeePlayer())
        {
            if (timeBeforeChase > baseTimeBeforeChase)
            {
                ChasePlayer();
                timeBeforeChase = 0;
            }
            else
            {
                timeBeforeChase += Time.deltaTime;
            }
        }
    }

    public override void ConsumeItem(Item item)
    {
        if (item == itemToGive)
        {
            timesItemGiven++;
            chaseSpeed *= decreaseSpeedMultiplier;
            damageAmount += increaseDamage;
        }
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }

    protected override void ChasePlayer()
    {
        Vector2 playerPosition = player.GetPosition();
        float angleToPlayer = MathUtils.GetAngleBetween(GetPosition(), player.GetPosition());
        Vector2 vectorToPlayer = MathUtils.GetVectorFromAngle(angleToPlayer);
        Debug.DrawLine(GetPosition(), vectorToPlayer * viewDistance, Color.blue);
        rigidbody2d.AddForce(vectorToPlayer * 2500f * 1000f, ForceMode2D.Force);
        //Push(vectorToPlayer.x + (xPushForce * -10000), vectorToPlayer.y + (yPushForce * 10000));
    }

    protected override void MainRoutine()
    {
        //throw new System.NotImplementedException();
    }
    new protected void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (other.gameObject.tag == "Fake")
        {
            touchingGround = true;
        }
    }

    new protected void OnCollisionExit2D(Collision2D other)
    {
        base.OnCollisionExit2D(other);
        if (other.gameObject.tag == "Ground")
        {
            touchingGround = false;
        }
        if (other.gameObject.tag == "Player")
        {
            gameObject.layer = LayerMask.NameToLayer("Ghost");
        }
    }

    /*private void TouchingGround()
    {
        RaycastHit2D = 
    }*/
}
