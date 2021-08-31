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
        enemyMovement.DefaultPatrol();
    }

    new void FixedUpdate()
    {
        if (groundChecker.lastGroundTag == "Platform")
        {
            if ( (!fieldOfView.canSeePlayer && (fieldOfView.inFrontOfObstacle || groundChecker.isNearEdge))
                || (fieldOfView.canSeePlayer && player.GetPosition().y > GetPosition().y) )
            {
                enemyMovement.Jump();
            }
        }
        base.FixedUpdate();
    }
    protected override void ChasePlayer()
    {
        float newSpeed = 1 / (MathUtils.GetAbsXDistance(player.GetPosition(), GetPosition())) * speedMultiplier * averageSpeed;
        if (newSpeed <= speedLimit)
        {
            speed = newSpeed;
            enemyMovement.SetChaseSpeed(speed);
        }
        if (MathUtils.GetAbsXDistance(player.GetPosition(), GetPosition()) > 1f)
        {
            enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: true);
        }
    }

    protected override void Attack()
    {
        base.Attack();
        statesManager.AddState(touchedPlayerEffect);
    }
}