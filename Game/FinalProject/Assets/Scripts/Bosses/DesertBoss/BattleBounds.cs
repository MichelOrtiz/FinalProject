using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBounds : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log(other.gameObject.name + " inside");
            other.gameObject.layer = LayerMask.NameToLayer("Ghost");
        }
    }*/
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.layer = LayerMask.NameToLayer("Ghost");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.layer = LayerMask.NameToLayer("Enemies");
        }
    }
}
