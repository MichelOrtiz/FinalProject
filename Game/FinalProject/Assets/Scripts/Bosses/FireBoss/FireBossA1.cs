using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossA1 : FireBossEnemy
{
    [SerializeField] int projectiles;
    [SerializeField] float angleBtwProjectiles;
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (TimeBeforeStart <= 0)
        {
            if (timeBtwShot > baseTimeBtwShot)
            {
                ShotProjectiles();
                projectilesShot = true;
                timeBtwShot = 0;
            }
            else //if(!projectilesShot)
            {
                timeBtwShot += Time.deltaTime;
            }
        }
        

    }


    private void ShotProjectiles()
    {
        float angle = facingDirection == LEFT ? 180 : 0;
        for (int i = 0; i < projectiles; i++)
        {
            Vector3 target = MathUtils.GetVectorFromAngle(angle);
            base.ShotProjectile(shotPoint, shotPoint.position + target);
            
            if (facingDirection == LEFT)
            {
                angle -= angleBtwProjectiles;
            }
            else
            {
                angle += angleBtwProjectiles;
            }
        }
    }
}
