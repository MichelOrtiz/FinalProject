using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreparParedes : Ability
{
    public GameObject NicoFeet;

    public override void UseAbility()
    {   
        
        if(player.currentStamina < staminaCost)return;
        NicoFeet.gameObject.GetComponent<GroundChecker>().checkFeetRadius=.15f;
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
            NicoFeet.gameObject.GetComponent<GroundChecker>().checkFeetRadius=.12f;
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
