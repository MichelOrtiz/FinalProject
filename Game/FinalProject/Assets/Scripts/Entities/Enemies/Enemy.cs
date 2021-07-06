using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    //public static Enemy instance = null;

    #region Main Parameters
    [Header("Main parameters")]
    [SerializeField] public EnemyName enemyName;
    [SerializeField] protected float normalSpeedMultiplier;
    [SerializeReference] public float normalSpeed;
    [SerializeField] protected float chaseSpeedMultiplier;
    [SerializeReference] public float chaseSpeed;
    [SerializeField] protected float damageAmount;
    [SerializeField] protected State atackEffect;
    [SerializeField] protected State projectileEffect;
    #endregion

    #region Time
    [Header("Time")]
    [SerializeField] protected float startWaitTime;
    protected float waitTime;
    #endregion


    #region Layers, rigids, etc
    [Header("References")]
    [SerializeField] protected FieldOfView fieldOfView;
    [SerializeField] protected EnemyMovement enemyMovement;
    private RaycastHit2D hit;
    //[SerializeField] protected Transform fovOrigin;
    
    // Distance from fovOrigin to check if in front of obstacle
    
    // Fov distance
    //[SerializeField] protected float viewDistance;

    // Fov angle if needed
    //[SerializeField] protected float fovAngle;
    public FieldOfView FieldOfView { get => fieldOfView; }
    [HideInInspector] public EnemyCollisionHandler eCollisionHandler;
    #endregion

    #region Status
    public bool touchingPlayer;
    #endregion

    #region Abstract methods
    protected abstract void MainRoutine();
    
    /// <summary>
    /// What happens when the enemy sees the player
    /// </summary>
    protected abstract void ChasePlayer();
    protected abstract void Attack();
    public abstract void ConsumeItem(Item item);
    #endregion

    #region Utils
    [SerializeField] protected PlayerManager player;
    #endregion

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
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


    #region Unity stuff
    new protected void Start()
    {
        base.Start();
        player = ScenesManagers.Instance.player;
        chaseSpeed = chaseSpeedMultiplier * averageSpeed;
        normalSpeed = normalSpeedMultiplier * averageSpeed;
        
        eCollisionHandler = (EnemyCollisionHandler)base.collisionHandler;
        
        eCollisionHandler.TouchingPlayerHandler += eCollisionHandler_TouchingPlayer;
        
    }

    new protected void Update()
    {
        /*if (InFrontOfObstacle())
        {
            ChangeFacingDirection();
        }*/
        /*if ((GetPosition().x > player.GetPosition().x && facingDirection == RIGHT)
            || GetPosition().x < player.GetPosition().x && facingDirection == LEFT)
            {
                ChangeFacingDirection();
            }*/
        touchingPlayer = eCollisionHandler.touchingPlayer;
        

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

    protected virtual void eCollisionHandler_TouchingPlayer()
    {
        if (!player.isImmune)
        {
            Vector2 direction = player.GetPosition() - GetPosition();
            player.rigidbody2d.velocity = new Vector2();
            player.Push(-direction.x *50, -direction.y * 50);
            Attack();
        }
    } 
    #endregion

    #region General behaviour methods
    
    protected bool InFrontOfObstacle()
    {
        return fieldOfView.inFrontOfObstacle;
    }

    protected bool IsNearEdge()
    {
        return groundChecker.isNearEdge;
    }


    protected void MoveTowardsPlayerInGround(float speed)
    {
        Vector3 playerPosition = (player.isGrounded? player.GetPosition(): new Vector3(player.GetPosition().x, GetPosition().y));
        if (!InFrontOfObstacle() && isGrounded && !touchingPlayer)
        {
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), playerPosition, speed * Time.deltaTime);
        }
    }

    public void Jump(float xForce)
    {
        rigidbody2d.AddForce(new Vector2(xForce,jumpForce),ForceMode2D.Impulse);
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
    #endregion

    

    #region Fov stuff
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