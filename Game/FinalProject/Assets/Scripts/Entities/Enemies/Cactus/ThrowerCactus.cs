using UnityEngine;
using System.Collections.Generic;
public class ThrowerCactus : Cactus, IProjectile
{
    [SerializeField] private Transform shotPos;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float startTimeBtwShot;
    [SerializeField] private GameObject allucination;
    //[SerializeField] private float numberOfAllucinations;
    [SerializeField] private List<Transform> allucinationsPos;
    private int numberOfAllucinations;
    private bool allucinationsInstantiated;
    private Projectile projectile;
    private float timeBtwShot;
    new void Start()
    {
        base.Start();

    }
    new void Update()
    {
        base.Update();
    }

    /*protected override void MainRoutine()
    {
        return;
    }*/

    protected override void ChasePlayer()
    {
        if (timeBtwShot <= 0)
        {
            ShotProjectile(shotPos, player.GetPosition());
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    } 

    public void ProjectileAttack()
    {
        //player.TakeTirement(projectile.damage);
        // make screen dark for 0.5s
        if (!allucinationsInstantiated)
        {
            numberOfAllucinations = RandomGenerator.NewRandom(3, 8);
            for (int i = 0; i < numberOfAllucinations; i++)
            {
                Instantiate(allucination, allucinationsPos[i].position, Quaternion.identity);
            }
            allucinationsInstantiated = true;
        }
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }

}