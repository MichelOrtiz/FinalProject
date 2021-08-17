using UnityEngine;
[CreateAssetMenu(fileName="New LevitateComically", menuName = "States/new LevitateComically")]
public class LevitateComically : State
{
    [SerializeField] private Paralized paralized;
    [SerializeField] private float speed;

    private EnemyMovement enemyMovement;
    public override void StartAffect(StatesManager newManager)
    {
        //base.StartAffect(newManager);
        base.StartAffect(newManager);
        
        manager.hostEntity.rigidbody2d.gravityScale = 0;
        manager.hostEntity.rigidbody2d.isKinematic = true;
        manager.hostEntity.statesManager.AddState(paralized);

        manager.hostEntity.enabled = false;
        enemyMovement = manager.hostEntity.GetComponentInChildren<EnemyMovement>();

        
    }

    public override void Affect()
    {
        if (!manager.currentStates.Contains(paralized))
        {
            if (currentTime >= duration)
            {
                StopAffect();
            }
            else
            {
                manager.hostEntity.animator.SetBool("Is Walking", true);
                manager.hostEntity.enabled = false;

                manager.hostEntity.rigidbody2d.position = 
                    Vector2.MoveTowards(manager.hostEntity.GetPosition(), manager.hostEntity.GetPosition() + Vector3.up , Time.deltaTime);
                
                currentTime += Time.deltaTime;
            }
        }
    }

    public override void StopAffect()
    {
        //base.StopAffect();
        manager.hostEntity.DestroyEntity();
    }
}