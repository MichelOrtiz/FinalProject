using System.Collections;
using System.Collections.Generic;
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
    private float currentTimeBtwShot;


    #endregion

    RaycastHit2D raycast;
    [SerializeField] private int maxRayDistance;

    private PlayerManager player;

    public event IBossFinishedBehaviour.Finished FinishedHandler;
    public void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }
    

    // Start is called before the first frame update
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

    // Update is called once per frame
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
