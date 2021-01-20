using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Enemy : Entity
{

    #region Main Parameters
    [SerializeField] protected float damageAmount;
    [SerializeField] protected float normalSpeed;
    [SerializeField] protected float chaseSpeed;
    [SerializeField] protected float agroRange; // for fieldOfView

    protected bool movingRight;
    protected int yAngle;
    #endregion

    #region Layers, rigids, etc

    //  Using 2 raycasts to check: Obstacles and Ground that may be on its way
    [SerializeField] protected float collisionCheckRadius;
    [SerializeField] protected float groundCheckRadius;
    
    [SerializeField] protected Transform collisionChecker;
    [SerializeField] protected Transform groundChecker;
    [SerializeField] protected RaycastHit2D collisionInfo;
    [SerializeField] protected RaycastHit2D groundInfo;

    [SerializeField] protected Transform castPos;
    [SerializeField] protected float baseCastDistance;
    protected string facingDirection;


    protected Vector3 baseScale;

    [SerializeField]protected LayerMask whatIsGround;
    #endregion

    #region Status
    //public bool inFrontOfObstacle;
    protected bool playerSighted;

    #endregion

    [SerializeField]protected PlayerManager player;

    protected bool InFrontOfObstacle()
    {

        float castDistance = facingDirection == LEFT ? -baseCastDistance : baseCastDistance;
        Vector3 targetPos = castPos.position;
        targetPos.x += castDistance;

        return Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"));
    }

    protected bool IsNearEdge()
    {

        //float castDistance = facingDirection == LEFT ? -baseCastDistance : baseCastDistance;
        Vector3 targetPos = castPos.position;
        targetPos.y -= baseCastDistance;

        return !Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"));
    }
    
    protected bool TouchingPlayer()
    {
        Vector3 targetPos = castPos.position;

        return Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Player"));
    }

    protected abstract IEnumerator MainRoutine();
    protected abstract void ChasePlayer();

    protected void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseScale = transform.localScale;
        player = SceneManager.Instance.player;
    }

    protected void Update()
    {
        //float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //playerSighted = distanceToPlayer < agroRange;
    }

    protected void ChangeFacingDirection()
    {
        transform.eulerAngles = new Vector3(0, yAngle, 0);
        facingDirection = facingDirection ==  LEFT? RIGHT:LEFT;
        movingRight = !movingRight;
    }

    protected bool CanSeePlayer(float distance)
    {
        Vector2 endPos;
        //endPos = castPos.position + Vector3.left * distance;
        if (facingDirection == LEFT)
        {
            endPos = castPos.position + Vector3.left * distance;
        }
        else
        {
            endPos = castPos.position + Vector3.right * distance;
        }

        RaycastHit2D hit = Physics2D.Linecast(castPos.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        
        if (hit.collider == null)
        {
            return false;
        }
        Debug.DrawLine(castPos.position, endPos, Color.blue);
        return hit.collider.gameObject.CompareTag("Player");
    }

}