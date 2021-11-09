using UnityEngine;
[CreateAssetMenu(fileName = "New KnockbackState", menuName = "States/New KnockbackState")]
public class KnockbackState : State
{
    [Range(0, 360)]
    [SerializeField] public float angle;
    [SerializeField] private float force;

    [Tooltip("If host entity is an enemy that needs to flip to the player")]
    [SerializeField] private bool flipToPlayer;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
    
        Vector2 direction;
        if (manager.hostEntity.facingDirection == "right")
        {
            direction = manager.hostEntity.transform.InverseTransformPoint
                (
                    manager.hostEntity.GetPosition() + 
                    (MathUtils.GetVectorFromAngle(angle)
                    ));
        }
        else
        {
            direction = -manager.hostEntity.transform.InverseTransformPoint
                (
                    manager.hostEntity.GetPosition() + 
                    (MathUtils.GetVectorFromAngle(angle + 180)
                    ));
        }

        manager.hostEntity.Knockback(duration, force, direction);
        /*manager.hostEntity.Knockback
            (
                duration, 
                force,
                
                //facingDirection == entity.facingDirection ?
                manager.hostEntity.transform.InverseTransformPoint
                (
                    manager.hostEntity.GetPosition() + 
                    (MathUtils.GetVectorFromAngle(angle)
                    ))
                    //:
                /*-entity.transform.InverseTransformPoint
                (
                    entity.GetPosition() + 
                    (MathUtils.GetVectorFromAngle(-enemyPushAngle)
                    ))
            );*/
    }

    public override void Affect()
    {
        if (currentTime > duration)
        {
            currentTime = 0;
            StopAffect();


        }
        else
        {
            HandleFlip();
            currentTime += Time.deltaTime;
        }
    }

    void HandleFlip()
    {
        if (manager.hostEntity is Enemy && flipToPlayer)
        {
            var enemy = manager.hostEntity as Enemy;
            if (enemy.isChasing)
            {
                if (MathUtils.GetAbsXDistance(enemy.GetPosition(), PlayerManager.instance.GetPosition()) > 1f)
                {
                    if ((enemy.GetPosition().x > PlayerManager.instance.GetPosition().x && enemy.facingDirection == "right")
                        || (enemy.GetPosition().x < PlayerManager.instance.GetPosition().x && enemy.facingDirection == "left"))
                    {
                        if (enemy.rigidbody2d?.gravityScale == 0 ||  enemy.groundChecker.isGrounded)
                        {
                            enemy.ChangeFacingDirection();
                        }
                    }
                }
            }
        }
    }
}