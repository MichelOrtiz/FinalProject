using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    PlayerManager player;
    [SerializeField] State waterSlowState;

    void Start()
    {
        player = PlayerManager.instance;
        waterSlowState.onEffect = false;
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.currentGravity = .5f;
            waterSlowState = player.statesManager.AddState(waterSlowState);
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.currentGravity = .5f;
            waterSlowState = player.statesManager.AddState(waterSlowState);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
            GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.currentGravity = PlayerManager.defaultGravity;
            player.statesManager.RemoveState(waterSlowState);
        }
    }
}
