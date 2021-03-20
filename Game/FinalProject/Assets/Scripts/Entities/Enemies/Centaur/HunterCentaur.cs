using UnityEngine;

public class HunterCentaur : Centaur, IProjectile
{
    [SerializeField] private Transform shotProjectilePos;
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    [SerializeField] private float startTimeBtwShot;
    private float timeBtwShot;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    protected override void MainRoutine()
    {
        if (waitTime <= 0)
        {
            ChangeFacingDirection();
            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    protected override void ChasePlayer()
    {
        if (timeBtwShot <= 0)
        {
            ShotProjectile(shotProjectilePos, player.GetPosition());
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
        
        rigidbody2d.position = Vector3.MoveTowards(GetPosition(), player.GetPosition(), chaseSpeed * Time.deltaTime);
    }

    protected override void Attack()
    {
        Debug.Log("CENTAURO ATACANDO");
        player.Captured(10, damageAmount,this);
    }

    public void ProjectileAttack()
    {
        // player paralized 1s
        // player captured 10 damage
        projectile.Destroy();
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }
}