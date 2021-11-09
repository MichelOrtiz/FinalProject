using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreparParedes : Ability
{
    public GameObject NicoFeet;
    public GameObject escalera;
    public Escalera esca;
    public override void UseAbility()
    {   
        
        if(player.currentStamina < staminaCost + 0.1f)return;
        escalera.SetActive(true);
        if (esca.isLadder)
        {
            player.currentGravity = 0;
            player.rigidbody2d.velocity = new Vector2(player.rigidbody2d.velocity.x, player.inputs.movementY * player.currentSpeed);
        }
        else
        {
            player.currentGravity = 2.5f;
        }
        if (isInCooldown)
        {
            player.TakeTirement(staminaCost);
            //Debug.Log("Usando en cooldown");
        }
    }
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        //Entonces, para este, por el momento hay un bug el cual le permite a nico saltar en las paredes, lo cual seria como si tuviera la
        //habilidad de trepar, segun yo, ya lo elimine, reduciendo su feetpos, entonces para hacerlo, simplemente deberiamos de aumentarla
        //nuevamente, cambiarle de .12 a .13 en el ground checker
        if (!isUnlocked)
        {
            this.enabled = false;
        }
        if (Input.GetKeyUp(hotkey))
        {
            player.currentGravity = 2.5f;
            escalera.SetActive(false);
            player.isClimbing = false;
        }
        if (Input.GetKey(hotkey))
        {
            if (player.currentStamina > staminaCost)
            {
                UseAbility();
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
        }

    }

    private IEnumerator Cansancio(){
        yield return new WaitForSecondsRealtime(1);
       
    }
}
