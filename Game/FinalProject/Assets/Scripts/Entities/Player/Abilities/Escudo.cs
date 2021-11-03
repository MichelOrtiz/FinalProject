using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : Ability
{
    public bool isInShield;
    [SerializeField] State inmuniScudo;

    public override void UseAbility()
    {   
        if(player.currentStamina < staminaCost)return;
        if(isInShield) return;
        inmuniScudo = PlayerManager.instance.statesManager.AddState(inmuniScudo);
        base.UseAbility();
    }
    protected override void Start()
    {
        base.Start();
        time = cooldownTime;
        isInShield=false;
        inmuniScudo.onEffect = false;
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
        }
        if (Input.GetKeyDown(hotkey))
        {
            UseAbility();
        }
        if(player.statesManager.currentStates.Contains(inmuniScudo)){
            isInShield = true;
        }else{
            isInShield = false;
        }
    }
}
