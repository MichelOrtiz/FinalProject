using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessScript : MonoBehaviour
{
    public GameObject Oscuridad;
    PlayerManager player;
    Collider2D collision;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
        player.isInDark = false;
        Oscuridad.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.isInDark = true;
            Oscuridad.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            player.isInDark = false;
            Oscuridad.SetActive(false);
        }
    }
}
