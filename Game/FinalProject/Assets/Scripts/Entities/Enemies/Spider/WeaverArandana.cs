using System.Collections;
using UnityEngine;

public class WeaverArandana : Arandaña, IProjectile
{
    [SerializeField] private Transform shotProjectilePos;
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    [SerializeField] private float startTimeBtwShot;
    private float timeBtwShot;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    protected override void ChasePlayer()
    {
        if (timeBtwShot <= 0)
        {
            animator.SetBool("Is Shooting", true);
            ShotProjectile(shotProjectilePos, player.GetPosition());
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            animator.SetBool("Is Shooting", false);
            timeBtwShot -= Time.deltaTime;
        }
    }

    protected override void MainRoutine()
    {
        return;
    }

    protected override void Attack()
    {
        return;
    }


    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
        // player decrease speed to 0.6 for 2 seconds
    }


    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }

}