using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UBPlatformBehaviour : UBBehaviour
{
    [SerializeField] private float startingTime;
    private float currentStartingTime;
    private bool preparing;

    PlayerManager player;
    [SerializeField] private float speedMultiplier;
    private float chaseSpeed;

    [SerializeField] private float pushForce;

    private Vector2 direction;
    private Vector2 push;

    [SerializeField] private float timeBeforePush;
    private float currentTimeBeforePush;
    private bool inPush;
    
    [SerializeField] private float startDamageAmount;
    [SerializeField] private float inPushDamageAmount;
    
    new private EnemyCollisionHandler collisionHandler;

    private bool defaultsSet;
    
    void Awake()
    {
        collisionHandler = (EnemyCollisionHandler) base.collisionHandler;

        collisionHandler.TouchingGroundHandler += collisionHandler_TouchingGround;
        collisionHandler.TouchingPlayer += collisionHandler_Attack;

       inPush = true;
       preparing = true;
    }
    
    new void Start()
    {
        base.Start();
        
        //SetDefaults();
        SetDefaults();

        
        
    }

    new void Update()
    {
        
        base.Update();
    }

    void FixedUpdate()
    {
        if (!finishedAttack)
        {
            if (inPush)
            {
                if (currentStartingTime < startingTime)
                {
                    currentStartingTime += Time.deltaTime;
                }

                if (currentTimeBeforePush > timeBeforePush)
                {
                    rigidbody2d.AddForce(push * Time.deltaTime, ForceMode2D.Impulse);
                }
                else
                {
                    rigidbody2d.AddForce(-push * 0.01f * Time.deltaTime, ForceMode2D.Impulse);
                    currentTimeBeforePush += Time.deltaTime;
                }
            }
        }
    }

    void collisionHandler_TouchingGround()
    {
        Debug.Log("preparing " + preparing);
        if (currentStartingTime > startingTime)
        {
            preparing = false;
        }

        if (!preparing)
        {
            rigidbody2d.velocity = new Vector2();
            currentTimeBeforePush = 0;
            inPush = false;

            OnFinishedAttack();
        }
    }


    void collisionHandler_Attack()
    {
        if (inPush)
        {
            player.TakeTirement(inPushDamageAmount);
        }
        else
        {
            player.TakeTirement(startDamageAmount);
        }
    }

    
    new void OnFinishedAttack()
    {
        defaultsSet = false;
        base.OnFinishedAttack();
    }

    new void SetActive(bool value)
    {
        base.SetActive(value);
    }

    protected override void SetDefaults()
    {
        currentStartingTime = 0;
        currentTimeBeforePush = 0;
        preparing = true;
        inPush = true;


        player = PlayerManager.instance;

        direction = (player.GetPosition() - GetPosition()).normalized;
        push = new Vector2(direction.x * pushForce * speedMultiplier, direction.y * speedMultiplier * pushForce);

        
        Debug.Log("Defaults Set");
    }
}
