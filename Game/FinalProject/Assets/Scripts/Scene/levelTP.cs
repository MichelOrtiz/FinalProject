using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTP : MonoBehaviour
{
    public int posx, posy;
    public static loadlevel instance = null;

    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
           transform.position = new Vector2(posx,posy);
        }
    }
}
