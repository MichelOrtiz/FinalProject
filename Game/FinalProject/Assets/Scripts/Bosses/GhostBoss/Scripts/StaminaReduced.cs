using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New StaminaReduced", menuName = "States/new StaminaReduced")]
public class StaminaReduced : State
{
    [SerializeField] private float staminaSet;

    PlayerManager player;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        player = PlayerManager.instance;
    }
    public override void Affect()
    {
        currentTime += Time.deltaTime;
        player.currentStamina = staminaSet;
        
        if (currentTime >= duration)
        {
            StopAffect();
        }
    }
    public override void StopAffect(){
        base.StopAffect();
    }
}
