using UnityEngine;
using System.Collections;
public class CryingGhost : Enemy
{
    [SerializeField] private float startTimeBtwShot;
    private float timeBtwShot;

    protected override void MainRoutine()
    {
        return;
    }
    
    protected override void ChasePlayer()
    {
        if (MathUtils.GetAbsXDistance(player.GetPosition(), GetPosition()) > 0.5f)
        {
            enemyMovement.GoTo(MathUtils.GetXDirection(GetPosition(), player.GetPosition()), chasing: false, gravity: false);
        }

        Vector2 shotPos = projectileShooter.ShotPos.position;
        RaycastHit2D hit = Physics2D.Linecast(shotPos, shotPos + Vector2.down * fieldOfView.ViewDistance, 1 << LayerMask.NameToLayer("Ground"));
        if (timeBtwShot <= 0)
        {
            projectileShooter.ShootProjectile(hit.point);
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }
}