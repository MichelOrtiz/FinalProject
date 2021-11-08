using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UBPlatformBehaviour : UBBehaviour
{
    #region Prepare
    [Header("Prepare")]
    [SerializeField] private float startingTime;
    private float currentStartingTime;
    private bool preparing;
    [SerializeField] private float timeBeforePush;
    private float currentTimeBeforePush;
    #endregion

    #region Push
    [Header("Push")]
    [SerializeField] private float pushForce;
    private Vector2 direction;
    private Vector2 push;
    private bool inPush;
    #endregion

    #region Params
    [Header("Params")]
    [SerializeField] private float inPushDamageAmount;
    #endregion
    
    new void Awake()
    {
        base.Awake();
        eCollisionHandler.TouchingGroundHandler += eCollisionHandler_TouchingGround;
        //eCollisionHandler.TouchedPlayerHandler += eCollisionHandler_Attack;
    }

    /*void OnEnable()
    {
        SetDefaults();
    }*/
    
    new void Start()
    {
        base.Start();
        SetDefaults();
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

    void eCollisionHandler_TouchingGround()
    {
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


    protected override void eCollisionHandler_Attack()
    {
        if (inPush)
        {
            player.TakeTirement(inPushDamageAmount);
        }
        else
        {
            player.TakeTirement(startDamageAmount);
        }
        player.currentStaminaLimit -= staminaPunish;
        player.SetImmune();
    }

    protected override void SetDefaults()
    {
        currentStartingTime = 0;
        currentTimeBeforePush = 0;
        preparing = true;
        inPush = true;


        player = PlayerManager.instance;

        direction = (player.GetPosition() - GetPosition()).normalized;
        push = new Vector2(direction.x * pushForce * speed, direction.y * speed * pushForce);
    }
}