using UnityEngine;
[CreateAssetMenu(fileName = "New KnockbackState", menuName = "States/New KnockbackState")]
public class KnockbackState : State
{
    [SerializeField] private float angle;
    [SerializeField] private float force;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
    


        manager.hostEntity.Knockback
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
                    ))*/
            );
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
}