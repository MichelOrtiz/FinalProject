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
        if (timeBtwShot > baseTimeBtwShot)
        {
            ShotProjectiles();
            timeBtwShot = 0;
        }
        else
        {
            timeBtwShot += Time.deltaTime;
        }
    }


    private void ShotProjectiles()
    {
        float angle = 180;
        for (int i = 0; i < projectiles; i++)
        {
            Vector3 target = MathUtils.GetVectorFromAngle(angle);
            base.ShotProjectile(shotPoint, shotPoint.position + target);
            angle -= angleBtwProjectiles;
        }
    }
}
