using UnityEngine;

public abstract class State : ScriptableObject
{
    [SerializeField] protected StateNames name;
    [SerializeField] protected float duration;
    protected StatesManager manager;
    public bool onEffect;
    protected float currentTime;
    public abstract void Affect();
    public virtual void StartAffect(StatesManager newManager){
        manager=newManager;
        //Debug.Log(this.name.ToString()+" started in " + manager.hostEntity.name.ToString());
        onEffect=true;
        currentTime = 0;
        manager.statusCheck += Affect;
    }
    public virtual void StopAffect(){
        //Debug.Log(this.name.ToString()+" ended in "+ manager.hostEntity.name.ToString());
        onEffect=false;
        manager.statusCheck-=Affect;
        manager.RemoveState(this);
    }
    
}