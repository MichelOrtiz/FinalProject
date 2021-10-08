using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerProjectile : Projectile
{
    //[SerializeField] private float lifeTime;

    PlayerManager player;
    private new Transform target;

    public void Setup(Transform startPoint, Transform target)
    {
        this.startPoint = startPoint.position;
        this.target = target;
    }

    public void Setup(Transform startPoint, Transform target, IProjectile enemy)
    {
        this.startPoint = startPoint.position;
        this.target = target;
        this.enemy = enemy;
    }

    void Start()
    {
        player = PlayerManager.instance;

        defaultLayer = gameObject.layer;
        if (impactEffect != null)
        {
            impactEffect.SetActive(false);
        }
        speedMultiplier *= Entity.averageSpeed;


        /*if (targetWarningAvailable)
        {
            Instantiate(warning, target.position, Quaternion.identity);
        }*/
    }

    void Update()
    {
        /*if (lifeTime <= 0)
        {
            Destroy();
        }
        else
        {
            lifeTime -= Time.deltaTime;*/

            if (target != null && !touchingObstacle) transform.position = Vector2.MoveTowards(transform.position, target.position, speedMultiplier * Time.deltaTime);
        //}
        
        if (touchingObstacle)
        {
            rigidbody2d.Sleep();
            if (waitTime <= 0)
            {
                Destroy();
            }
            else
            {
                waitTime -= Time.deltaTime;
            } 
        }
        
        if (changeSizeByTime)
        {
            ChangeSizeByTime();
        }

        if (touchingPlayer)
        {
            if (enemy != null)
            {
                enemy.ProjectileAttack();
            }
        }
        
    }
    
}