using UnityEngine;
public class AimerUnicorn : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float timeBtwShot;
    private float curTimeBtwShot;
    protected override void MainRoutine()
    {
        if (laserShooter.Laser == null)
        {
            enemyMovement.DefaultPatrol();
        }
    }

    protected override void ChasePlayer()
    {
        enemyMovement.StopMovement();
        if (curTimeBtwShot > timeBtwShot)
        {
            laserShooter.ShootLaserAndSetEndPos(player.transform);
            laserShooter.Laser.collidesWithObstacles = false;
            curTimeBtwShot = 0;
        }
        else
        {
            curTimeBtwShot += Time.deltaTime;
        }
    }
}