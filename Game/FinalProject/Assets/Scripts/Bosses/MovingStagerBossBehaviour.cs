using UnityEngine;
public abstract class MovingStagerBehaviour : Entity
{

    #region FinishAttackStuff
    public bool finishedAttack;
    public delegate void FinishedAttack();
    public event FinishedAttack FinishedAttackHandler;
    protected virtual void OnFinishedAttack()
    {
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
    protected abstract void SetDefaults();
    #endregion
    

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {

    }

    new protected virtual void Start()
    {
        base.Start();
       // player = PlayerManager.instance;
    }


    new protected virtual void Update()
    {
        /*if (player.GetPosition().x > GetPosition().x && facingDirection == LEFT
            || player.GetPosition().x < GetPosition().x && facingDirection == RIGHT)
            {
                ChangeFacingDirection();
            }*/ 

        base.Update();
    }

}