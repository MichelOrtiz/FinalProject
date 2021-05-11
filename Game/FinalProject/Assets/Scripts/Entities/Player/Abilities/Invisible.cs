using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : Ability
{
    public GameObject Nico;
    public bool isInvisible;

    public override void UseAbility()
    {   
        if(player.currentStamina < staminaCost)return;
        isInvisible=true;
        base.UseAbility();
    }
    protected override void Start()
    {
        base.Start();
        time = cooldownTime;
        isInvisible=false;
    }
    protected override void Update()
    {
        if (!isUnlocked)
        {
            this.enabled = false;
        }
        if (isInCooldown)
        {
            time += Time.deltaTime;
            if (time >= cooldownTime)
            {
                isInCooldown = false;
                time = 0;
            }
        }else
        {
            isInvisible = false;
        }
        if (isInvisible)
        {
            player.gameObject.layer = LayerMask.NameToLayer("Invisible");
            player.collisionHandler.gameObject.layer = LayerMask.NameToLayer("Invisible");
            player.abilityManager.gameObject.layer = LayerMask.NameToLayer("Invisible");
        }else
        {
            player.gameObject.layer = LayerMask.NameToLayer("Default");
            player.collisionHandler.gameObject.layer = LayerMask.NameToLayer("Default");
            player.abilityManager.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        if (Input.GetKeyDown(hotkey))
        {
            if (player.currentStamina > staminaCost)
            {
                UseAbility();
            }
        }
    }
}
