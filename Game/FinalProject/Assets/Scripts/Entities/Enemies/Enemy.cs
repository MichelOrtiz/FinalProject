using System;
using UnityEngine;

public abstract class Enemy : Entity
{
    //public static Enemy instance = null;

    #region Main Parameters
    [Header("Main parameters")]
    [SerializeField] public EnemyType enemyType;
    [SerializeField] public EnemyName enemyName;
    [SerializeField] protected bool flipToPlayerIfSpotted;
    [SerializeField] protected float normalSpeedMultiplier;
    [SerializeReference] public float normalSpeed;
    [SerializeField] protected float chaseSpeedMultiplier;
    [SerializeReference] public float chaseSpeed;

    [Header("Effect on Player")]
    [SerializeField] protected float damageAmount;
    [SerializeField] protected State atackEffect;
    [SerializeField] protected bool canKnockbackPlayer;
    [SerializeField] private float knockbackAngle;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockBackForce;
    #endregion

    #region Layers, rigids, etc
    [Header("References")]
    [SerializeField] protected FieldOfView fieldOfView;
    [SerializeField] protected EnemyMovement enemyMovement;
    [SerializeField] protected ProjectileShooter projectileShooter;
    private RaycastHit2D hit;
    public FieldOfView FieldOfView { get => fieldOfView; }
    [HideInInspector] public EnemyCollisionHandler eCollisionHandler;
    protected PlayerManager player;
    public bool touchingPlayer;
    #endregion

    #region eCollisionHanlder && FieldOfView Event Subs
    protected virtual void fieldOfView_InFrontOfObstacle(){}
    protected virtual void eCollisionHandler_TouchingPlayer(){}
    protected virtual void eCollisionHandler_TouchedPlayer()
    {
        Attack();
    }
    #endregion

    #region Unity stuff
    protected void Awake()
    {
        if (enemyMovement == null)
        {
            enemyMovement = GetComponent<EnemyMovement>();
        }
        if (fieldOfView == null)
        {
            fieldOfView = GetComponent<FieldOfView>();
        }
    }


    new protected void Start()
    {
        base.Start();
        player = ScenesManagers.Instance.player;
        chaseSpeed = chaseSpeedMultiplier * averageSpeed;
        normalSpeed = normalSpeedMultiplier * averageSpeed;
        
        eCollisionHandler = (EnemyCollisionHandler)base.collisionHandler;
        
        //eCollisionHandler.TouchingPlayerHandler += eCollisionHandler_TouchingPlayer;
        if (eCollisionHandler != null)
        {
            eCollisionHandler.TouchedPlayerHandler += eCollisionHandler_TouchedPlayer;
            fieldOfView.FrontOfObstacleHandler += fieldOfView_InFrontOfObstacle;
        }
    }

    new protected void Update()
    {
        //Debug.DrawLine(GetPosition(), transform.TransformPoint((MathUtils.GetVectorFromAngle(knockbackAngle)).normalized));
        if (isChasing && flipToPlayerIfSpotted && MathUtils.GetAbsXDistance(GetPosition(), player.GetPosition()) > 1f)
        {
            if ((GetPosition().x > player.GetPosition().x && facingDirection == RIGHT)
                || GetPosition().x < player.GetPosition().x && facingDirection == LEFT)
                {
                    if (rigidbody2d.gravityScale == 0 ||  groundChecker.isGrounded) ChangeFacingDirection();
                }
        }
        touchingPlayer = eCollisionHandler.touchingPlayer;
        
        SetStates();
        UpdateState();
        base.Update();
    }

    protected void FixedUpdate()
    {
        switch (currentState)
        {
            case StateNames.Chasing:
                ChasePlayer();
                break;
            case StateNames.Paralized:
                //justCapturedPlayer;
                break;
            case StateNames.Fear:
                //Fear();
                break;
            case StateNames.Patrolling:
                MainRoutine();
                break;
        }
    }
    #endregion

    
    
    #region Behaviour (mainly called by current state)
    protected virtual void MainRoutine() {}
    
    /// <summary>
    /// What happens when the enemy sees the player
    /// </summary>
    protected virtual void ChasePlayer() {}

    protected virtual void Attack()
    {
        if(atackEffect != null)
        {
            player.statesManager.AddState(atackEffect,this);
        }
        player.TakeTirement(damageAmount);

        if (canKnockbackPlayer)
        {
            KnockbackEntity(player);
        }

        enemyMovement?.StopMovement();
    }
    public virtual void ConsumeItem(Item item)
    {
        Debug.Log("Consumiendo "+ item.nombre);
    }
    #endregion

    #region Effects on self or other
    protected virtual void KnockbackEntity(Entity entity)
    {
        entity.Knockback
            (
                knockbackDuration, 
                knockBackForce,
                
                facingDirection == entity.facingDirection ?
                entity.transform.InverseTransformPoint
                (
                    entity.GetPosition() + 
                    (MathUtils.GetVectorFromAngle(knockbackAngle)
                    ))
                    :
                -entity.transform.InverseTransformPoint
                (
                    entity.GetPosition() + 
                    (MathUtils.GetVectorFromAngle(-knockbackAngle)
                    ))
            );
    }

    public void EnhanceValues(float multiplier)
    {
        enemyMovement.DefaultSpeed *= multiplier;
        enemyMovement.ChaseSpeed *= multiplier;
        enemyMovement.JumpForce *= multiplier;
        damageAmount *= multiplier;
    }

    public void NerfValues(float divider)
    {
        enemyMovement.DefaultSpeed /= divider;
        enemyMovement.ChaseSpeed /= divider;
        enemyMovement.JumpForce /= divider;
        damageAmount /= divider;
    }
    #endregion

    #region Self state methods
    protected void UpdateState()
    {
        currentState =
            isChasing? StateNames.Chasing :
            isInFear? StateNames.Fear :
            isResting? StateNames.Resting :
            isParalized? StateNames.Paralized :
            isJumping? StateNames.Jumping :
            isFalling? StateNames.Falling :
            StateNames.Patrolling;
    }

    protected virtual void SetStates()
    {
        isChasing = fieldOfView.canSeePlayer;
    }
    
    protected bool InFrontOfObstacle()
    {
        return fieldOfView.inFrontOfObstacle;
    }

    protected bool IsNearEdge()
    {
        return groundChecker.isNearEdge;
    }
    #endregion

    #region Delete Later

    /// <summary>
    /// Checks if the enemy is able to see the player based on its field of view
    /// </summary>
    /// <returns></returns>
    protected bool CanSeePlayer()
    {
        return fieldOfView.canSeePlayer;
    }

    public float GetDistanceFromPlayerFov()
    {
        return Math.Abs(hit.distance);
    }

    #endregion
}