using UnityEngine;

public class WeaverArandana : Arandaña
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

    protected override void ChasePlayer()
    {
        if (timeBtwShot <= 0)
        {
            animator.SetBool("Is Shooting", true);
            Instantiate(projectile, GetPosition(), Quaternion.identity);
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            animator.SetBool("Is Shooting", false);
            timeBtwShot -= Time.deltaTime;
        }
    }

    protected override void MainRoutine()
    {
        return;
    }

    protected override void Attack()
    {
        
    }

    public void ProjectileAtack()
    {
        player.TakeTirement(projectileDamage);

    }

}