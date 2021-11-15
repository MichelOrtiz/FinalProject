using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : Ability
{
    public override KeyCode hotkey {get => PlayerManager.instance.inputs.controlBinds["RUN"];}
    [SerializeField] private float speedMultiplier;
    [SerializeField] public float runningSpeed;
    [SerializeField] private float LimitStamCost;
    [SerializeField] private Rigidbody2D body;
    public override void UseAbility()
    {
        if (isInCooldown)
        {
            if (player.statesManager.currentStates.Contains(Escudo.scudoState))
            {
                beenUsed = true;
            }
            player.TakeTirement(staminaCost);
        }
        player.isRunning = true;
        player.currentSpeed = runningSpeed;
        isInCooldown = true;
    } 
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (!isUnlocked)
        {
            this.enabled = false;
        }
        if (Input.GetKeyUp(hotkey))
        {
            StopRunning();
        }
        if(Input.GetKeyDown(hotkey)){
            StartRunning();
        }
        if (Input.GetKey(hotkey))
        {
            if (player.currentStamina > staminaCost && player.currentStamina >= LimitStamCost && player.inputs.movementX != 0)
            {
                UseAbility();
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    isInCooldown = false; //evita que se consuma inmediatamente la stamina
                }
                else if(time <= 0)
                {
                    time = cooldownTime;
                }
            }
            else
            {
                StopRunning();
            }
        }
        
    }
    /*
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
    */
    public float GetSpeedMultiplier(){
        return speedMultiplier;
    }
    public void SetSpeedMultiplier(float newSpeedMultiplier){
        speedMultiplier = newSpeedMultiplier;
        runningSpeed = PlayerManager.defaultwalkingSpeed * speedMultiplier;
    }
    void StartRunning(){
        runningSpeed = PlayerManager.defaultwalkingSpeed * speedMultiplier;
        isInCooldown = true;
    }
    void StopRunning(){
        player.currentSpeed = player.walkingSpeed;
        isInCooldown = false;
        player.isRunning = false;
    }
}
