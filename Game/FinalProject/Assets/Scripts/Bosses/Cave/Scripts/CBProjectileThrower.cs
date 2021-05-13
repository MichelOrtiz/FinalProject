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

    #region 
    [Header("Threads")]
    [SerializeField] private List<LineRenderer> threads;
    [SerializeField] private List<Vector2> startPositions;
    [SerializeField] private List<Vector2> endPositions;
    #endregion

    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        UpdateThreadPositions();
        //if (!finishedAttack)
        //{
            if (currentTimeBtwShot > timeBtwShot)
            {
                ShotProjectiles();
                currentTimeBtwShot = 0;
            }
            else
            {
                currentTimeBtwShot += Time.deltaTime;
            }
        //}
        


        base.Update();
    }

    public void ProjectileAttack()
    {
        
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

    void UpdateThreadPositions()
    {
        byte index = 0;
        foreach (var thread in threads)
        {
            thread.SetPosition(0, startPositions[index]);
            thread.SetPosition(1, endPositions[index]);
            index++;
        }
    }

    public void ShotProjectile(Vector2 from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }

    /*protected override void SetDefaults()
    {
        //throw new System.NotImplementedException();
    }*/
}
