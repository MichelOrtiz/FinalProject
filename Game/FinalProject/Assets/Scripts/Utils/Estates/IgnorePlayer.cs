using UnityEngine;
[CreateAssetMenu(fileName="New IgnorePlayer", menuName = "States/new IgnorePlayer")]
public class IgnorePlayer : State
{
    [SerializeField] private float speedModifier;
    private Enemy enemy;
    private float defaultSpeed;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        if (manager.hostEntity is Enemy)
        {
            enemy = (Enemy) manager.hostEntity;
            
            enemy.FieldOfView.blockFov = true;

            defaultSpeed = enemy.EnemyMovement.DefaultSpeed;
            if (speedModifier > 0)
            {
                enemy.EnemyMovement.DefaultSpeed *= speedModifier;
            }
        }
    }
    public override void Affect()
    {
        if (currentTime > duration)
        {
            StopAffect();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
    public override void StopAffect()
    {
        enemy.FieldOfView.blockFov = false;
        enemy.EnemyMovement.DefaultSpeed = defaultSpeed;
        base.StopAffect();
    }
}