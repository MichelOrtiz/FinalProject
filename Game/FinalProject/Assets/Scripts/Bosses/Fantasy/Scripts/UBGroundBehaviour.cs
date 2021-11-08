using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UBGroundBehaviour : UBBehaviour
{
    #region TargetPosition
    [Header("Target Position")]
    [SerializeField] private Vector2 positionToGo;
    [SerializeField] private float destinationRadius;
    private bool reachedDestination;
    #endregion

    #region LaserBeam
    [Header("Laser Beam")]
    [SerializeField] private LaserShooter laserShooter;
    private bool shotLaser;

    #endregion
    


    new void Start()
    {
        base.Start();
        SetDefaults();
    }

    // Update is called once per frame
    new void Update()
    {
        if (!finishedAttack)
        {
            if (ReachedDestination())
            {
                if (!shotLaser)
                {
                    laserShooter.ShootLaserAndSetEndPos(player.transform);
                    shotLaser = true;
                }
                else if (laserShooter.Laser == null)
                {
                    OnFinishedAttack();
                }
            }
        }
        

        base.Update();
    }

    void FixedUpdate()
    {
        if (!ReachedDestination())
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), positionToGo, speed * Time.deltaTime);
        }
    }

    bool ReachedDestination()
    {
        return Vector2.Distance(GetPosition(), positionToGo) <= destinationRadius;
    }

    new void OnFinishedAttack()
    {
        shotLaser = false;
        base.OnFinishedAttack();
    }

    protected override void SetDefaults()
    {
        return;
    }
}
