using UnityEngine;

public class FlyingDragon : Dragon
{
    [SerializeField] private float projectileDamage;
    [SerializeField] private GameObject projectile;
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
            Instantiate(projectile, this.GetPosition(), Quaternion.identity);
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }
}