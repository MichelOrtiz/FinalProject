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
    public FovType FovType { get => fovType; }

    [SerializeField] private float fovAngle;
    public float FovAngle { get => fovAngle; }

    [SerializeField] private float viewDistance;
    public float ViewDistance { get => viewDistance; }

    [SerializeField] private LayerMask layerMask;

    public bool canSeePlayer;
    public bool inFrontOfObstacle;
    private RaycastHit2D hit;
    #endregion

    #region ObstacleChecking
    [Header("Obstacle Checking")]
    [SerializeField] private LayerMask whatIsObstacle;
    [SerializeField] private Transform obstacleCheckOrigin;
    [SerializeField] private float obstacleViewDistance;
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
        canSeePlayer = CanSeePlayer();
        inFrontOfObstacle = InFrontOfObstacle();
       
        if(inFrontOfObstacle)
        {
            OnInFrontOfObstacle();
        }
    }

    void FixedUpdate()
    {
    }

    public void SetViewDistanceOnRayHitObstacle(Vector2 direction, float maxViewDistance)
    {
        viewDistance = maxViewDistance;
        float distance = 0;
        //RaycastHit2D rayHit;
        Collider2D collider = Physics2D.OverlapArea(fovOrigin.position,(Vector2) fovOrigin.position + direction * maxViewDistance, whatIsObstacle);
        if (collider != null)
        {
            distance = Vector2.Distance(fovOrigin.position, collider.ClosestPoint(fovOrigin.position));
        }
        /*foreach (var obstacle in whatIsObstacle)
        {
            rayHit = Physics2D.Raycast(fovOrigin.position, direction, maxViewDistance, obstacle);
            if (rayHit)
            {
                if (distance == 0 || rayHit.distance < distance)
                {
                    distance = rayHit.distance;
                }
            }
        }*/
        if (distance > 0)
        {
            viewDistance = distance;
        }
        else
        {
            viewDistance = maxViewDistance;
        }
    }

    public bool CanSeePlayer()
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
            hit = Physics2D.Linecast(fovOrigin.position, endPos, layerMask);//, 1 << LayerMask.NameToLayer("Default"));
            if (hit.collider == null)
            {
                return false;
            }
            return hit.collider.gameObject.CompareTag("Player");
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
        Vector2 targetPos = (Vector2)fovOrigin.position + (facingDirection == LEFT? Vector2.left : Vector2.right) * obstacleViewDistance;
        Debug.DrawLine(fovOrigin.position, targetPos, Color.blue);
        return RayHitObstacle(fovOrigin.position, targetPos);
    }


    protected bool RayHitObstacle(Vector2 start, Vector2 end)
    {
        RaycastHit2D linecast = Physics2D.Linecast(start, end, whatIsObstacle);
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
}