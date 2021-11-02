using UnityEngine;
[CreateAssetMenu(fileName = "New KnockbackState", menuName = "States/New KnockbackState")]
public class KnockbackState : State
{
    [Range(0, 360)]
    [SerializeField] public float angle;
    [SerializeField] private float force;

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
            currentTime += Time.deltaTime;
        }
    }
}