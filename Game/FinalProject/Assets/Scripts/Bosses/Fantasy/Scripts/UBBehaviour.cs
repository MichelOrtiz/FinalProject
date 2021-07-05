using UnityEngine;
public abstract class UBBehaviour : Entity
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
    
    #region ParamsEtc
    protected PlayerManager player;
    [SerializeField] private float speedMultiplier;
    protected float speed;
    [SerializeField] protected float startDamageAmount;
    protected EnemyCollisionHandler eCollisionHandler;

    #endregion

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        eCollisionHandler = (EnemyCollisionHandler) base.collisionHandler;
        eCollisionHandler.TouchingPlayerHandler += eCollisionHandler_TouchingPlayer;
        
        speed = averageSpeed * speedMultiplier;
    }

    new protected virtual void Start()
    {
        base.Start();
        player = PlayerManager.instance;
    }


    new protected virtual void Update()
    {
        if (player.GetPosition().x > GetPosition().x && facingDirection == LEFT
            || player.GetPosition().x < GetPosition().x && facingDirection == RIGHT)
            {
                ChangeFacingDirection();
            } 

        base.Update();
    }

    protected virtual void eCollisionHandler_TouchingPlayer()
    {
        player.TakeTirement(startDamageAmount);
    }
}