using UnityEngine;
public abstract class UBBehaviour : Entity
{
    protected abstract void SetDefaults();

    public bool finishedAttack;
    public delegate void FinishedAttack();
    public event FinishedAttack FinishedAttackHandler;
    protected virtual void OnFinishedAttack()
    {
        Debug.Log("FinishedAttack");
        finishedAttack = true;
        FinishedAttackHandler?.Invoke();
    }

    public void FinishAttack()
    {
        OnFinishedAttack();
    }
    

    
    public virtual void SetActive(bool value)
    {
        finishedAttack = !value;
        
        

        Start();
        SetDefaults();

        gameObject.SetActive(value);
    }

    new protected virtual void Start()
    {
        base.Start();
    }

    new protected virtual void Update()
    {
        base.Update();
    }
}