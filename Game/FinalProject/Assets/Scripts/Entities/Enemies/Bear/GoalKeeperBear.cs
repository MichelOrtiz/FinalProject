using UnityEngine;
public class GoalKeeperBear : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private State touchedPlayerEffect;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float speedLimit;
    [SerializeReference] private float speed;
    new void Start()
    {
        speedLimit *= averageSpeed;
        base.Start();
    }


    protected override void MainRoutine()
    {
        if (groundChecker.isGrounded)
        {
            enemyMovement.DefaultPatrol();
        }
    }

    protected override void ChasePlayer()
    {
        if (player.GetPosition().y > GetPosition().y)
        {
            animationManager.ChangeAnimation("jump");
            enemyMovement.Invoke("JumpByKnockback", 0.02f );
        }
        else
        {
            float newSpeed = 1 / (MathUtils.GetAbsXDistance(player.GetPosition(), GetPosition())) * speedMultiplier * averageSpeed;
            if (newSpeed <= speedLimit)
            {
                speed = newSpeed;
                enemyMovement.SetChaseSpeed(speed);
            }
            if (groundChecker.isGrounded)
            {
                if (MathUtils.GetAbsXDistance(player.GetPosition(), GetPosition()) > 1f)
                {
                    enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: false);
                }
                animationManager.ChangeAnimation("walk", enemyMovement.ChaseSpeed * 1 / enemyMovement.DefaultSpeed);
            }
        }

    }

    protected override void Attack()
    {
        base.Attack();
        statesManager.AddState(touchedPlayerEffect);
    }

    protected override void groundChecker_Grounded(string groundTag)
    {
        enemyMovement.StopMovement();
    }
}