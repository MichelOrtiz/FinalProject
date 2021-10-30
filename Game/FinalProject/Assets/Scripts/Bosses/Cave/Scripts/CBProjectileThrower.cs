using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBProjectileThrower : Entity
{
    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private ProjectileShooter projectileShooter;
    [SerializeField] private float timeBtwShot;

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
        InvokeRepeating("ShootProjectiles", timeBtwShot, timeBtwShot);
    }

    // Update is called once per frame
    new void Update()
    {
        UpdateThreadPositions();
        base.Update();
    }
    public void ShootProjectiles()
    {
        projectileShooter.ShootRotating();
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
}
