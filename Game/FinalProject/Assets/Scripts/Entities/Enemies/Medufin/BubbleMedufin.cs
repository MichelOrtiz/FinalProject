using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMedufin : Medufin, IProjectile
{
    [SerializeField] private Transform shotProjectilePos;
     private Projectile projectile;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float startTimetwShot;
    [SerializeField] private float startTimeBtwShots;
    public bool projectileTouchingPlayer;
    private float timeBtwShot;
    private float timeBtwShots;
    
    new void Start()
    {
        base.Start();
    }

    
    new void Update()
    {
        if (projectileTouchingPlayer)
        {
            ProjectileAttack();
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
                //Instantiate(projectile, shotProjectilePos.position, Quaternion.identity);
                ShotProjectile(shotProjectilePos, player.GetPosition());
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

    /*public void ProjectileAttack()
    {
        throw new System.NotImplementedException();
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        throw new System.NotImplementedException();
    }*/

    public void ProjectileAttack()
    {
        projectile.Destroy();
        player.TakeTirement(projectile.damage);
        //player.Captured(5, 5, this);
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }
}
