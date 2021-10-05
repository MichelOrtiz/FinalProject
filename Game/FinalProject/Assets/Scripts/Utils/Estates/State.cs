using UnityEngine;
using System;
public abstract class State : ScriptableObject
{
    public string ObjectName { get => base.name; }
    private string objectName;
    [SerializeField] new public StateNames name;
    public StateNames Name { get => this.name; }
    public float duration;
    protected StatesManager manager;
    public bool onEffect = false;
    protected float currentTime;

    public Action StoppedAffect;

    public EmoteSetter emoteSetter;
    private EmoteSetter emoteInstance;

    public abstract void Affect();
    public virtual void StartAffect(StatesManager newManager){
        objectName = ObjectName;
        manager=newManager;
        //Debug.Log(this.name.ToString()+" started in " + manager.hostEntity.name.ToString());
        onEffect=true;
        currentTime = 0;
        manager.statusCheck += Affect;

        if (emoteSetter != null)
        {
            emoteSetter.duration = duration;
            emoteInstance = (EmoteSetter) manager.AddState(emoteSetter);
        }
    }
    public virtual void StopAffect(){
        //Debug.Log(this.name.ToString()+" ended in "+ manager.hostEntity.name.ToString());
        onEffect=false;
        manager.statusCheck-=Affect;
        emoteInstance?.StopAffect();
        manager.RemoveState(this);
        StoppedAffect?.Invoke();
    }
    
  
    void OnDestroy()
    {

        onEffect = false;
    }
}