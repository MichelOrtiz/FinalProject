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


    new void Start()
    {
        base.Start();
        initialChaseSpeed = enemyMovement.ChaseSpeed;
        secondFovSpeed *= averageSpeed;
        thirdFovSpeed *= averageSpeed;
        projectileShooter.ProjectileTouchedPlayerHandler += projectileShooter_ProjectileTouchedPlayer;
    }

    protected override void ChasePlayer()
    {
        base.ChasePlayer();
        float distance = Vector2.Distance(GetPosition(), player.GetPosition());
        
        if (distance <= secondFovDistance && distance > thirdFovDistance)
        {
            enemyMovement.ChaseSpeed = secondFovSpeed;

            if (curTimeBtwShot > timeBtwShot)
            {
                projectileShooter.ShootProjectile(player.GetPosition());
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
        }

    }


    void projectileShooter_ProjectileTouchedPlayer()
    {
        Vector2 knockbackDir = (player.GetPosition() - GetPosition()).normalized;
        player.Knockback(projectileKnockbackDuration, projectileKnockbackForce, knockbackDir);
    }
}