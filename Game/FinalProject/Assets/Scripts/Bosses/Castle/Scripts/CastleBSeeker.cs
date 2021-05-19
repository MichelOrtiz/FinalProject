using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBSeeker : Entity
{
    [SerializeField] private float speedMultiplier;
    private float speed;
    PlayerManager player;


    // Start is called before the first frame update
    new void Start()
    {
        speed = averageSpeed * speedMultiplier;
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    new void Update()
    {
        //transform.position += transform.Translate(player.GetPosition() * speed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        rigidbody2d.position = Vector2.MoveTowards(GetPosition(), player.GetPosition(), speed * Time.deltaTime);
    }
}
