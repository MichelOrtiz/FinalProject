using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New State", menuName = "States/new SpeedMultiplier")]
public class SpeedMultiplier : State
{
    [SerializeField] private float speedMultiplier;
    PlayerManager player;
    enum Hazard
    {
        Nieve,Hielo,Viento,Arena,Agua,Nada
    }
    [SerializeField] Hazard hazard = Hazard.Nada;
    Run runOverride = null;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer)
        {
            player = manager.hostEntity.GetComponent<PlayerManager>();
            //Hacer que camines rapido
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
            switch(hazard){
                case Hazard.Nieve:{
                    player.isInSnow = true;
                    break;
                }
                case Hazard.Hielo:{
                    player.isInIce = true;
                    break;
                }
                default:{
                    break;
                }
            }
            
        }
        else if (manager.hostEntity.GetComponent<Enemy>() != null)
        {
            StopAffect();
        }
    }
    public override void Affect()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= duration){
            StopAffect();
        }
    }
    public override void StopAffect()
    {
        base.StopAffect();
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            //regresar la velocidad 
            player.speedMods /= speedMultiplier;
            //regresar la velocidad de la habilidad run a sus valores anteriores...
            if(runOverride != null){
                float newSpeedMultiplier = runOverride.GetSpeedMultiplier() / speedMultiplier;
                runOverride.SetSpeedMultiplier(newSpeedMultiplier);
            }
            
            switch(hazard){
                case Hazard.Nieve:{
                    player.isInSnow = false;
                    break;
                }
                case Hazard.Hielo:{
                    player.isInIce = false;
                    break;
                }
                default:{
                    break;
                }
            }
            
        }
    }
}
