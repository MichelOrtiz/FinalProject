using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Enemy : Entity
{
    public enum FovType
    {
        LinearFov, 
        CircularFov
    }

    #region Main Parameters
    [SerializeField] public EnemyName enemyName;
    [SerializeField] protected float damageAmount;
    [SerializeField] protected float normalSpeed;
    [SerializeField] protected float chaseSpeed;
    [SerializeField] protected FovType fovType;
    #endregion

    #region Layers, rigids, etc
    [SerializeField] protected Transform groundCheck;

    [SerializeField] protected float viewDistance;
    [SerializeField] protected Transform fovOrigin; // LinearFov

    [SerializeField] protected float baseCastDistance;
    [SerializeField] protected string facingDirection;
    protected Vector3 baseScale;
    [SerializeField] protected MeshFov meshFov;
    [SerializeField] protected float fov;
    #endregion

    #region Status
    [SerializeField] protected bool touchingPlayer;
    #endregion    

    #region Abstract methods
    protected abstract void MainRoutine();
    protected abstract void ChasePlayer();
    protected abstract void Attack();
    #endregion

    #region Utils
    [SerializeField] protected PlayerManager player;
    #endregion

    #region Unity stuff
    new protected void Start()
    {
        base.Start();
        baseScale = transform.localScale;
        player = ScenesManagers.Instance.player;
        if (fovType == FovType.CircularFov)
        {
            meshFov.SetFov(fov);
            meshFov.SetViewDistance(viewDistance);
        }
    }

    new protected void Update()
    {
        facingDirection = transform.rotation.y == 0? RIGHT:LEFT;
        if (InFrontOfObstacle())
        {
            ChangeFacingDirection();
        }
        UpdateState();
        base.Update();
    }

    protected void FixedUpdate()
    {
        switch (state)
        {
            case State.Chasing:
                ChasePlayer();
                break;
            case State.Paralized:
                //Paralized();
                break;
            case State.Fear:
                Fear();
                break;
            case State.Patrolling:
                MainRoutine();
                break;
        }        
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            if (!player.isImmune)
            {
                Attack();
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        touchingPlayer = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!player.isImmune)
            {
                Attack();
            }
        }
    }
    #endregion

    #region General behaviour methods
    protected bool InFrontOfObstacle()
    {

        float castDistance = facingDirection == LEFT ? -baseCastDistance : baseCastDistance;
        Vector3 targetPos = fovOrigin.position;
        targetPos.x += castDistance;

        return Physics2D.Linecast(fovOrigin.position, targetPos, 1 << whatIsObstacle);
    }

    protected bool IsNearEdge()
    {

        //float castDistance = facingDirection == LEFT ? -baseCastDistance : baseCastDistance;
        //Vector3 targetPos = fovOrigin.position;
        //targetPos.y -= baseCastDistance;

        //return !(Physics2D.Raycast(fovOrigin.position, Vector2.down, 0.2f)).collider;
        return !(Physics2D.Raycast(groundCheck.position, Vector3.down,0.2f)).collider;
    }

    protected bool PlayerSighted()
    {
        if (fovType == FovType.LinearFov)
        {
            return CanSeePlayerLinearFov(viewDistance);
        }
        else
        {
            return CanSeePlayerMeshFov();
        }
    }

    public IEnumerator AfterPlayerReleasedFromCapture()
    {
        isParalized = true;
        rigidbody2d.Sleep();
        yield return new WaitForSeconds(2);
        rigidbody2d.WakeUp();
        isParalized = false;
    }

    protected void ChangeFacingDirection()
    {
        transform.eulerAngles = new Vector3(0, facingDirection == LEFT? 0:180);
    }
    #endregion

    #region Self state methods
    protected void UpdateState()
    {
        state =
            isChasing? State.Chasing :
            isInFear? State.Fear :
            isResting? State.Resting :
            isParalized? State.Paralized :
            isJumping? State.Jumping :
            isFalling? State.Falling :
            State.Patrolling;
    }
    // To call IEnumerators use StartCoroutine() pls
    public IEnumerator Paralized(float time)
    {
        isParalized = true;
        rigidbody2d.Sleep();
        yield return new WaitForSeconds(time);
        rigidbody2d.WakeUp();
        isParalized = false;
    }
    
    // not tested yet
    public IEnumerator Rest()
    {
        isResting = true;
        rigidbody2d.Sleep();
        yield return new WaitUntil(()=>PlayerSighted());
        rigidbody2d.WakeUp();
        isResting = false;
    }
    #endregion


    #region Fov stuff
    public abstract bool CanSeePlayerLinearFov(float distance);

    public bool CanSeePlayerMeshFov()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            Debug.DrawLine(transform.position, dirToPlayer);
            
            if (Vector3.Angle(transform.eulerAngles, dirToPlayer) < fov / 2f)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dirToPlayer, viewDistance);
                return raycastHit2D.collider != null;
            }
        }
        return false;
    }

    #endregion
}