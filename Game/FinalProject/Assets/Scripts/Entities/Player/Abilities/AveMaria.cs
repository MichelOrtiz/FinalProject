using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AveMaria : Ability
{
    private int AvesMarias;
    void collisionHandler_EnterContact(GameObject contact){

        if (contact.gameObject.tag == "Projectile")
        {
            AvesMarias++;
            if (AvesMarias == 4)
            {
                Debug.Log("Stamina increased by 10");
                player.currentStamina += 10;
                AvesMarias = 0;
            }
        }
    }
    
    protected override void Start ()
    {
        base.Start();
        player.collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterContact;
        AvesMarias = 0;
    }
}
