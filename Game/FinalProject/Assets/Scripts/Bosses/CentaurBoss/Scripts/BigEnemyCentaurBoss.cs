using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyCentaurBoss : Entity
{
    [SerializeField] private Vector2 target;
    [SerializeField] private float speedMultiplier;
    private float speed;
    
    private PlayerManager player;
    private bool touchingPlayer;
    
    new void Start()
    {
        base.Start();
        speed = averageSpeed * speedMultiplier;
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    new void Update()
    {
        // base.Update
    }

    void FixedUpdate()
    {
        if (GetPosition().x < target.x)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            player.TakeTirement(player.currentStamina);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
        }  
    }
}
