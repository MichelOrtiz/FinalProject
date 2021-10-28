using UnityEngine;
public class CastleBLaserBeamer : MonoBehaviour, IBossFinishedBehaviour
{
    #region LaserStuff
    [Header("Laser Stuff")]
    [SerializeField] private LaserShooter laserShooter;
    private Vector2 endPos; 

    [SerializeField] private byte shots;
    private byte shotsDone;
    [SerializeField] private float timeBtwShot;
    #endregion

    [SerializeField] private int maxRayDistance;

    private PlayerManager player;

    public event IBossFinishedBehaviour.Finished FinishedHandler;
    public void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }
    
    void Start()
    {
        player = PlayerManager.instance;

        InvokeRepeating("ShootLaser", timeBtwShot, timeBtwShot);
    }

    void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Linecast(laserShooter.ShotPos.position, (player.GetPosition() - laserShooter.ShotPos.position) * maxRayDistance, 1 << LayerMask.NameToLayer("Ground"));
        
        if (hit.collider == null)
        {
            endPos = new Vector2(hit.point.x, hit.point.y) * maxRayDistance;
        }
        else 
        {
            endPos = hit.point;
        }

        Debug.DrawLine(laserShooter.ShotPos.position, endPos);
        if (shotsDone < shots)
        {
            laserShooter.ShootLaser(endPos);
            shotsDone++;
        }
    }

    void Update()
    {
        if (shotsDone >= shots)
        {
            if (laserShooter.Laser == null)
            {
                OnFinished(transform.position);
            }
        }
    }
}
