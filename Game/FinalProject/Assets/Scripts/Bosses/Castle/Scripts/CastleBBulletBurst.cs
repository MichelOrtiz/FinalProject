using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBBulletBurst : MonoBehaviour, IProjectile, IBossFinishedBehaviour
{
    #region TotalTime
    [Header("Total Time")]
    [SerializeField] private float totalTime;
    private float currentTime;
    #endregion

    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;

    [SerializeField] private Transform shotPoint;
    private Vector2 shotTarget;
    
    [SerializeField] private float timeBtwShot;
    private float currentTimeBtwShot;

    /*[SerializeField] private float burstTime;
    private float curBurstTime;*/

    [SerializeField] private byte shotsPerBurst;
    private byte curShots;

    [SerializeField] private float timeBtwBurst;
    private float curTimeBtwBurst;
    #endregion

    private PlayerManager player;
    private Vector2 lastPlayerPos;


    public event IBossFinishedBehaviour.Finished FinishedHandler;
    public void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }


    void Start()
    {
        player = PlayerManager.instance;

        shotTarget = (Vector2) player.GetPosition();

    }


    void Update()
    {
        if (currentTime <= totalTime)
        {
            if (curTimeBtwBurst > timeBtwBurst)
            {
                if (curShots > shotsPerBurst-1)
                {

                    curShots = 0;
                    curTimeBtwBurst = 0;
                }
                else
                {
                    if (curShots == 0)
                    {
                        // player position before start shooting
                        shotTarget = (Vector2) player.GetPosition();
                    }
                    if (currentTimeBtwShot > timeBtwShot)
                    {
                        ShotProjectile(shotPoint, shotTarget);
                        curShots++;
                        currentTimeBtwShot = 0;
                    }
                    else
                    {
                        currentTimeBtwShot += Time.deltaTime;
                    }
                }

            }
            else
            {
                curTimeBtwBurst += Time.deltaTime;
            }
        
            currentTime += Time.deltaTime;        
        }
        else
        {
            // Next Stage
            OnFinished(transform.position);
        }
    }


    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }

    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
    }
}
