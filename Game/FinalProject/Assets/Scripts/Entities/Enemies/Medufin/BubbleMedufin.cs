using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMedufin : Medufin
{
    [SerializeField] private Transform shotProjectilePos;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float startTimetwShot;
    [SerializeField] private float startTimeBtwShots;
    private float timeBtwShot;
    private float timeBtwShots;
    
    new void Start()
    {
        base.Start();
    }

    
    new void Update()
    {
        if (projectile.touchingPlayer)
        {
            ProjectileAtack();
        }
        base.Update();
    }

    protected override void MainRoutine()
    {
        return;
    }

    protected override void ChasePlayer()
    {
        //if (timeBtwShots <= 0)
        {
            if (timeBtwShot <= 0)
            {
                Instantiate(projectile, shotProjectilePos.position, Quaternion.identity);
                timeBtwShot = startTimetwShot;
            }
            else
            {
                timeBtwShot -= Time.deltaTime;
            }
            timeBtwShots = startTimeBtwShots;
        }
        /*else
        {
            timeBtwShots -= Time.deltaTime;
        }*/
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }

    private void ProjectileAtack()
    {
        player.TakeTirement(projectile.damage);
        player.Captured(5, 5, this);
    }
}
