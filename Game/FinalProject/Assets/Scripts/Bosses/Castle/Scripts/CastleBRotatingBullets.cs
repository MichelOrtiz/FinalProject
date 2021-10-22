using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBRotatingBullets : MonoBehaviour, IProjectile, IBossFinishedBehaviour
{
    #region TotalTime
    [Header("Total Time")]
    [SerializeField] private float totalTime;
    private float currentTime;
    #endregion

    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private ProjectileShooter projectileShooter;
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    //[SerializeField] private Transform shotPoint; 
    private Vector2 shotPoint;
    [SerializeField] private float timeBtwShot;
    private float currentTimeBtwShot;

    [SerializeField] private float burstTime;
    private float curBurstTime;

    [SerializeField] private float timeBtwBurst;
    private float curTimeBtwBurst;

    [SerializeField] private float angleBtwShots;
    [SerializeField] private Transform center;
    private  float centerAngle;
    [SerializeField] private float radius;

    [SerializeField] private float timeToCompleteCircle;
    
    private float rotationSpeed;
    private bool isClockwise;
    #endregion

    private PlayerManager player;


    public event IBossFinishedBehaviour.Finished FinishedHandler;
    public void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }


    void Start()
    {
       rotationSpeed = (2*Mathf.PI)/timeToCompleteCircle;
        player = PlayerManager.instance;

        InvokeRepeating("ShootProjectiles", timeBtwShot, timeBtwShot);
        InvokeRepeating("ChangeProjectilesRotation", burstTime, burstTime + timeBtwBurst);

    }

    void ChangeProjectilesRotation()
    {
        projectileShooter.ChangeRotation();
    }

    void ShootProjectiles()
    {
        if (curTimeBtwBurst > timeBtwBurst) projectileShooter.ShootRotating();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= totalTime)
        {
            //RotateCenter();
            if (curTimeBtwBurst > timeBtwBurst)
            {
                if (curBurstTime > burstTime)
                {
                    curBurstTime = 0;
                    curTimeBtwBurst = 0;

                    // Invert the direction
                    isClockwise = !isClockwise;
                }
                else
                {
                    if (currentTimeBtwShot > timeBtwShot)
                    {
                        ShotProjectiles();
                        currentTimeBtwShot = 0;
                    }
                    else
                    {
                        currentTimeBtwShot += Time.deltaTime;
                    }
                    curBurstTime += Time.deltaTime;
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
            // Next stage
            OnFinished(transform.position);
        }
            
        //}
        


    }

    void RotateCenter()
    {
        if (isClockwise)
        {
            centerAngle += rotationSpeed * Time.deltaTime;
        }
        else
        {
            centerAngle -= rotationSpeed * Time.deltaTime;
        }
    }


    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
    }

    public void ShotProjectiles()
    {
        float angle = 0;
        while (angle + angleBtwShots <= 360)
        {
            shotPoint = center.position + MathUtils.GetVectorFromAngle(angle + centerAngle);

            ShotProjectile(center, shotPoint);
            angle += angleBtwShots;
        }
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }
}
