using UnityEngine;
using System;
public class AimingUnicorn : Enemy
{
    [Header("Self Additions")]

    [Header("Time")]
    [SerializeField] private float walkingTime;
    private float curWalkingTime;
    private float curWaitTime;
    [SerializeField] private float timeBtwShot;
    private float curTimeBtwShot;

    [Header("Aimer Unicorn")]
    [SerializeField] private GameObject aimerUnicorn;
    [SerializeField] private float newFovDistance;

    private Vector2 spawnedPos;
    bool laserShot;
    bool laserTouchedPlayer;

    new void Start()
    {
        base.Start();
        laserShooter.OnLaserAttack += laserShooter_OnLaserAttack;

        ProbabilitySpawner spawner = FindObjectOfType<ProbabilitySpawner>();
        if (spawner != null)
        {
            var instantiated = spawner.SpawnedObjects.Find(s => s.gameObject == gameObject); 

            if ( instantiated != null )
            {
                spawnedPos = instantiated.spawnedPos;
            }
        }
    }

    new void Update()
    {
        if (laserShot && laserShooter.Laser == null && !laserTouchedPlayer)
        {
            Instantiate(aimerUnicorn, spawnedPos, aimerUnicorn.transform.rotation);
            fieldOfView.ViewDistance = newFovDistance;
            
            // So it doesn't instantiate more than once
            laserTouchedPlayer = true;
        }
        base.Update();
    }

    protected override void MainRoutine()
    {
        if (laserShooter.Laser == null)
        {
            if (curWalkingTime > walkingTime)
            {
                enemyMovement.StopMovement();
                if (curWaitTime < enemyMovement.WaitTime)
                {
                    curWaitTime += Time.deltaTime;
                    return;
                }
                curWaitTime = 0;
                curWalkingTime = 0;
            }
            else
            {
                curWalkingTime += Time.deltaTime;
                enemyMovement.DefaultPatrol();
            }
        }
    }

    protected override void ChasePlayer()
    {
        if (curTimeBtwShot > timeBtwShot)
        {
            laserShooter.ShootLaserAndSetEndPos(player.transform);
            if (!laserShot) laserShot = true;
            curTimeBtwShot = 0;
        }
        else
        {
            curTimeBtwShot += Time.deltaTime;
        }
    }

    private void laserShooter_OnLaserAttack()
    {
        laserTouchedPlayer = true;
    }
}