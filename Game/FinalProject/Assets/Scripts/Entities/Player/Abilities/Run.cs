using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : Ability
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] public float runningSpeed;
    public override void UseAbility()
    {
        if(player.currentStamina < staminaCost)return;
        if (isInCooldown)
        {
            player.TakeTirement(staminaCost);
            //Debug.Log("Usando en cooldown");
        }
        player.walkingSpeed = runningSpeed;
    }

    protected override void Start()
    {
        base.Start();
        runningSpeed = player.walkingSpeed * speedMultiplier;
    }

    protected override void Update()
    {
        if (!isUnlocked)
        {
            this.enabled = false;
        }
        if (Input.GetKeyUp(hotkey))
        {
            player.walkingSpeed = runningSpeed/speedMultiplier;
            isInCooldown = false;
            player.isRunning = false;
        }
        if (Input.GetKey(hotkey))
        {
            if (player.currentStamina > staminaCost)
            {
                UseAbility();
                player.isRunning = true;
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
            else
            {
                player.isRunning = false;
            }
        }
        
    }
}
