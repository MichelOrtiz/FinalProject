using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMedufin : Enemy, IProjectile
{
    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;

    [SerializeField] private Transform shotPoint;
    private Vector2 shotTarget;
    
    [SerializeField] private float timeBtwShot;
    private float currentTimeBtwShot;

    /*[SerializeField] private float burstTime;
    private float curBurstTime;*/

    [SerializeField] private byte shotsPerBurst;
    private byte curShots;

    [SerializeField] private float timeBtwBurst;
    private float curTimeBtwBurst;

    #endregion
    
    new void Start()
    {
        base.Start();
    }

    
    new void Update()
    {
        if (!CanSeePlayer())
        {
            //lastSeenPlayerPos = this.GetPosition();
            //shotsFired = 0;
           // timeBtwShots = 0;
        }
        if (curShots > 0 && curShots < shotsPerBurst)
        {
            ChasePlayer();
        }
        base.Update();
    }

    new protected void FixedUpdate()
    {
        Debug.Log("waaa");
        base.FixedUpdate();
    }

    protected override void MainRoutine()
    {
        return;
    }

    protected override void ChasePlayer()
    {
        if (curTimeBtwBurst > timeBtwBurst)
            {
                if (curShots > shotsPerBurst-1)
                {
                    flipToPlayerIfSpotted = true;
                    curShots = 0;
                    curTimeBtwBurst = 0;
                }
                else
                {
                    flipToPlayerIfSpotted = false;
                    if (curShots == 0)
                    {
                        // player position before start shooting
                        shotTarget = (Vector2) player.GetPosition();
                    }
                    if (currentTimeBtwShot > timeBtwShot)
                    {
                        ShotProjectile(shotPoint, shotTarget);
                        curShots++;
                        currentTimeBtwShot = 0;
                    }
                    else
                    {
                        currentTimeBtwShot += Time.deltaTime;
                    }
                }

            }
            else
            {
                curTimeBtwBurst += Time.deltaTime;
            }
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }

    public void ProjectileAttack()
    {
        //projectile.Destroy();
        player.TakeTirement(projectile.damage);
        //player.Captured(5, 5, this);
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
        //shotsFired++;
    }
}
