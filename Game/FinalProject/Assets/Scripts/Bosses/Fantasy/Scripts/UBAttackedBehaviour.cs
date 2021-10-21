using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UBAttackedBehaviour : UBBehaviour
{
    #region TargetPosition
    [Header("Target Position")]
    [SerializeField] private Vector2 positionToGo;
    [SerializeField] private float destinationRadius;
    #endregion
    

    #region Tears
    [Header("Tears")]
    [SerializeField] private int minTears;
    [SerializeField] private int maxTears;
    [SerializeField] private float rainTime;
    private float currentRainTime;
    [SerializeField] private float intervalBtwDrops;
    private float currentTimeBtwDrops;
    private int nTears;
    private int index;
    #endregion
    
    #region Projectiles
    [Header("Projectiles")]
    [SerializeField] private ProjectileShooter projectileShooter;
    [SerializeField] private float timeBtwShot;
    [SerializeField] private float timeTilChangeRot;
    #endregion

    [Header("Modifiers")]
    [SerializeField] private float timeBtwShotMod;
    [SerializeField] private float dropsIntervalMod;

    public void ModValues()
    {
        timeBtwShot *= timeBtwShotMod;
        intervalBtwDrops*= dropsIntervalMod;
    }


    void OnEnable()
    {
        InvokeRepeating("ShootProjectiles", 2f, timeBtwShot);
        InvokeRepeating("ChangeProjectilesRotation", 2f, timeTilChangeRot);
    }


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
                if (currentRainTime < rainTime)
                {
                    if (currentTimeBtwDrops > intervalBtwDrops)
                    {
                        index = minTears;
                        nTears = RandomGenerator.NewRandom(minTears, maxTears);
                        while (index++ <= nTears)
                        {
                            DropTear();
                        }
                        currentTimeBtwDrops = 0;
                    }
                    else
                    {
                        currentTimeBtwDrops += Time.deltaTime;
                    }
                    currentRainTime += Time.deltaTime;
                }
                else
                {
                    OnFinishedAttack();
                }
            }
        }
        base.Update();
    }

    bool ReachedDestination()
    {
        return Vector2.Distance(GetPosition(), positionToGo) <= destinationRadius;
    }

    private void DropTear()
    {
        FallingRocks.instance.GenerateRandomRock();
    }

    void FixedUpdate()
    {
        if (!ReachedDestination())
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), positionToGo, speed * Time.deltaTime);
        }
    }


    void ShootProjectiles()
    {
        if (ReachedDestination()) projectileShooter.ShootRotating();
    }

    void ChangeProjectilesRotation()
    {
        if (ReachedDestination()) projectileShooter.ChangeRotation();
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    protected override void SetDefaults()
    {
        currentRainTime = 0;
        currentTimeBtwDrops = intervalBtwDrops;
    }
}
