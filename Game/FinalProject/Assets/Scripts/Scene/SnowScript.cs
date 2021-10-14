using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowScript : MonoBehaviour
{
    [SerializeField] State snowState;

    PlayerManager player;
    Collider2D collision;
    // Start is called before the first frame update
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
            if (!player.isInIce)
            {
                player.isInSnow = true;
                player.statesManager.AddState(snowState);
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
                player.statesManager.AddState(snowState);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
            GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.isInSnow = false;
            player.statesManager.RemoveState(snowState);//ojala funcione
        }
    }
}
