using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour
{
    [SerializeField] private CollisionHandler collisionHandler;
    PlayerManager player;
    Collider2D collision;

    void Start()
    {
        if (collisionHandler == null)
        {
            collisionHandler = GetComponent<CollisionHandler>();
        }
        player = PlayerManager.instance;

        collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterContact;
        //collisionHandler.StayTouchingContactHandler += collisionHandler_StayContact;
        collisionHandler.ExitTouchingContactHandler += collisionHandler_ExitContact;
    }

    void Update()
    {
    }

    void SetEffects(GameObject contact)
    {
        if (contact.tag == "Player")
        {
            //player.rigidbody2d.AddForce(new Vector2( 300f * player.rigidbody2d.velocity.x, player.rigidbody2d.velocity.y));
            player.isInIce = true;
            player.walkingSpeed = 100f;
        }
    }

    void RemoveEffects(GameObject contact)
    {
        if (contact.tag == "Player")
        {
            player.isInIce = false;
            player.walkingSpeed = player.defaultwalkingSpeed;
        }
    }

    void collisionHandler_EnterContact(GameObject contact)
    {
        SetEffects(contact);
    }
   /* void collisionHandler_StayContact(GameObject contact)
    {
        SetEffects(contact);
    }*/

    void collisionHandler_ExitContact(GameObject contact)
    {
        RemoveEffects(contact);
    }
}
