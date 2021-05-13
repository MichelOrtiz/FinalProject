using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rafaga : Ability
{
    public GameObject Rafagaobj;
    public bool isActivated;

    public override void UseAbility()
    {   
        if(player.currentStamina < staminaCost)return;
        isActivated=true;
        base.UseAbility();
    }
    protected override void Start()
    {
        base.Start();
        time = cooldownTime;
        isActivated=false;
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
            isActivated = false;
        }
        if (isActivated)
        {
            Rafagaobj.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            //player.abilityManager.escudo.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }else
        {
            Rafagaobj.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //player.abilityManager.gameObject.GetComponent<CircleCollider2D>().enabled = false;
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
