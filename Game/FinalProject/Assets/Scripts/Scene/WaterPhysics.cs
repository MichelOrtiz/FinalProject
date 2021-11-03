using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    public Rigidbody2D body;
    PlayerManager player;
    Collider2D collision;
    [SerializeField] State waterSlowState;

    void Start()
    {
        player = PlayerManager.instance;

    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.currentGravity = .5f;
            //player.isInWater = true;
            //player.walkingSpeed = 3.5f;
            player.statesManager.AddState(waterSlowState);
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.currentGravity = .5f;
            //player.isInWater = true;
            //player.walkingSpeed = 3.5f;
            player.statesManager.AddState(waterSlowState);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
            GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.currentGravity = PlayerManager.defaultGravity;
            //player.isInWater = false;
            //player.walkingSpeed = PlayerManager.defaultwalkingSpeed;
            player.statesManager.RemoveState(waterSlowState);
        }
    }
}
