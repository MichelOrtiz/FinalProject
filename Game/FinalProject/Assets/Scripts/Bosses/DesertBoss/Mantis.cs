using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantis : Enemy//, IBattleBounds
{
    [SerializeField] protected float baseTimeBeforeChase;
    [SerializeField] protected Item itemToGive;
    [SerializeField] protected float decreaseSpeedMultiplier;
    [SerializeField] protected float increaseDamage;
    [SerializeField] private int timesToGiveItem;
    
    [SerializeField] private GameObject battleBoundsPrefab;
    private BattleBounds battleBounds;

    protected float timeBeforeChase;
    protected bool touchingGround;
    protected bool justTouchedGround;
    public int timesItemGiven;



    new protected void Start()
    {
        base.Start();

        timesItemGiven = 0;
        //battleBounds = battleBoundsPrefab.GetComponent<BattleBounds>();
        battleBounds = FindObjectOfType<BattleBounds>();

        eCollisionHandler.StoppedTouchingHandler += eCollisionHandler_StoppedTouchingPlayer;
        eCollisionHandler.TouchingGroundHandler += eCollisionHandler_TouchingGround;
        //battleBounds_SetEventHandler();
    }

    // Update is called once per frame
    new protected void Update()
    {
        if (InFrontOfObstacle() ||( (GetPosition().x > player.GetPosition().x && facingDirection == RIGHT)
            || GetPosition().x < player.GetPosition().x && facingDirection == LEFT) )
            {
                ChangeFacingDirection();
            }

        //touchingGround = collisionHandler.touchingGround;
        
        
        /*if (touchingGround)
        {
            rigidbody2d.velocity = new Vector2();
            touchingGround = false;
        }*/
        base.Update();
    }

    new protected void FixedUpdate()
    {
        
        //if (CanSeePlayer())
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
        //ChangeParentAndChildrenLayer(LayerMask.NameToLayer("Fake"));
    }

    protected virtual void eCollisionHandler_StoppedTouchingPlayer()
    {
        //ChangeParentAndChildrenLayer(LayerMask.NameToLayer("Semi Ghost"));
    }

    protected virtual void eCollisionHandler_TouchingGround()
    {
        rigidbody2d.velocity = new Vector2();
    }

    protected override void ChasePlayer()
    {
        Vector2 playerPosition = player.GetPosition();
        float angleToPlayer = MathUtils.GetAngleBetween(GetPosition(), player.GetPosition());
        Vector2 vectorToPlayer = MathUtils.GetVectorFromAngle(angleToPlayer);

        //RaycastHit2D hit = Physics2D.Raycast(GetPosition(), vectorToPlayer, 100f, LayerMask.NameToLayer("Ground"));
        //Debug.DrawLine(GetPosition(), hit.point);
        Debug.DrawLine(GetPosition(), vectorToPlayer * fieldOfView.ViewDistance, Color.blue);
        
       // rigidbody2d.position = Vector2.MoveTowards(GetPosition(), hit.point, chaseSpeed * Time.deltaTime);
        rigidbody2d.AddForce(vectorToPlayer  * 2500f * 1000f, ForceMode2D.Force);
        //Push(vectorToPlayer.x + (xPushForce * -10000), vectorToPlayer.y + (yPushForce * 10000));
    }

    protected override void MainRoutine()
    {
        //throw new System.NotImplementedException();
    }

    

    void ChangeParentAndChildrenLayer(LayerMask layerMask)
    {
        gameObject.layer = layerMask;
        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.layer = layerMask;
        }
    }

    

}
