using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : Ability
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] public float runningSpeed;
    [SerializeField] private float LimitStamCost;
    [SerializeField] private Rigidbody2D body;
    public override void UseAbility()
    {
        //if(player.currentStamina >= LimitStamCost && body.velocity.magnitude != 0){
            if (isInCooldown)
            {
                player.TakeTirement(staminaCost);
                //Debug.Log("Usando en cooldown");
            }
            player.walkingSpeed = runningSpeed;
            if (player.isInWater)
            {
                player.walkingSpeed = 7f;
            }
            if (player.isInIce)
            {
                player.walkingSpeed = 75f;
            }
            if (player.isInSnow && !player.isInIce)
            {
                player.walkingSpeed = 8.5f;
            }
            if (player.isInConvey)
            {
                player.walkingSpeed = 75f;
            }
        //}
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
            if (player.currentStamina > staminaCost && player.currentStamina >= LimitStamCost && player.inputs.movementX != 0)
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
                player.walkingSpeed = runningSpeed/speedMultiplier;
                isInCooldown = false;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Water")
        {
            runningSpeed = player.walkingSpeed * speedMultiplier;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Water")
        {
            player.walkingSpeed = PlayerManager.defaultwalkingSpeed;
            runningSpeed = player.walkingSpeed * speedMultiplier;
        }
    }
    public float GetSpeedMultiplier(){
        return speedMultiplier;
    }
    public void SetSpeedMultiplier(float newSpeedMultiplier){
        speedMultiplier = newSpeedMultiplier;
    }
}
