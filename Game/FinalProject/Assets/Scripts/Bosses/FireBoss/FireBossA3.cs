using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossA3 : FireBossEnemy
{
    [SerializeField] private float speedMultiplier;
    private float speed;
    [SerializeField] private byte shotsUntilDeflect;
    private byte curShots;

    new void Start()
    {
        base.Start();
        speed = speedMultiplier * averageSpeed;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (TimeBeforeStart <= 0)
        {
            if (timeBtwShot > baseTimeBtwShot)
            {
                var proj = projectileShooter.ShootProjectile(projectileShooter.ShotPos.position + Vector3.down);
                
                proj.GetComponent<ProjectileDeflector>().enabled = curShots == shotsUntilDeflect;
                proj.GetComponent<BlinkingSprite>().enabled = curShots == shotsUntilDeflect;

                if (curShots < shotsUntilDeflect)
                {
                    curShots++;
                }
                else
                {
                    curShots = 0;
                }

                timeBtwShot = 0;
            }
            else// if(!projectilesShot)
            {
                timeBtwShot += Time.deltaTime;
            }
        }
        

    }

    
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (facingDirection == RIGHT)
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), GetPosition() + Vector3.right, speed * Time.deltaTime);
        }
        else
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), GetPosition() + Vector3.left, speed * Time.deltaTime);
        }
    }


    
}
