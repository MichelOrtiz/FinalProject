using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : Ability
{
    public GameObject Escudobj;
    public bool isInShield;
    public float staminaActual;
    [SerializeField] State inmuniScudo;

    public override void UseAbility()
    {   
        if(player.currentStamina < staminaCost)return;
        isInShield=true;
        base.UseAbility();
        staminaActual = player.currentStamina;
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
            if (!player.statesManager.currentStates.Exists(x=>x.name == inmuniScudo.name))
            {
                player.statesManager.AddState(inmuniScudo);
            }
        }else
        {
            if (player.statesManager.currentStates.Exists(x=>x.name == inmuniScudo.name))
            {
                player.statesManager.RemoveState(inmuniScudo);
            }
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
