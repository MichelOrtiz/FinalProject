using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : Entity
{
    [SerializeField] private float baseSpeedMultiplier;
    [SerializeField] private float speedChangeMultiplier;
    private float speed;
    [SerializeField] private float intervalToChangeSpeed;
    private float currentTimeUntilChange;
    [SerializeField] private float changeSpeedTimeAcive;
    private float currentChangedSpeedTime;
    private bool speedChanged;

    

    [SerializeField] private float damageAmount;

    /* change to enemy and inherit later*/
    private EnemyCollisionHandler collisionHandler;
    private bool touchingPlayer;

    private PlayerManager player;
    /*-----------------------------------*/


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        speed = averageSpeed * baseSpeedMultiplier;
    }

    // Update is called once per frame
    new void Update()
    {
        if (currentTimeUntilChange > intervalToChangeSpeed)
        {
            if (!speedChanged)
            {
                speed = averageSpeed * speedChangeMultiplier;
                speedChanged = true;
            }
            if (currentChangedSpeedTime > changeSpeedTimeAcive)
            {
                speed = averageSpeed * baseSpeedMultiplier;
                currentChangedSpeedTime = 0;
                currentTimeUntilChange = 0;
                speedChanged = false;
            }
            else
            {
                currentChangedSpeedTime += Time.deltaTime;
            }
        }
        else
        {
            currentTimeUntilChange += Time.deltaTime;
        }
        base.Update();
    }

    void FixedUpdate()
    {
        if (GetPosition().y > player.GetPosition().y)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = true;
            player.TakeTirement(damageAmount);
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = false;
        }
    }
}
