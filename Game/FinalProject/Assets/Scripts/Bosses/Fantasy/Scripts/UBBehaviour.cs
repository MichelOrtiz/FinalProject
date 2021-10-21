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
    [SerializeField] protected float staminaPunish;
    protected EnemyCollisionHandler eCollisionHandler;

    #endregion


    new protected void Awake()
    {
        base.Awake();
        eCollisionHandler = (EnemyCollisionHandler) base.collisionHandler;
        eCollisionHandler.TouchedPlayerHandler += eCollisionHandler_Attack;
        
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

    protected virtual void eCollisionHandler_Attack()
    {
        player.TakeTirement(startDamageAmount);
        player.currentStaminaLimit -= staminaPunish;
        player.SetImmune();
    }
}