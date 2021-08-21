using UnityEngine;

[CreateAssetMenu(fileName = "New ChaseSpeedModifier", menuName = "States/New ChaseSpeedModifier")]
public class ChaseSpeedModifier : State
{
    [SerializeField] private float speedMultiplier;
    
    // To be able to stop from duration as well
    [SerializeField] private bool hasDuration;
    
    private float defSpeed;
    private Enemy enemy;
    private FieldOfView fieldOfView;
    private bool sawPlayer;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        if (manager.hostEntity is Enemy)
        {
            enemy = (Enemy) manager.hostEntity;
            fieldOfView = enemy.FieldOfView;
            defSpeed = enemy.EnemyMovement.ChaseSpeed;
            enemy.EnemyMovement.ChaseSpeed *= speedMultiplier;
        }
    }

    public override void Affect()
    {
        // Only starts affecting once the player has been spotted
        if (!sawPlayer)
        {
            sawPlayer = fieldOfView.canSeePlayer;
        }
        if (sawPlayer)
        {
            if ((hasDuration && currentTime > duration) || !fieldOfView.canSeePlayer)
            {
                currentTime = 0;
                StopAffect();
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    public override void StopAffect()
    {
        enemy.EnemyMovement.ChaseSpeed /= speedMultiplier;
        base.StopAffect();
    }
}