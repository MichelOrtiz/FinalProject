using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour
{
    public Rigidbody2D body;
    PlayerManager player;
    Collider2D collision;

    void Start()
    {
        player = PlayerManager.instance;

    }

    void Update()
    {
        if (player.isInWater)
        {
            GameObject collisionGameObject = collision.gameObject;
            if (collisionGameObject.tag == "Player")
            {
                player.isInIce = true;
                player.walkingSpeed = 50f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.isInIce = true;
            player.walkingSpeed = 50f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.isInIce = true;
            player.walkingSpeed = 50f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
            GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.isInIce = false;
            player.walkingSpeed = player.defaultwalkingSpeed;
        }
    }
}
