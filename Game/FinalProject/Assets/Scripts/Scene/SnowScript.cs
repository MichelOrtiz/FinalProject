using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowScript : MonoBehaviour
{
    public Rigidbody2D body;
    PlayerManager player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
        
    }

    void Update(Collider2D collision)
    {
        if (player.isInSnow)
        {
            GameObject collisionGameObject = collision.gameObject;
            if (collisionGameObject.tag == "Player")
            {
                if (!player.isInIce)
                {
                    player.isInSnow = true;
                    player.walkingSpeed = 4.25f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            if (!player.isInIce)
            {
                player.isInSnow = true;
                player.walkingSpeed = 4.25f;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {            
            if (!player.isInIce)
            {
                player.isInSnow = true;
                player.walkingSpeed = 4.25f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
            GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.isInSnow = false;
            player.walkingSpeed = player.defaultwalkingSpeed;
        }
    }
}
