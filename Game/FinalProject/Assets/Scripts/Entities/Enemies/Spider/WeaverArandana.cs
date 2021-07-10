using System.Collections;
using UnityEngine;

public class WeaverArandana : Enemy
{
    private Projectile projectile;
    [SerializeField] private float startTimeBtwShot;
    private float timeBtwShot;

    protected override void ChasePlayer()
    {
        if (timeBtwShot <= 0)
        {
            animator.SetBool("Is Shooting", true);
            projectileShooter.ShootProjectileAndSetDistance(player.GetPosition());
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


    /*public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
        projectile.MaxShotDistance = Vector2.Distance(from.position, to);
    }*/

}