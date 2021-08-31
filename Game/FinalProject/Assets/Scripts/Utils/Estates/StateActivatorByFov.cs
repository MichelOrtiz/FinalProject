
using UnityEngine;
[CreateAssetMenu(fileName = "New StateActivatorByFov", menuName = "States/new StateActivatorByFov")]
public class StateActivatorByFov : State
{
    [SerializeField] private State state;

    private Enemy enemy;

    private FieldOfView fieldOfView;
    private bool sawPlayer;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        if (manager.hostEntity is Enemy)
        {
            enemy = manager.hostEntity as Enemy;
             fieldOfView = enemy.FieldOfView;
        }
    }

    public override void Affect()
    {
        if (!sawPlayer)
        {
            sawPlayer = fieldOfView.canSeePlayer;
        }
        if (sawPlayer)
        {
            manager.AddState(state);
            StopAffect();
        }
    }
}