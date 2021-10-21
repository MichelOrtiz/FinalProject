using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour
{
    [SerializeField] private CollisionHandler collisionHandler;
    [SerializeField] private State iceState;
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
        collisionHandler.StayTouchingContactHandler += collisionHandler_StayContact;
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
            //player.isInIce = true;
            player.statesManager.AddState(iceState);
            Debug.Log("esta en ice");
        }
    }

    void RemoveEffects(GameObject contact)
    {
        if (contact.tag == "Player")
        {
            //player.isInIce = false;
            player.statesManager.RemoveState(iceState);
            
        }
    }

    void collisionHandler_EnterContact(GameObject contact)
    {
        SetEffects(contact);
    }
    void collisionHandler_StayContact(GameObject contact)
    {
        SetEffects(contact);
    }

    void collisionHandler_ExitContact(GameObject contact)
    {
        RemoveEffects(contact);
    }
}
