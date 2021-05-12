using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : Ability
{
    public GameObject Escudobj;
    public bool isInShield;

    public override void UseAbility()
    {   
        if(player.currentStamina < staminaCost)return;
        isInShield=true;
        base.UseAbility();
    }
    protected override void Start()
    {
        base.Start();
        time = cooldownTime;
        isInShield=false;
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
            isInShield = false;
        }
        if (isInShield)
        {
            Escudobj.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            //player.abilityManager.escudo.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }else
        {
            Escudobj.gameObject.GetComponent<CircleCollider2D>().enabled = false;
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
