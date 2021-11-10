using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : Ability
{
    public bool isInShield;
    [SerializeField] State inmuniScudo;
    public GameObject escudo;
    public override void UseAbility()
    {   
        if(player.currentStamina < staminaCost + 0.1f)return;
        if(isInShield) return;
        base.UseAbility();
        inmuniScudo = PlayerManager.instance.statesManager.AddState(inmuniScudo);
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
            escudo.SetActive(true);
        }else{
            isInShield = false;
            escudo.SetActive(false);
        }
    }
}
