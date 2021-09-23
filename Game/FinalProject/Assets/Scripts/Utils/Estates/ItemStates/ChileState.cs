using UnityEngine;

[CreateAssetMenu(fileName="New State", menuName = "States/Items/Chile")]
public class ChileState : State
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float staminaCostMultiplier;
    private float defaultWalkingSpeed;
    PlayerManager player;
    Run runOverride = null;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer)
        {
            player = manager.hostEntity.GetComponent<PlayerManager>();
            defaultWalkingSpeed = player.walkingSpeed;
            player.walkingSpeed *= speedMultiplier;
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
            //Aumentar el costo de las abilidades RIP player
            foreach(Ability a in player.abilityManager.abilities){
                float newStaminaCost = a.GetStaminaCost() * staminaCostMultiplier;
                a.SetStaminaCost(newStaminaCost);
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
            player.walkingSpeed = defaultWalkingSpeed;
            //regresar la velocidad de la habilidad run a sus valores anteriores...
            if(runOverride != null){
                float newSpeedMultiplier = runOverride.GetSpeedMultiplier() / speedMultiplier;
                runOverride.SetSpeedMultiplier(newSpeedMultiplier);
            }
            //Regresar el costo de las abilidades a sus valores anteriores
            foreach(Ability a in player.abilityManager.abilities){
                float newStaminaCost = a.GetStaminaCost() / staminaCostMultiplier;
                a.SetStaminaCost(newStaminaCost);
            }
        }
    }
}
