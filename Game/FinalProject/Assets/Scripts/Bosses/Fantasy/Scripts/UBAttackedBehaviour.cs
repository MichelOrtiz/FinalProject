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
    


    private bool reachedDestination;
    private PlayerManager player;

    new void Start()
    {
        base.Start();
        speed = averageSpeed * speedMultiplier;
        player = PlayerManager.instance;
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
                int index = minTears;
                int nTears = RandomGenerator.NewRandom(minTears, maxTears);
                while (index <= nTears)
                {
                   FallingRocks.instance.GenerateRandomRock();
                    index++;    
                }
            }
        }


        base.Update();
    }

    void FixedUpdate()
    {
        if (!reachedDestination)
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), positionToGo, speed * Time.deltaTime);
        }
    }
}
