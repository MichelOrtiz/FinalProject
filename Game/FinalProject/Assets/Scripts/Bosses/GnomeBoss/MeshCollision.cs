using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCollision : MonoBehaviour
{
    public bool touchingPlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            Debug.Log("Fov touching player");
        }

    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            Debug.Log("Fov stopped touching player");
        }
    }
}
