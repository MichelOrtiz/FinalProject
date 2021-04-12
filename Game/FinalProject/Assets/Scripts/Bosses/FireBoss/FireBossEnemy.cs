using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireBossEnemy : Entity, IProjectile
{
    private FireBoss fireBoss;
    private PlayerManager player;
    [SerializeField] protected float TimeBeforeStart;
    [SerializeField] private Transform collisionCheck;
    [SerializeField] private float baseCastDistance;
   

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

        if (InFrontOfObstacle())
        {
            ChangeFacingDirection();
        }
        base.Update();
    }

    protected bool InFrontOfObstacle()
    {

        float castDistance = facingDirection == LEFT ? -baseCastDistance : baseCastDistance;
        Vector3 targetPos = collisionCheck.position + (facingDirection == LEFT? Vector3.left : Vector3.right) * castDistance;
        return RayHitObstacle(collisionCheck.position, targetPos);
    }

    protected void ChangeFacingDirection()
    {
        transform.eulerAngles = new Vector3(0, facingDirection == LEFT? 0:180);
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
    protected void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.tag == "Projectile")
        {
            Debug.Log("enemy hit by projectile");
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
