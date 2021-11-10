using UnityEngine;
using System;

public class NormalGolem : NormalType
{
    [Header("Self Additions")]
    [Header("Extra Fovs")]
    [SerializeField] private float secondFovDistance;
    [SerializeField] private float thirdFovDistance;
    private float initialChaseSpeed;
    [SerializeField] private float secondFovSpeed;
    [SerializeField] private float thirdFovSpeed;

    [Header("ProjectileStuff")]
    [SerializeField] private float timeBtwShot;
    private float curTimeBtwShot;
    [SerializeField] private float projectileKnockbackDuration;
    [SerializeField] private float projectileKnockbackForce;

    [SerializeField] private Transform firstShotPos;
    [SerializeField] private Transform secondShotPos;


    new void Start()
    {
        base.Start();
        initialChaseSpeed = enemyMovement.ChaseSpeed;
        secondFovSpeed *= averageSpeed;
        thirdFovSpeed *= averageSpeed;
        projectileShooter.ProjectileTouchedPlayerHandler += projectileShooter_ProjectileTouchedPlayer;
    }


    new void Update()
    {

        base.Update();
    }

     new protected void FixedUpdate()
    {
        if ( (fieldOfView.inFrontOfObstacle || groundChecker.isNearEdge) && !isFalling)
        {
            rigidbody2d.velocity = Vector3.zero;
            HandleStopAnimation();
        }
        base.EnemyFixedUpdate();
    }

    protected override void ChasePlayer()
    {
        if (!touchingPlayer)
        {
            if (!fieldOfView.inFrontOfObstacle)
            {
                enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: true);
            }
            
            if (!fieldOfView.inFrontOfObstacle && !groundChecker.isNearEdge && !animationManager.currentState.Contains("shoot"))
            {
                animationManager.ChangeAnimation("chase");
            }
        }

        //animationManager.ChangeAnimation("chase");
        float distance = Vector2.Distance(GetPosition(), player.GetPosition());
        
        if (distance <= secondFovDistance && distance > thirdFovDistance)
        {
            enemyMovement.ChaseSpeed = secondFovSpeed;

            if (curTimeBtwShot > timeBtwShot)
            {
                HandleShootAnimation();
                Invoke("HandleShootProjectile", 0.65f);
                curTimeBtwShot = 0;
            }
            else
            {
                curTimeBtwShot += Time.deltaTime;
            }
        }
        else if (distance <= thirdFovSpeed)
        {
            enemyMovement.ChaseSpeed = thirdFovSpeed;
            curTimeBtwShot = 0;
        }

    }

    void HandleStopAnimation()
    {
        if (!animationManager.currentState.Contains("idle_shoot"))
        {
            animationManager.ChangeAnimation(fieldOfView.canSeePlayer? "idle_chase" : "idle");
        }
    }

    void HandleShootAnimation()
    {
        if (fieldOfView.inFrontOfObstacle || groundChecker.isNearEdge)
        {
            animationManager.ChangeAnimation( projectileShooter.ShotPos == firstShotPos? "idle_shoot_1" : "idle_shoot_2" );
        }
        else
        {
            animationManager.ChangeAnimation( projectileShooter.ShotPos == firstShotPos? "chase_shoot_1" : "chase_shoot_2" );
        }
    }

    void HandleShootProjectile()
    {
        projectileShooter.ShootProjectile(player.GetPosition());
        projectileShooter.ShotPos = projectileShooter.ShotPos == firstShotPos? secondShotPos : firstShotPos;
        curTimeBtwShot = 0;
    }


    void projectileShooter_ProjectileTouchedPlayer()
    {
        Vector2 knockbackDir = (player.GetPosition() - GetPosition()).normalized;
        player.Knockback(projectileKnockbackDuration, projectileKnockbackForce, knockbackDir);
    }
}