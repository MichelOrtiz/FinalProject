using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacado : MonoBehaviour
{
    public bool Dodgeable;
    void Start()
    {
        Dodgeable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Enemy")
        {
            Dodgeable = true;
            //Debug.Log("DodgeEable");
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Enemy")
        {
            Dodgeable = false;
            //Debug.Log("No DodgeEable");
        }
    }
}
