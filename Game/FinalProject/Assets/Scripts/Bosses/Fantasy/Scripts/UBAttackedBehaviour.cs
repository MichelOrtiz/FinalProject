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
    [SerializeField] private int minTears;
    [SerializeField] private int maxTears;
    [SerializeField] private float rainTime;
    private float currentRainTime;
    [SerializeField] private float intervalBtwDrops;
    private float currentTimeBtwDrops;
    private int nTears;
    private int index;

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

    protected override void SetDefaults()
    {
        currentRainTime = 0;
        currentTimeBtwDrops = intervalBtwDrops;
    }
}
