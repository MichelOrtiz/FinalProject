using UnityEngine;

public class HunterCentaur : Enemy
{
    [SerializeField] private float timeBtwFlip;
    private float curTimeBtwFlip;
    [SerializeField] private float startTimeBtwShot;
    private float timeBtwShot;

    protected override void MainRoutine()
    {
        if (rigidbody2d.velocity.magnitude != 0 && !fieldOfView.inFrontOfObstacle)
        {
            enemyMovement.StopMovement();
        }
        if (curTimeBtwFlip > timeBtwFlip)
        {
            animationManager.ChangeAnimation("search");
            animationManager.SetNextAnimation("idle");
            ChangeFacingDirection();
            curTimeBtwFlip = 0;
        }
        else
        {
            curTimeBtwFlip += Time.deltaTime;
        }
    }

    protected override void ChasePlayer()
    {

        if (timeBtwShot <= 0)
        {
            projectileShooter.ShootProjectile(player.GetPosition());
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }

        if (fieldOfView.inFrontOfObstacle)
        {
            enemyMovement.Jump();
        }
        else
        {
            enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: false);
        }
    }
}