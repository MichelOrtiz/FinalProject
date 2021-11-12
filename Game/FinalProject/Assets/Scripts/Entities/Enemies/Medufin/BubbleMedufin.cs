using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMedufin : Enemy
{
    #region ProjectileStuff
    [Header("Projectile Stuff")]
    private Vector2 shotTarget;
    [SerializeField] private float timeBtwShot;
    private float currentTimeBtwShot;
    [SerializeField] private byte shotsPerBurst;
    private byte curShots;
    [SerializeField] private float timeBtwBurst;
    private float curTimeBtwBurst;

    #endregion
    
    new void Update()
    {
        if (!fieldOfView.canSeePlayer)
        {
            //lastSeenPlayerPos = this.GetPosition();
            //shotsFired = 0;
           // timeBtwShots = 0;
        }
        if (curShots > 0 && curShots < shotsPerBurst)
        {
            ChasePlayer();
        }
        base.Update();
    }

    protected override void MainRoutine()
    {
        return;
    }

    protected override void ChasePlayer()
    {
        if (curTimeBtwBurst > timeBtwBurst)
            {
                if (curShots > shotsPerBurst-1)
                {
                    flipToPlayerIfSpotted = true;
                    curShots = 0;
                    curTimeBtwBurst = 0;
                }
                else
                {
                    flipToPlayerIfSpotted = false;
                    if (curShots == 0)
                    {
                        // player position before start shooting
                        //shotTarget = (Vector2) player.GetPosition();
                    }
                    if (currentTimeBtwShot > timeBtwShot)
                    {
                        projectileShooter.ShootProjectile(player.GetPosition());
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
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }
}
