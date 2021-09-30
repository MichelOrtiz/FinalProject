using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantis : Entity//, IBattleBounds
{
    [Header("Giving Item")]
    [SerializeField] protected Item itemToGive;
    [SerializeField] private byte timesToGiveItem;
    public byte timesItemGiven;
    [SerializeField] private float forceIncrease;
    [SerializeField] private float damageIncrease;
    /*[SerializeField] protected EnemyStatsModifier statsModifier;
    private State curState;*/
    

    [Header("Time")]
    [SerializeField] protected float baseTimeBeforeChase;
    protected float timeBeforeChase;

    [Header("Other References")]
    [SerializeField] private float force;
    [SerializeField] private float damage;
    [SerializeField] private EnemyCollisionHandler eCollisionHandler;
    bool canChase;

    private PlayerManager player;

    /*protected bool touchingGround;
    protected bool justTouchedGround;*/




    new protected void Start()
    {
        base.Start();
        player = PlayerManager.instance;


        timesItemGiven = 0;

        eCollisionHandler.TouchedPlayerHandler += eCollisionHanlder_TouchedPlayer;
        eCollisionHandler.StoppedTouchingHandler += eCollisionHandler_StoppedTouchingPlayer;
        eCollisionHandler.TouchingGroundHandler += eCollisionHandler_TouchingGround;


        eCollisionHandler.EnterTouchingContactHandler += eCollisionHandler_EnterContact;
        eCollisionHandler.ExitTouchingContactHandler += eCollisionHandler_ExitContact;
        //battleBounds_SetEventHandler();
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
        if ( (GetPosition().x < player.GetPosition().x && facingDirection == LEFT)
            || (GetPosition().x > player.GetPosition().x && facingDirection == RIGHT) )
            {
                ChangeFacingDirection();
            }

        //touchingGround = collisionHandler.touchingGround;
        
        
        /*if (touchingGround)
        {
            rigidbody2d.velocity = new Vector2();
            touchingGround = false;
        }*/
    }

    protected void FixedUpdate()
    {
        
        if (canChase)
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

    protected void eCollisionHandler_EnterContact(GameObject contact)
    {

        if (contact.CompareTag("Berry"))
        {
            var inter = contact.GetComponent<Inter>();
            if (inter == null) return;
            
            var item = inter.item;
            if (item == itemToGive)
            {
                if (timesItemGiven < timesToGiveItem-1)
                {
                    timesItemGiven++;

                    /*curState?.StopAffect();
                    curState = statesManager.AddState(statsModifier);*/
                    force *= forceIncrease;
                    damage += damageIncrease;
                }
                else
                {
                    DestroyEntity();
                }
                
            }
        }
        else
        {
            if (GroundChecker.IsGround(contact.tag))
            {
                canChase = true;
            }
        }
    }

    void eCollisionHandler_ExitContact(GameObject contact)
    {
        if (GroundChecker.IsGround(contact.tag))
        {
            canChase = false;
        }
    }

    

    protected virtual void eCollisionHanlder_TouchedPlayer()
    {
        PlayerManager.instance.TakeTirement(damage);
        player.SetImmune();
        //ChangeParentAndChildrenLayer(LayerMask.NameToLayer("Fake"));
    }

    protected virtual void eCollisionHandler_StoppedTouchingPlayer()
    {
        //ChangeParentAndChildrenLayer(LayerMask.NameToLayer("Semi Ghost"));
    }

    protected virtual void eCollisionHandler_TouchingGround()
    {
        canChase = true;
        rigidbody2d.velocity = new Vector2();
    }

    protected virtual void ChasePlayer()
    {
        Vector2 playerPosition = player.GetPosition();
        float angleToPlayer = MathUtils.GetAngleBetween(GetPosition(), player.GetPosition());
        Vector2 vectorToPlayer = MathUtils.GetVectorFromAngle(angleToPlayer);

        //RaycastHit2D hit = Physics2D.Raycast(GetPosition(), vectorToPlayer, 100f, LayerMask.NameToLayer("Ground"));
        //Debug.DrawLine(GetPosition(), hit.point);
        //Debug.DrawLine(GetPosition(), vectorToPlayer * fieldOfView.ViewDistance, Color.blue);
        
       // rigidbody2d.position = Vector2.MoveTowards(GetPosition(), hit.point, chaseSpeed * Time.deltaTime);


        rigidbody2d.AddForce(vectorToPlayer * force, ForceMode2D.Force);
        //Push(vectorToPlayer.x + (xPushForce * -10000), vectorToPlayer.y + (yPushForce * 10000));
        
    
    }


    

    /*void ChangeParentAndChildrenLayer(LayerMask layerMask)
    {
        gameObject.layer = layerMask;
        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.layer = layerMask;
        }
    }*/

    

}
