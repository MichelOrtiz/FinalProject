using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AveMaria : Ability
{
    private int AvesMarias;
    [SerializeField] private float staminaIncrease;
    void collisionHandler_EnterContact(GameObject contact){
        if (!isUnlocked) return;

        if (contact.gameObject.tag == "Projectile")
        {
            AvesMarias++;
            if (AvesMarias == 4)
            {
                Debug.Log("Stamina increased by " + staminaIncrease);
                player.RegenStamina(staminaIncrease);
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
