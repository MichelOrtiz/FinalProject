using UnityEngine;

public class FlyingDragon : Dragon, IProjectile
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
        if (InFrontOfObstacle())
        {
            if (waitTime > 0)
            {
                isWalking = false;
                waitTime -= Time.deltaTime;
                return;
            }
            ChangeFacingDirection();
            waitTime = startWaitTime;
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * normalSpeed);
            isWalking = true;
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
    }

    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
        projectile.Destroy();
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }
}