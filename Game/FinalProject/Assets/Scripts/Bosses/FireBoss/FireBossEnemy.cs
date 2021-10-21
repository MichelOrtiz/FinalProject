using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireBossEnemy : Entity
{
    [Header("Self Additions")]
    [SerializeField] private LayerMask obstacles;
    protected FireBoss fireBoss;
    private PlayerManager player;
    [SerializeField] protected float TimeBeforeStart;
    [SerializeField] private Transform collisionCheck;
    [SerializeField] private float baseCastDistance;
   

    #region Projectile Stuff
    [Header("Projectile Stuff")]
    [SerializeField] protected ProjectileShooter projectileShooter;
    [SerializeField] protected float baseTimeBtwShot;
    protected float timeBtwShot;

    #endregion

    #region Hit Handler
    [Header("Hit Handler")]
    private int currentHits;
    // To change time before start each time the enemy gets hit
    [SerializeField] protected float timeChangeMultiplier;


    // To increase the damage each time the enemy gets hit
    [SerializeField] protected float damageChangeIncrease;
    [SerializeField] protected float speedChangeIncrease;
    #endregion
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        player = PlayerManager.instance;

        fireBoss = FindObjectOfType<FireBoss>();
        UpdateValues();

    }

    // Update is called once per frame
    protected new void Update()
    {

        if (TimeBeforeStart > 0)
        {
            TimeBeforeStart -= Time.deltaTime;
            return;
        }

        if (InFrontOfObstacle())
        {
            ChangeFacingDirection();
        }
        base.Update();
    }

    protected bool InFrontOfObstacle()
    {
        return FieldOfView.RayHitObstacle(collisionCheck.position, collisionCheck.right * baseCastDistance, obstacles);
    }

    void UpdateValues()
    {
        TimeBeforeStart *= fireBoss.curTimeBfStartMod;
        baseTimeBtwShot *= fireBoss.curTimeBtwShotMod;
    }
    
    protected override void collisionHandler_EnterContact(GameObject contact)
    {
        if (contact.tag == "Projectile")
        {
            if (contact.TryGetComponent<ProjectileDeflector>(out var deflector))
            {
                if (deflector.Deflected)
                {
                    fireBoss.AddHit();
                    TimeBeforeStart = TimeBeforeStart + TimeBeforeStart * timeChangeMultiplier;
                    UpdateValues();
                    Destroy(contact);
                }
            }
        }
    }
}
