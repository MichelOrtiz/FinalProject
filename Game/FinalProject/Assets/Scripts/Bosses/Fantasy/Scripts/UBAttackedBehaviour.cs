using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UBAttackedBehaviour : Entity
{
    [SerializeField] private float speedMultiplier;
    private float speed;
    
    [SerializeField] private Vector2 positionToGo;
    [SerializeField] private float destinationRadius;


    [SerializeField] private int minTears;
    [SerializeField] private int maxTears;
    
    [SerializeField] private float rainTime;
    private float currentRainTime;
    [SerializeField] private float intervalBtwDrops;
    private float currentTimeBtwDrops;


    private bool reachedDestination;
    private PlayerManager player;
    private int index;
    private int nTears;

    new void Start()
    {
        base.Start();
        speed = averageSpeed * speedMultiplier;
        player = PlayerManager.instance;
        currentTimeBtwDrops = intervalBtwDrops;
    }

    // Update is called once per frame
    new void Update()
    {
        if (!reachedDestination)
        {
            if (Vector2.Distance(GetPosition(), positionToGo) <= destinationRadius)
            {
                //OnReachedDestination();
                reachedDestination = true;
                
            }
        }
        else
        {
            if (currentRainTime < rainTime)
            {
                if (currentTimeBtwDrops > intervalBtwDrops)
                {
                    index = minTears;
                    nTears = RandomGenerator.NewRandom(minTears, maxTears);
                    Debug.Log(nTears);
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
        }


        base.Update();
    }

    private void DropTear()
    {
        FallingRocks.instance.GenerateRandomRock();
    }

    void FixedUpdate()
    {
        if (!reachedDestination)
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), positionToGo, speed * Time.deltaTime);
        }
    }
}
