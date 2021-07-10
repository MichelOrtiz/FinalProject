using UnityEngine;

public class FlyingDragon : Enemy
{
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

    protected override void MainRoutine()
    {
        enemyMovement.DefaultPatrolInAir();
    }

    protected override void ChasePlayer()
    {
        enemyMovement.DefaultPatrolInAir();
        if (timeBtwShot <= 0)
        {
            projectileShooter.ShootProjectile(player.GetPosition());
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }
}