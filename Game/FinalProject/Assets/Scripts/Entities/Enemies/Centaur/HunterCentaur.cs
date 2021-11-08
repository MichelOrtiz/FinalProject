using UnityEngine;

public class HunterCentaur : Enemy
{
    [SerializeField] private float timeBtwFlip;
    private float curTimeBtwFlip;
    [SerializeField] private float startTimeBtwShot;
    private float timeBtwShot;

    [SerializeField] private Paralized paralized;

    new void Awake()
    {
        base.Awake();
        timeBtwShot = startTimeBtwShot;
    }

    protected override void MainRoutine()
    {
        animationManager.ChangeAnimation("idle");
        timeBtwShot = startTimeBtwShot;

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
        if (groundChecker.isNearEdge)
        {
            if (animationManager.currentState != "HunterCentaur_throw") animationManager.ChangeAnimation("idle");
            if (timeBtwShot <= 0)
            {
                if (animationManager.currentState != "HunterCentaur_throw")
                {
                    Invoke("ShootProjectile", 1.8f);
                    animationManager.ChangeAnimation("throw");
                }
            }
            else
            {
                timeBtwShot -= Time.deltaTime;
            }
        }
        else if (animationManager.currentState != "HunterCentaur_throw")
        {
            if (fieldOfView.inFrontOfObstacle)
            {
                enemyMovement.Jump();
            }
            else
            {
                enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: true);
                animationManager.ChangeAnimation("walk");
            }
        }
    }

    void ShootProjectile()
    {
        projectileShooter.ShootProjectile(player.GetPosition());
        timeBtwShot = startTimeBtwShot + 1.5f;
    }

    protected override void Attack()
    {
        base.Attack();
        statesManager.AddState(paralized);
    }
}