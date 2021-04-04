using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantis : Enemy
{
    [SerializeField] protected float baseTimeBeforeChase;
    [SerializeField] protected Item itemToGive;
    [SerializeField] protected float decreaseSpeedMultiplier;
    [SerializeField] protected float increaseDamage;
    [SerializeField] private int timesToGiveItem;

    protected float timeBeforeChase;
    protected bool touchingGround;
    public int timesItemGiven;

    new void Start()
    {
        timesItemGiven = 0;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if (InFrontOfObstacle() ||( (GetPosition().x > player.GetPosition().x && facingDirection == RIGHT)
            || GetPosition().x < player.GetPosition().x && facingDirection == LEFT) )
            {
                ChangeFacingDirection();
            }
        if (touchingGround)
        {
            rigidbody2d.velocity = new Vector2();
            touchingGround = false;
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
            if (timesItemGiven < timesToGiveItem-1)
            {
                timesItemGiven++;
                chaseSpeed *= decreaseSpeedMultiplier;
                damageAmount += increaseDamage;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }

    protected override void Attack()
    {
        PlayerManager.instance.TakeTirement(damageAmount);
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
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        //base.OnCollisionEnter2D(other);
        if (other.gameObject.tag == "Ground")
        {
            touchingGround = true;
        }
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            if (!PlayerManager.instance.isImmune)
            {
                Attack();
            }
            gameObject.layer = LayerMask.NameToLayer("Fake");
        }
    }

    protected override void OnCollisionExit2D(Collision2D other)
    {
        base.OnCollisionExit2D(other);
        if (other.gameObject.tag == "Ground")
        {
            touchingGround = false;
        }
        if (other.gameObject.tag == "Player")
        {
            gameObject.layer = LayerMask.NameToLayer("Enemies");
        }
    }

    /*private void TouchingGround()
    {
        RaycastHit2D = 
    }*/
}
