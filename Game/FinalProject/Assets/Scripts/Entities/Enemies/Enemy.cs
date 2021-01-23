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

    [SerializeField] protected Transform castPos;
    [SerializeField] protected float baseCastDistance;
    protected string facingDirection;


    protected Vector3 baseScale;

    #endregion

    #region Status
    //public bool inFrontOfObstacle;
    protected bool playerSighted;
    
    //ActionHandler action;
    public delegate void EnemyTouchedPlayer();
    public event EnemyTouchedPlayer OnEnemyTouchedPlayer;

    #endregion

    [SerializeField]protected PlayerManager player;

    new protected void Start()
    {
        base.Start();

        baseScale = transform.localScale;
        player = SceneManager.Instance.player;
    }


    new protected void Update()
    {
        base.UpdateAnimation();
    }

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

    protected abstract IEnumerator MainRoutine();
    protected abstract void ChasePlayer();

    

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