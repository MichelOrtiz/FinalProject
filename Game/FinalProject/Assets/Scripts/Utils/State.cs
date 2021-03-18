using UnityEngine;

public abstract class State
{
    [SerializeField] private StateNames name;
    [SerializeField] private float duration;
    private Entity entity;

    public abstract void Affect();

    public void ActivateEffect(Entity entity)
    {

        entity.statusCheck += Affect;
    }

    public void DeactivateEffect(Entity entity)
    {
        entity.statusCheck -= Affect;
    }
    
}