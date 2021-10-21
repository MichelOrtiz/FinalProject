using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossA2 : FireBossEnemy
{
    
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
                //base.ShotProjectile(shotPoint, );
                var proj = projectileShooter.ShootProjectile(projectileShooter.ShotPos.position + facingDirection == LEFT? Vector2.left : Vector2.right);
                
                timeBtwShot = 0;
            }
            else
            {
                timeBtwShot += Time.deltaTime;
            }
        }

    }


    
}
