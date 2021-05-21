using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour
{
    public Rigidbody2D body;
    PlayerManager player;

    void Start()
    {
        player = PlayerManager.instance;

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
