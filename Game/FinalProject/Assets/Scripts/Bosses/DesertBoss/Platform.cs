using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool isTarget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTarget)
        {
            gameObject.layer = LayerMask.NameToLayer("Fake");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Ground");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Spit")
        {
            isTarget = false;
            
        }
    }
}
