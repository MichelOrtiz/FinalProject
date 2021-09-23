using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Respirar como pez", menuName = "States/BreatheWater")]
public class BreatheWater : State
{
    private float oxygen;
    private float maxOxygen;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            PlayerManager player = manager.hostEntity.GetComponent<PlayerManager>();
            oxygen = player.currentOxygen = player.maxOxygen;
            
        }

    }
    public override void Affect()
    {
        //player.StopCoroutine(player.Drowning(1f, 0.05f));
        if(oxygen != maxOxygen){
            oxygen = maxOxygen;
        }
        currentTime += Time.deltaTime;
        if(currentTime >= duration){
            StopAffect();
        }
    }
}
