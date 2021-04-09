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

        if (timeBtwShot > baseTimeBtwShot)
        {
            base.ShotProjectile(shotPoint, Vector2.right);
            timeBtwShot = 0;
        }
        else
        {
            timeBtwShot += Time.deltaTime;
        }

    }


    
}
