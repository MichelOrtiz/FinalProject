using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBLaserBeamer : MonoBehaviour, ILaser, IBossFinishedBehaviour
{
    #region LaserStuff
    [Header("Laser Stuff")]
    [SerializeField] private float laserDamage;
    [SerializeField] private GameObject laserPrefab;
    private Laser laser; 
    [SerializeField] private Transform shotPos;
    public Transform ShotPos { get => shotPos; }
    private Vector2 endPos; 
    public Vector2 EndPos { get => endPos; }

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
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Linecast(shotPos.position, (player.GetPosition() - shotPos.position) * maxRayDistance, 1 << LayerMask.NameToLayer("Ground"));
        
        if (hit.collider == null)
        {
            endPos = new Vector2(hit.point.x, hit.point.y) * maxRayDistance;
        }
        else 
        {
            endPos = hit.point;
        }

        Debug.DrawLine(shotPos.position, endPos);

        if (shotsDone < shots)
        {
            if (currentTimeBtwShot > timeBtwShot)
            {
                

                //ShootLaser(shotPos.position, player.GetPosition());
                ShootLaser(shotPos.position, endPos);

                shotsDone++;
                currentTimeBtwShot = 0;
            }
            else
            {
                currentTimeBtwShot += Time.deltaTime;
            }
        }
        else if (laser == null)
        {
            OnFinished(transform.position);
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        
    }

    public void LaserAttack()
    {
        player.TakeTirement(laserDamage);
    }

    public void ShootLaser(Vector2 from, Vector2 to)
    {
        laser = Instantiate(laserPrefab, from, Quaternion.identity).GetComponent<Laser>();
        laser.Setup(from, to, this);
    }
}
