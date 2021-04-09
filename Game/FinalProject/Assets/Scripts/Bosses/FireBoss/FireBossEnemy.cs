using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireBossEnemy : Entity, IProjectile
{
    private FireBoss fireBoss;
    private PlayerManager player;
    [SerializeField] protected float TimeBeforeStart;
   

    #region Projectile Stuff
    [Header("Projectile Stuff")]
    [SerializeField] protected Transform shotPoint;
    [SerializeField] protected float baseTimeBtwShot;
    protected float timeBtwShot;
    [SerializeField] protected GameObject projectilePrefab;
    protected Projectile projectile;
    #endregion

    #region Hit Handler
    [Header("Hit Handler")]
    private int maxHits;
    private int currentHits;
    // To change time before start each time the enemy gets hit
    [SerializeField] protected float timeChangeMultiplier;


    // To increase the damage each time the enemy gets hit
    [SerializeField] protected float damageChangeIncrease;
    #endregion
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        fireBoss = GetComponent<FireBoss>();
        player = PlayerManager.instance;

        if (fireBoss != null)
        {
            maxHits = fireBoss.maxHits;
        }

    }

    // Update is called once per frame
    protected new void Update()
    {

        if (TimeBeforeStart > 0)
        {
            TimeBeforeStart -= Time.deltaTime;
            return;
        }
        base.Update();
    }

    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
    }
    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            if (currentHits < maxHits)
            {
                currentHits++;
                TimeBeforeStart = TimeBeforeStart + TimeBeforeStart * timeChangeMultiplier;
                if (projectile != null)
                {
                    projectile.damage +=  damageChangeIncrease;
                }
            }
            else
            {
                fireBoss.EndBattle();
            }
        }
    }
}
