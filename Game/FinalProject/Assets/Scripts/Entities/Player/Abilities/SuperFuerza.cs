using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFuerza : Ability
{
    new void Start()
    {
        base.Start();
        player.collisionHandler.EnterTouchingContactHandler -= collision_EnterContact;
        player.collisionHandler.EnterTouchingContactHandler += collision_EnterContact;

        player.collisionHandler.ExitTouchingContactHandler -= collision_ExitContact;
        player.collisionHandler.ExitTouchingContactHandler += collision_ExitContact;

    }

    void collision_EnterContact(GameObject contact)
    {
        if (!isUnlocked) return;
        if(contact.gameObject.CompareTag("Movable"))
        {
            if (contact.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    void collision_ExitContact(GameObject contact)
    {
        if (!isUnlocked) return;
        if(contact.gameObject.CompareTag("Movable"))
         {
            if (contact.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
    }
}
