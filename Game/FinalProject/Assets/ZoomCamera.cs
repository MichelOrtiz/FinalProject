using System;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public float zCam;
    public Bounds Bounds;

    public Action<ZoomCamera> EnterBounds;

    
    
    void Start()
    {
        Bounds = GetComponent<BoxCollider2D>().bounds;
    }

    void Update()
    {
     //4.487261   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnterBounds?.Invoke(this);
    }

    /*
    void collisionHandler_EnterContact(GameObject contact)
    {
        if (contact.tag == "Player")
        {
            if (PlayerManager.instance.collisionHandler.Contacts.Exists(!))
            EnterBounds?.Invoke(this);
        }
    }*/

}
