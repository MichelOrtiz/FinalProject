using UnityEngine;
[CreateAssetMenu(fileName="New LevitateComically", menuName = "States/new LevitateComically")]
public class LevitateComically : State
{
    [SerializeField] private float speed;
    private EnemyMovement enemyMovement;

    public override void StartAffect(StatesManager newManager)
    {
        //base.StartAffect(newManager);
        base.StartAffect(newManager);
        
        manager.hostEntity.rigidbody2d.gravityScale = 0;
        manager.hostEntity.rigidbody2d.isKinematic = true;

        manager.hostEntity.enabled = false;
        enemyMovement = manager.hostEntity.GetComponentInChildren<EnemyMovement>();

        
    }

    public override void Affect()
    {
        if (currentTime >= duration)
        {
            currentTime = 0;
            StopAffect();
        }
        else
        {
            // Animation
            //manager.hostEntity.animator.SetBool("Is Walking", true);
            manager.hostEntity.animationManager.ChangeAnimation("walk", 2f);
            //manager.hostEntity.enabled = false;

            /*manager.hostEntity.rigidbody2d.position = 
                Vector2.MoveTowards(manager.hostEntity.GetPosition(), manager.hostEntity.GetPosition() + Vector3.up , Time.deltaTime);*/

            enemyMovement.GoTo(manager.hostEntity.GetPosition() + Vector3.up, speed, gravity: false);
            
            currentTime += Time.deltaTime;
        }
    }

    public override void StopAffect()
    {

        manager.hostEntity.rigidbody2d.isKinematic = false;
        manager.hostEntity.rigidbody2d.gravityScale = 1;
        manager.hostEntity.enabled = true;

        base.StopAffect();
        //manager.hostEntity.DestroyEntity();
    }
}