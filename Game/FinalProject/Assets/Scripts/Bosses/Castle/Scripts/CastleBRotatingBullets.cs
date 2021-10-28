using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBRotatingBullets : MonoBehaviour, IBossFinishedBehaviour
{
    #region TotalTime
    [Header("Total Time")]
    [SerializeField] private float totalTime;
    #endregion

    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private ProjectileShooter projectileShooter;
    [SerializeField] private float timeBtwShot;

    [SerializeField] private float burstTime;
    private float curBurstTime;

    [SerializeField] private float timeBtwBurst;
    private float curTimeBtwBurst;
    #endregion

    private PlayerManager player;


    public event IBossFinishedBehaviour.Finished FinishedHandler;
    public void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }


    void Start()
    {
        player = PlayerManager.instance;

        InvokeRepeating("ShootProjectiles", timeBtwShot, timeBtwShot);
        InvokeRepeating("ChangeProjectilesRotation", burstTime, burstTime);
        Invoke("FinishBehaviour", totalTime);

    }

    void ChangeProjectilesRotation()
    {
        projectileShooter.ChangeRotation();
    }

    void ShootProjectiles()
    {
        if (curBurstTime <= burstTime)
        {
            projectileShooter.ShootRotating();
        }
    }

    void Update()
    {
        if (curBurstTime > burstTime)
        {
            if (curTimeBtwBurst > timeBtwBurst)
            {
                curBurstTime = 0;
                curTimeBtwBurst = 0;
                CancelInvoke("ShootProjectiles");
                InvokeRepeating("ShootProjectiles", 0, timeBtwShot); 
            }
            else
            {
                curTimeBtwBurst += Time.deltaTime;
            }
        }
        else
        {
            curBurstTime += Time.deltaTime;
        }
    }

    void FinishBehaviour()
    {
        OnFinished(transform.position);
    }
    
}
