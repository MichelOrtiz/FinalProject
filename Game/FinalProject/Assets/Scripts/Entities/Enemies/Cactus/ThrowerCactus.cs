using UnityEngine;
using System.Collections.Generic;
public class ThrowerCactus : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float startTimeBtwShot;
    private float timeBtwShot;
    [SerializeField] private GameObject allucination;
    [SerializeField] private List<Transform> allucinationsPos;
    [SerializeField] private byte minAllucinations;
    [SerializeField] private byte maxAllucinations;
    private bool allucinationsInstantiated;

    new void Start()
    {
        base.Start();
        projectileShooter.ProjectileTouchedPlayerHandler += projectileShooter_ProjectileTouchedPlayer;
    }

    protected override void ChasePlayer()
    {
        if (timeBtwShot <= 0)
        {
            projectileShooter.ShootProjectile(player.GetPosition());
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }

    public void projectileShooter_ProjectileTouchedPlayer()
    {
        //player.TakeTirement(projectile.damage);
        // make screen dark for 0.5s
        if (!allucinationsInstantiated)
        {
            int numberOfAllucinations = RandomGenerator.NewRandom(minAllucinations, maxAllucinations);
            for (int i = 0; i < numberOfAllucinations; i++)
            {
                Instantiate(allucination, allucinationsPos[i].position, Quaternion.identity);
            }
            allucinationsInstantiated = true;
        }
    }

    protected override void MainRoutine()
    {
        return;
    }
}