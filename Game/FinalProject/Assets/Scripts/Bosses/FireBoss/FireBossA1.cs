using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossA1 : FireBossEnemy
{

    [SerializeField] private float timeUntilRotateBack;
    private float curTime;
    [SerializeField] private byte burstsUntilDeflect;
    private byte currentBursts;


    new void Start()
    {
        base.Start();


        Invoke("ChangeRotation", timeUntilRotateBack);
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
                timeBtwShot = 0;
            }
            else //if(!projectilesShot)
            {
                timeBtwShot += Time.deltaTime;
            }
        }
    }

    void ChangeRotation()
    {
        projectileShooter.ChangeRotation();
    }



    private void ShotProjectiles()
    {
        /*float angle = facingDirection == LEFT ? 180 : 0;
        for (int i = 0; i < projectiles; i++)
        {
            Vector3 target = MathUtils.GetVectorFromAngle(angle);


            var proj = projectileShooter.ShootRotating();

            proj.GetComponent<ProjectileDeflector>().enabled = currentBursts == burstsUntilDeflect;
            proj.GetComponent<BlinkingSprite>().enabled = currentBursts == burstsUntilDeflect;
            
            if (facingDirection == LEFT)
            {
                projectileShooter.currentAngle -= angleBtwProjectiles;
            }
            else
            {
                projectileShooter.currentAngle += angleBtwProjectiles;
            }
        }*/

        var projectiles = projectileShooter.ShootRotating();

        foreach (var proj in projectiles)
        {
            proj.GetComponent<ProjectileDeflector>().enabled = currentBursts == burstsUntilDeflect;
            proj.GetComponent<BlinkingSprite>().enabled = currentBursts == burstsUntilDeflect;
        }


        if (currentBursts < burstsUntilDeflect)
        {
            currentBursts++;
        }
        else
        {
            currentBursts = 0;
        }
    }
}
