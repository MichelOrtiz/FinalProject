using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreparParedes : Ability
{
    public GameObject NicoFeet;
    public Collider2D escalera;
    public Escalera esca;
    public override void UseAbility()
    {   
        
        if(player.currentStamina < staminaCost + 0.1f)return;
        escalera.enabled = true;
        if (esca.isLadder)
        {
            player.rigidbody2d.velocity = new Vector2(player.rigidbody2d.velocity.x, player.inputs.movementY * player.currentSpeed-1);
        }
        else
        {
        }
        if (isInCooldown)
        {
            player.TakeTirement(staminaCost);
        }
    }
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (!isUnlocked)
        {
            this.enabled = false;
        }
        if (Input.GetKeyUp(hotkey))
        {
            escalera.enabled = false;
            
        }
        if (Input.GetKey(hotkey))
        {
            if (player.currentStamina > staminaCost)
            {
                UseAbility();
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    isInCooldown = false;
                }
                else if(time <= 0)
                {
                    time = cooldownTime;
                    isInCooldown = true;
                }
            }
        }

    }
}
