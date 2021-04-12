using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossA3 : FireBossEnemy
{
    
    [SerializeField] private float speedMultiplier;
    private float speed;

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
                base.ShotProjectile(shotPoint, shotPoint.position + Vector3.down);
                timeBtwShot = 0;
            }
            else
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
