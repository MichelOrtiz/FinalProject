using UnityEngine;

public abstract class State 
{
    [SerializeField] protected StateNames name;
    [SerializeField] protected float duration;
    protected float currentTime;
    protected Entity entity;

    public abstract void Affect();

    public virtual void ActivateEffect(Entity newEntity)
    {
        Debug.Log(entity.name + " esta "+ name.ToString());
        entity = newEntity;
        entity.currentState = name;
        entity.statusCheck += Affect;
    }

    public void RemoveEffect(Entity newEntity)
    {
        Debug.Log(entity.name + " ya no esta "+ name.ToString());
        entity.statusCheck -= Affect;
    }
    
}