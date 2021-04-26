using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    //public static Enemy instance = null;

    #region Main Parameters
    [Header("Main parameters")]
    [SerializeField] protected FovType fovType;
    [SerializeField] public EnemyName enemyName;
    [SerializeField] protected float damageAmount;
    [SerializeField] protected float normalSpeedMultiplier;
    [SerializeField] protected float chaseSpeedMultiplier;
    [SerializeField]protected State atackEffect;
    [SerializeField]protected State projectileEffect;

    [SerializeField] protected float startWaitTime;
    protected float waitTime;
    public float normalSpeed;
    public float chaseSpeed;

    #endregion

    #region Layers, rigids, etc
    [Header("Layers, rigids, etc")]
    private RaycastHit2D hit;
    [SerializeField] protected Transform? groundCheck;
    [SerializeField] protected Transform? fovOrigin;
    
    // Distance from fovOrigin to check if in front of obstacle
    [SerializeField] protected float baseCastDistance;
    [SerializeField] protected float downDistanceGroundCheck;
    
    // Fov distance
    [SerializeField] protected float viewDistance;

    // Fov angle if needed
    [SerializeField] protected float fovAngle;

    [SerializeField] protected EnemyCollisionHandler collisionHandler;
    #endregion

    #region Status
    public bool touchingPlayer;
    [SerializeField] protected bool justCapturedPlayer;
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

    #region Unity stuff
    /*void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }*/
    new protected void Start()
    {
        base.Start();
        player = ScenesManagers.Instance.player;
        chaseSpeed = chaseSpeedMultiplier * averageSpeed;
        normalSpeed = normalSpeedMultiplier * averageSpeed;

        collisionHandler.TouchingPlayer += collisionHandler_Attack;
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
        touchingPlayer = collisionHandler.touchingPlayer;
        

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

    protected virtual void collisionHandler_Attack()
    {
        if (!player.isImmune)
        {
            Attack();
        }
    } 

    /*/// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody
    /// that is touching rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        // if the enemy is touching the player
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            if (!player.isImmune)
            {
                Attack();
            }
        }
    }
    
    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
        }
    }*/
    #endregion

    #region General behaviour methods
    /// <summary>
    /// Rotates the enemy Y axis
    /// </summary>
    protected void ChangeFacingDirection()
    {
        transform.eulerAngles = new Vector3(0, facingDirection == LEFT? 0:180);
    }
    protected bool InFrontOfObstacle()
    {

        float castDistance = facingDirection == LEFT ? -baseCastDistance : baseCastDistance;
        Vector3 targetPos = fovOrigin.position + (facingDirection == LEFT? Vector3.left : Vector3.right) * castDistance;
        return RayHitObstacle(fovOrigin.position, targetPos);
        //targetPos.x += castDistance;

        /*foreach (var obstacle in whatIsObstacle)
        {
            if (Physics2D.Linecast(fovOrigin.position, targetPos, obstacle))
            {
                return true;
            }
        }
        return false;
        /*return //Physics2D.Linecast(fovOrigin.position, targetPos, 1 << LayerMask.NameToLayer("Obstacles")) || 
                Physics2D.Linecast(fovOrigin.position, targetPos, 1 << LayerMask.NameToLayer("Ground"));*/
    }

    protected bool IsNearEdge()
    {
        // the raycast draws a 0.2f line down and checks if there's something 
        return !(Physics2D.Raycast(groundCheck.position, Vector3.down, 0.3f)).collider;
    }

    // not tested
    public IEnumerator AfterPlayerReleasedFromCapture()
    {
        isParalized = true;
        rigidbody2d.Sleep();
        yield return new WaitForSeconds(2);
        rigidbody2d.WakeUp();
        isParalized = false;
    }

    protected void MoveTowardsPlayerInGround(float speed)
    {
        Vector3 playerPosition = (player.isGrounded? player.GetPosition(): new Vector3(player.GetPosition().x, GetPosition().y));
        if (!InFrontOfObstacle() && isGrounded && !touchingPlayer)
        {
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), playerPosition, speed * Time.deltaTime);
        }
        //rigidbody2d.position = Vector3.MoveTowards(GetPosition(), player.GetPosition(), speed * Time.deltaTime);
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
    // To call IEnumerators use StartCoroutine() pls
    public IEnumerator Paralized(float time)
    {
        isParalized = true;
        rigidbody2d.Sleep();
        Debug.Log("Paralized");
        yield return new WaitForSeconds(time);
        rigidbody2d.WakeUp();
        isParalized = false;
        Debug.Log("Not paralized");
    }
    
    // not tested yet
    public IEnumerator Rest()
    {
        isResting = true;
        rigidbody2d.Sleep();
        yield return new WaitUntil(()=>CanSeePlayer());
        rigidbody2d.WakeUp();
        isResting = false;
    }
    #endregion

    

    #region Fov stuff
    /// <summary>
    /// Checks if the enemy is able to see the player based on its field of view
    /// </summary>
    /// <returns></returns>
    protected bool CanSeePlayer()
    {
        Vector3 endPos = fovOrigin.position;
        
        Vector3 dir = player.GetPosition() - fovOrigin.position;
 
        //      90
        //  180     0 or 360
        //      270       
        float angle = GetAngleFromPlayer();

        switch (fovType)
        {
            case FovType.Linear:
                if (facingDirection == LEFT)
                {
                    endPos = fovOrigin.position + Vector3.left * viewDistance;
                }
                else
                {
                    endPos = fovOrigin.position + Vector3.right * viewDistance;
                }
                break;
            case FovType.CircularFront:
                if (facingDirection == RIGHT)
                {
                    if ( (angle > 0 && angle < 90 && angle < 0 + fovAngle/2) ||
                        (angle > 270 && angle < 360 && angle > 360 - fovAngle/2) )
                    {
                        endPos = Vector3.MoveTowards(fovOrigin.position, player.GetPosition(), viewDistance);
                    }
                }
                else
                {
                    if (angle > (180 - fovAngle/2) && angle < 270 && angle < 180 + fovAngle/2 && angle < 180 + fovAngle/2) 
                    {
                        endPos = Vector3.MoveTowards(fovOrigin.position, player.GetPosition(), viewDistance);
                    }
                }
                break;
            case FovType.CircularDown:
                if (angle > 180 && angle < 360 && angle > 270 - fovAngle/2 && angle < 270 + fovAngle/2)
                {
                    endPos = Vector3.MoveTowards(fovOrigin.position, player.GetPosition(), viewDistance);
                }
                break;
            case FovType.CircularUp:
                if (angle < 180 && angle > 0 && angle > 90 - fovAngle/2 && angle < 90 + fovAngle/2)
                {
                    endPos = Vector3.MoveTowards(fovOrigin.position, player.GetPosition(), viewDistance);
                }
                break;
            case FovType.CompleteCircle:
                endPos = Vector3.MoveTowards(fovOrigin.position, player.GetPosition(), viewDistance);
                break;
        }
        Debug.DrawLine(fovOrigin.position, endPos, Color.red);

        if (!RayHitObstacle(fovOrigin.position, endPos))
        {
            hit = Physics2D.Linecast(fovOrigin.position, endPos, 1 << LayerMask.NameToLayer("Default"));
            if (hit.collider == null)
            {
                return false;
            }
            return hit.collider.gameObject.CompareTag("Player");
        }
        return false;
        
    }

    public float GetDistanceFromPlayerFov()
    {
        return Math.Abs(hit.distance);
    }

    public float GetAngleFromPlayer()
    {
        return MathUtils.GetAngleBetween(fovOrigin.position, player.GetPosition());
    }

    

    #endregion

    //public void push
}