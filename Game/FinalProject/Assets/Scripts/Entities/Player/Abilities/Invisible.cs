using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : Ability
{
    public GameObject Atacado;
    [SerializeField] SpriteRenderer sprite;
    Color tmp;
    public override void UseAbility()
    {   
        if(player.currentStamina < staminaCost)return;
        player.isInvisible=true;
        base.UseAbility();
        tmp = sprite.color;
    }
    protected override void Start()
    {
        base.Start();
        time = cooldownTime;
        player.isInvisible=false;
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
            player.isInvisible = false;
        }
        if (player.isInvisible)
        {
            
            if (sprite.color.a == 1)
            {
                sprite.color = new Color(1,1,1,.5f);
            }
            player.gameObject.layer = LayerMask.NameToLayer("Invisible");
            player.collisionHandler.gameObject.layer = LayerMask.NameToLayer("Invisible");
            player.groundChecker.gameObject.layer = LayerMask.NameToLayer("Invisible");
            player.abilityManager.gameObject.layer = LayerMask.NameToLayer("Invisible");
            Atacado.gameObject.layer = LayerMask.NameToLayer("Invisible");
        }else
        {
            if (sprite.color.a == .5)
            {
                sprite.color = new Color(1,1,1,1);
            }
            player.gameObject.layer = LayerMask.NameToLayer("Default");
            if (!player.isImmune) player.collisionHandler.gameObject.layer = LayerMask.NameToLayer("Default");
            player.groundChecker.gameObject.layer = LayerMask.NameToLayer("Fake");
            player.abilityManager.gameObject.layer = LayerMask.NameToLayer("Default");
            Atacado.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
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
