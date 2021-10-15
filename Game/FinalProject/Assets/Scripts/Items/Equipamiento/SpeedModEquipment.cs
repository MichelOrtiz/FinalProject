using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Equipment", menuName = "Equipment/SpeedMod")]
public class SpeedModEquipment : NegateStateEquipment
{
   [SerializeField] float speedMultiplier;
   Run runOverride = null;
   public override void StartEquip(){
       base.StartEquip();
       player.speedMods *= speedMultiplier;
       //Hacer que la habilidad correr sea mas rapida
        foreach(Ability a in player.abilityManager.abilities){
            if(a.abilityName == Ability.Abilities.Correr){
                runOverride = (Run)a;
                break;
            }
        }
        if(runOverride != null){
            float newSpeedMultiplier = runOverride.GetSpeedMultiplier() * speedMultiplier;
            runOverride.SetSpeedMultiplier(newSpeedMultiplier);
        }
   }
   public override void EndEquip()
    {
        base.EndEquip();
        player.statesManager.bannedStates.Remove(negateThisState);
        player.speedMods /= speedMultiplier;
        //regresar la velocidad de la habilidad run a sus valores anteriores...
        if(runOverride != null){
            float newSpeedMultiplier = runOverride.GetSpeedMultiplier() / speedMultiplier;
            runOverride.SetSpeedMultiplier(newSpeedMultiplier);
        }
    }

}
