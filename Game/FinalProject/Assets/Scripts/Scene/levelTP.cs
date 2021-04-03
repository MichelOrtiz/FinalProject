using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTP : MonoBehaviour
{
    public PlayerManager player;
    public float posx, posy;
    public static loadlevel instance = null;

    private void OnTriggerEnter2D(Collider2D collision){
        
        player = PlayerManager.instance;
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
           player.transform.position = new Vector2(posx,posy);
        }
    }
}
