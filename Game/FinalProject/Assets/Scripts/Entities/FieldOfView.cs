using System;
using System.Collections.Generic;
using UnityEngine;
public class FieldOfView : MonoBehaviour
{
    #region FieldOfView
    [Header("Field Of View")]
    [SerializeField] private Transform fovOrigin;
    public Transform FovOrigin { get => fovOrigin; }

    [SerializeField] private FovType fovType;
    public FovType FovType { get => fovType; set => fovType = value; }

    [SerializeField] private float fovAngle;
    public float FovAngle { get => fovAngle; }

    [SerializeField] private float viewDistance;
    public float ViewDistance
    {
        get { return viewDistance; }
        set { viewDistance = value; }
    }
    
    public bool blockFov;


    [SerializeField] private LayerMask layerMask;

    public bool canSeePlayer;
    public bool justSeenPlayer;
    public bool inFrontOfObstacle;
    private RaycastHit2D hit;
    #endregion

    #region ObstacleChecking
    [Header("Obstacle Checking")]
    [SerializeField] private LayerMask whatIsObstacle;
    public LayerMask WhatIsObstacle { get => whatIsObstacle; }
    [SerializeField] private Transform obstacleCheckOrigin;
    public Transform ObstacleCheckOrigin { get => obstacleCheckOrigin; }
    [SerializeField] private float obstacleViewDistance;
    public float ObstacleViewDistance { get => obstacleViewDistance; }

    [SerializeReference] private GameObject blockingCollider;
    #endregion

    #region Direction
    [Header("Direction")]
    [SerializeReference] private string facingDirection;
    private const string RIGHT = "right";
    private const string LEFT = "left";
    #endregion

    #region References
    private PlayerManager player;
    [SerializeField] private Entity entity;

    #endregion

    #region Events
    public delegate void FrontOfObstacle();
    public event FrontOfObstacle FrontOfObstacleHandler;
    protected virtual void OnInFrontOfObstacle()
    {
        FrontOfObstacleHandler?.Invoke();
    }

    public Action PlayerSighted;
    public Action PlayerUnsighted;
    #endregion

    void Start()
    {
        player = PlayerManager.instance;
        entity = GetComponent<Entity>();

        if (entity == null)
        {
            entity = transform.parent.GetComponentInChildren<Entity>();
        }
    }

    void Update()
    {
        facingDirection = entity.facingDirection;
        canSeePlayer = CanSeePlayer() && !blockFov;
        inFrontOfObstacle = InFrontOfObstacle();
       
        if(inFrontOfObstacle)
        {
            OnInFrontOfObstacle();
        }

        if (canSeePlayer)
        {
            PlayerSighted?.Invoke();
            justSeenPlayer = true;
        }
        if (justSeenPlayer)
        {
            PlayerUnsighted?.Invoke();
        }

        if (hit) Debug.DrawLine(fovOrigin.position, hit.point, Color.red);
    }

    void FixedUpdate()
    {
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        justSeenPlayer = !CanSeePlayer() && justSeenPlayer;
    }

    public void SetViewDistanceOnRayHitObstacle(Vector2 direction, float maxViewDistance)
    {
        float distance = 0;
        RaycastHit2D raycast = Physics2D.Linecast(fovOrigin.position,(Vector2)fovOrigin.position + direction * maxViewDistance, whatIsObstacle);
        if (raycast)
        {
            if (raycast.collider != null)
            {
                distance = raycast.distance;
            }
        }
        if (distance > 0)
        {
            viewDistance = distance;
        }
        else
        {
            viewDistance = maxViewDistance;
        }
    }

    private bool CanSeePlayer()
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
                endPos = fovOrigin.position + fovOrigin.right * viewDistance;
                /*if (facingDirection == LEFT)
                {
                    endPos = fovOrigin.position - fovOrigin.right * viewDistance;
                }
                else
                {
                    endPos = fovOrigin.position + fovOrigin.right * viewDistance;
                }*/
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
        Debug.DrawLine(fovOrigin.position, endPos, Color.green);

        //if (!RayHitObstacle(fovOrigin.position, endPos))
        {
            var hits = Physics2D.LinecastAll(fovOrigin.position, endPos, layerMask);
            hit = Array.Find(hits, h => h.collider.CompareTag("Player"));
            //hit = Physics2D.Linecast(fovOrigin.position, endPos, layerMask);//1 << LayerMask.NameToLayer("Default"));
            /*if (hit.collider == null)
            {

                return false;
            }
            blockingCollider = hit.collider.gameObject;
            return hit.collider.gameObject.CompareTag("Player");    */
            if (hit) return true;
        }
        //Debug.Log("hit collider of " + entity.gameObject + " false");
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

    protected bool InFrontOfObstacle()
    {
        //float castDistance = facingDirection == LEFT ? -obstacleViewDistance : obstacleViewDistance;
        //Vector2 targetPos = (Vector2)obstacleCheckOrigin.position + (facingDirection == LEFT? Vector2.left : Vector2.right) * obstacleViewDistance;
        Vector2 targetPos = obstacleCheckOrigin.position + obstacleCheckOrigin.right * obstacleViewDistance;
        Debug.DrawLine(obstacleCheckOrigin.position, targetPos, Color.blue);
        return RayHitObstacle(obstacleCheckOrigin.position, targetPos);
    }


    public bool RayHitObstacle(Vector2 start, Vector2 end)
    {
        RaycastHit2D linecast = Physics2D.Linecast(start, end, whatIsObstacle);
        /*var linecast = Array.Find(linecasts, l =>  whatIsObstacle == (whatIsObstacle | (1 << l.collider.gameObject.layer)));
        if (!linecast) return false;*/
        //Debug.Log(entity.gameObject + " Raycast hit " + Physics2D.GetRayIntersection(new Ray(start, end), Vector2.Distance(start, end), whatIsObstacle));// .OverlapArea(start, end, whatIsObstacle)?.gameObject);
        //return Physics2D.OverlapArea(start, end, whatIsObstacle);
        //return Physics2D.GetRayIntersection(new Ray(start, end), Vector2.Distance(start, end), whatIsObstacle).collider;
        return linecast.collider;
        //if (linecast.collider != null) Debug.Log(linecast.collider.gameObject.layer + " obstacle value: " + obstacle);
        /*if (linecast.collider != null && linecast.collider.IsTouchingLayers(whatIsObstacle))// gameObject. layer == whatIsObstacle)
        {
            return true;
        }*/
        /*foreach (var obstacle in whatIsObstacle)
        {
            RaycastHit2D linecast = Physics2D.Linecast(start, end, layerMask);
            if (linecast.collider != null) Debug.Log(linecast.collider.gameObject.layer + " obstacle value: " + obstacle);
            if (linecast.collider != null && linecast.collider.gameObject.layer == obstacle.value)
            {
                return true;
            }
        }*/
        //return false;
    }

    /*public bool InFrontOfPlayerAndSameFacingDirection()
    {
        return 
            (player.GetPosition().x > entity.GetPosition().x
            player.GetPosition().x < entity.GetPosition().x) && (player.facingDirection == facingDirection) 
    }
*/

    public static RaycastHit2D GetRaycastOnColliderHit(Vector2 start, Vector2 direction, float maxDistance, LayerMask whatIsObstacle)
    {
        return Physics2D.Raycast(start,  direction,  maxDistance, whatIsObstacle);
    }

    public static bool RayHitObstacle(Vector2 start, Vector2 end, LayerMask whatIsObstacle)
    {
        RaycastHit2D linecast = Physics2D.Linecast(start, end, whatIsObstacle);
    
        return linecast.collider;
    }

}