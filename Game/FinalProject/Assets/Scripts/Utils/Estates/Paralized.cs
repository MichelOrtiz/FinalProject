using UnityEngine;

public class Paralized : State
{
    
    Vector3 paralizatedPos;
    public override void ActivateEffect(Entity newEntity)
    {
        duration=5f;
        base.ActivateEffect(newEntity);
        paralizatedPos = entity.GetPosition();
    }
    public override void Affect()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= duration){
            RemoveEffect(entity);
        }
        entity.gameObject.transform.position = paralizatedPos;
    }
}