using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMedufin : Medufin, IProjectile
{
    [SerializeField] private Transform shotProjectilePos;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shotsPerBurst;
    [SerializeField] private float startTimeBtwShot;
    [SerializeField] private float startTimeBtwShots;
    private Projectile projectile;
    public float shotsFired;
    public bool projectileTouchingPlayer;
    private float timeBtwShot;
    private Vector3 lastSeenPlayerPos;
    private float timeBtwShots;
    
    new void Start()
    {
        base.Start();
    }

    
    new void Update()
    {
        if (!CanSeePlayer())
        {
            lastSeenPlayerPos = this.GetPosition();
            shotsFired = 0;
            timeBtwShots = 0;
        }
        base.Update();
    }

    protected override void MainRoutine()
    {
        return;
    }

    protected override void ChasePlayer()
    {
        if (lastSeenPlayerPos == this.GetPosition())
        {
            lastSeenPlayerPos = player.GetPosition();
        }
        if (shotsFired < shotsPerBurst)
        {
            if (timeBtwShot <= 0)
            {
                ShotProjectile(shotProjectilePos, lastSeenPlayerPos);
                timeBtwShot = startTimeBtwShot;
            }
            else
            {
                timeBtwShot -= Time.deltaTime;
            }
            timeBtwShots = 0;
        }
        else
        {
            if (timeBtwShots < startTimeBtwShots)
            {
                timeBtwShots += Time.deltaTime;
            }
            else
            {
                lastSeenPlayerPos = player.GetPosition();
                shotsFired = 0;
            }
        }
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }

    public void ProjectileAttack()
    {
        //projectile.Destroy();
        player.TakeTirement(projectile.damage);
        //player.Captured(5, 5, this);
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
        shotsFired++;
    }
}
