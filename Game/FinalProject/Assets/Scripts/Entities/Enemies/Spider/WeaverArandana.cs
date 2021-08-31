using System.Collections;
using UnityEngine;

public class WeaverArandana : Enemy
{
    private Projectile projectile;
    [SerializeField] private float startTimeBtwShot;
    private float timeBtwShot;
    [SerializeField] private float timeInShotAnim;

    new void Start()
    {
        base.Start();
        timeBtwShot = startTimeBtwShot;
    }

    protected override void ChasePlayer()
    {
        if (timeBtwShot <= 0)
        {
            //animator.SetBool("Is Shooting", true);
            animationManager.ChangeAnimation("shoot");
            Invoke("ChangeToWeave", timeInShotAnim);

            projectileShooter.ShootProjectileAndSetDistance(player.GetPosition());
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            if (animationManager.currentState != enemyName + "_" + "shoot")
            {
                animationManager.ChangeAnimation("weave");
            }
            
            timeBtwShot -= Time.deltaTime;
        }
    }

    protected override void MainRoutine()
    {
        animationManager.ChangeAnimation("idle");
    }

    void ChangeToWeave()
    {
        if (fieldOfView.canSeePlayer)
        {
            animationManager.ChangeAnimation("weave");
        }
    }

    /*new void FixedUpdate()
    {
        //if (animationManager.currentState != )
    }*/


    /*public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
        projectile.MaxShotDistance = Vector2.Distance(from.position, to);
    }*/

}