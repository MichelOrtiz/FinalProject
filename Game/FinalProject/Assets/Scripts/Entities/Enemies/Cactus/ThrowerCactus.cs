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

    [Header("Projectiles")]
    [SerializeField] private Transform firstShotPos;
    [SerializeField] private Transform secondShotPos;

    new void Start()
    {
        base.Start();
        projectileShooter.ProjectileTouchedPlayerHandler += projectileShooter_ProjectileTouchedPlayer;
    }

    protected override void ChasePlayer()
    {
        if (timeBtwShot > startTimeBtwShot)
        {
            animationManager.ChangeAnimation(projectileShooter.ShotPos == firstShotPos ? "first_shot" : "second_shot");


            // Shoot projectile inm 0.3s, so the animation has time to prepar
            Invoke("HandleShootProjectile", 0.3f);
            timeBtwShot = 0;
        }
        else
        {
            if (animationManager.previousState == "ThrowerCactus_idle" || animationManager.previousState == "")
            {
                animationManager.ChangeAnimation("chase");
            }
            else
            {
                animationManager.SetNextAnimation("chase");
            }
            timeBtwShot += Time.deltaTime;
        }
    }

    void HandleShootProjectile()
    {
        projectileShooter.ShootProjectile(player.GetPosition());
        projectileShooter.ShotPos = projectileShooter.ShotPos == firstShotPos? secondShotPos : firstShotPos;
        timeBtwShot = 0;
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
                var obj = Instantiate(allucination.GetComponent<NormalType>(), allucinationsPos[i].position, allucination.transform.rotation);
            }
            allucinationsInstantiated = true;
        }
    }

    protected override void MainRoutine()
    {
        timeBtwShot = 0;
        animationManager.ChangeAnimation("idle");
    }
}