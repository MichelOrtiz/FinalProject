using UnityEngine;
public class FieldOfView : MonoBehaviour
{
    [SerializeField] Transform fovOrigin;
    [SerializeField] private FovType fovType;
    [SerializeField] private float viewDistance;
    
    private string facingDirection;
    private const string RIGHT = "right";
    private const string LEFT = "left";

    private PlayerManager player;
    private bool canSeePlayer;
    private float whatIsObstacle;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        player = PlayerManager.instance;
    }

    void Update()
    {
       // canSeePlayer = 
    }
/*
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
            hit = Physics2D.Linecast(fovOrigin.position, endPos, 1 << LayerMask.NameToLayer("Default"));
            if (hit.collider == null)
            {
                //Debug.Log("Collider null");
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
    }*/

    
}