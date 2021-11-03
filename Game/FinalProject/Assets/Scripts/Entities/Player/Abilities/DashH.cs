using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashH : Ability
{
    [SerializeField]private Rigidbody2D body;
    public override KeyCode hotkey {get => PlayerManager.instance.inputs.controlBinds["MOVERIGHT"];}
    protected KeyCode altHotkey {get => PlayerManager.instance.inputs.controlBinds["MOVELEFT"];}
    private KeyCode lastKey;
    private float timeKeyPressed;
    public float doubleTimeTap;
    byte nKeyPressed;
    [SerializeField] KnockbackState dashH; 
    public override void UseAbility()
    {
        nKeyPressed = 0;
        if(player.currentStamina < staminaCost + 0.1f)return;
        base.UseAbility();
        dashH = (KnockbackState)player.statesManager.AddState(dashH);
        if (player.abilityManager.IsUnlocked(Abilities.DodgePerfecto))
        {
            player.SetImmune(duration);
        }
    }


    protected override void Start()
    {
        base.Start();
        dashH.onEffect = false;
    }
    protected override void Update(){
        this.enabled = isUnlocked;
        if (isInCooldown)
        {
            time += Time.deltaTime;
            if (time >= cooldownTime)
            {
                isInCooldown = false;
                time = 0;
            }
        }
        player.isDashingH = player.statesManager.currentStates.Contains(dashH);
        Dash(hotkey);
        Dash(altHotkey);
        
    }

    private void Dash(KeyCode key){
        
        if (Input.GetKeyDown(key))
        {
            nKeyPressed++;
            if (lastKey != key)
            {
                nKeyPressed = 0;
                timeKeyPressed = 0;
            }
            lastKey = key;
        }
        if (nKeyPressed == 1)
        {
            if (timeKeyPressed<doubleTimeTap)
            {
                timeKeyPressed += Time.deltaTime;
            }else
            {
                nKeyPressed = 0;
                timeKeyPressed = 0;
            }
        }
        if (nKeyPressed >= 2)
        {
            UseAbility();
        }
    }
    /*private void FixedUpdate() {
        if(player.isDashingH){
            body.AddForce(target * speed * Time.deltaTime);
        }
    }*/
    
}
