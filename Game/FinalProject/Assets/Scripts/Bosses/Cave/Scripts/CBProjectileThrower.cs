using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBProjectileThrower : Entity, IProjectile
{
    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    //[SerializeField] private Transform shotPoint; 
    private Vector2 shotPoint;
    [SerializeField] private float timeBtwShot;
    private float currentTimeBtwShot;
    [SerializeField] private float angleBtwShots;
    [SerializeField] private Transform center;
    [SerializeField] private float radius;

    [SerializeField] private float timeToCompleteCircle;

    #endregion

    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if (currentTimeBtwShot > timeBtwShot)
        {
            ShotProjectiles();
            currentTimeBtwShot = 0;
        }
        else
        {
            currentTimeBtwShot += Time.deltaTime;
        }


        base.Update();
    }

    public void ProjectileAttack()
    {
        //throw new System.NotImplementedException();
    }

    public void ShotProjectiles()
    {
        float angle = 0;
        while (angle + angleBtwShots <= 360)
        {
            shotPoint = center.position + MathUtils.GetVectorFromAngle(angle);

            ShotProjectile(center.position, shotPoint );
            angle += angleBtwShots;
        }
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        
    }

    public void ShotProjectile(Vector2 from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to);
    }
}
