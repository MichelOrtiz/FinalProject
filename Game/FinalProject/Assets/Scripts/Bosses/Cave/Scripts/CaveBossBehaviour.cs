using UnityEngine;
public class CaveBossBehaviour : Entity
{

    #region OnPlayerEffects
    [Header("Effects on Player")]
    [SerializeField] private State effectOnPlayer;
    [SerializeField] private float damageAmount;
    [SerializeField] private float staminaPunish;



    #endregion
    [SerializeField] protected Color colorWhenHit;
    protected Color defaultColor;
    [SerializeField] protected float timeInColor;
    protected float curTimeInColor;
    protected bool inColor;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    private EnemyCollisionHandler eCollisionHandler;
    protected PlayerManager player;

    public delegate void Finished(Vector2 lastPosition);
    public event Finished FinishedHandler;
    protected virtual void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }


    new protected void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        
        defaultColor = spriteRenderer.color;
        base.Start();

        player = PlayerManager.instance;

        eCollisionHandler = (EnemyCollisionHandler)base.collisionHandler;
        eCollisionHandler.TouchedPlayerHandler += eCollisionHandler_Attack;
    }

    new protected void Update()
    {
        if (inColor)
        {
            if (curTimeInColor > timeInColor)
            {
                spriteRenderer.color = defaultColor;
                curTimeInColor = 0;
                inColor = false;
            }
            else
            {
                curTimeInColor += Time.deltaTime;
            }
        }
        base.Update();
    }


    void eCollisionHandler_Attack()
    {
        player.TakeTirement(damageAmount);
        player.currentStaminaLimit -= staminaPunish;
        player.SetImmune();
        //player.statesManager.AddState(effectOnPlayer);
    }
}