using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Paralisis", menuName = "States/new Escudo")]
public class DañoAnular : State
{
    float stamina, castigo;
    public override void StartAffect(StatesManager newManager){
        base.StartAffect(newManager);
        stamina = PlayerManager.instance.currentStamina;
        castigo = PlayerManager.instance.currentStaminaLimit;
    }
    public override void Affect(){
        RemoveStaminaFromHabilities();
        currentTime += Time.deltaTime;
        if(currentTime>=duration){
            StopAffect();
        }
        PlayerManager.instance.currentStaminaLimit = castigo;
        PlayerManager.instance.currentStamina = stamina;
        
    }
    public override void StopAffect(){
       base.StopAffect(); 
    }
    public void RemoveStaminaFromHabilities(){
        foreach (Ability ability in AbilityManager.instance.abilities)
        {
            if (ability.beenUsed)
            {
                stamina -= ability.staminaCost;
                ability.beenUsed=false;
            }
        }
    }
}
