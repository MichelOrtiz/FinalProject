using UnityEngine;

public class HunterCentaur : Centaur
{
    

    [SerializeField] private Projectile projectile;
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
            Instantiate(projectile, GetPosition(), Quaternion.identity);
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
        player.Captured(10, damageAmount);
    }
}