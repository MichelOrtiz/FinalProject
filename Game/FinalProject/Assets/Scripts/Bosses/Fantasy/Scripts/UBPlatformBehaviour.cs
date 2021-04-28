using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UBPlatformBehaviour : Entity
{
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
    
    void Awake()
    {
        collisionHandler = (EnemyCollisionHandler) base.collisionHandler;

        collisionHandler.TouchingGroundHandler += collisionHandler_TouchingGround;
        collisionHandler.TouchingPlayer += collisionHandler_Attack;

        inPush = true;
    }
    
    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        direction = (player.GetPosition() - GetPosition()).normalized;
        push = new Vector2(direction.x * pushForce * speedMultiplier, direction.y * speedMultiplier * pushForce);

        rigidbody2d.AddForce(-push * Time.deltaTime, ForceMode2D.Impulse);

    }

    new void Update()
    {
        if (inPush)
        {
            if (currentTimeBeforePush > timeBeforePush)
            {
                rigidbody2d.AddForce(push * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                currentTimeBeforePush += Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {

    }

    void collisionHandler_TouchingGround()
    {
        rigidbody2d.velocity = new Vector2();
        inPush = false;
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
}
