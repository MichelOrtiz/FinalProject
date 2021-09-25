using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New State", menuName = "States/Items/Crujitos")]
public class CrujitosState : State
{
    [SerializeField] private float dmgMultiplier;
    float prevDmgMod = 0;
    PlayerManager player;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer)
        {
            player = manager.hostEntity.GetComponent<PlayerManager>();
            prevDmgMod = player.dmgMod;
            player.dmgMod *= dmgMultiplier;
        }
    }
    public override void Affect()
    {
        currentTime += Time.deltaTime;
        
        if(currentTime >= duration){
            StopAffect();
        }else{
            if(player.dmgMod == PlayerManager.defaultDmgMod){
                player.dmgMod *= dmgMultiplier;
            }
        }
    }
    public override void StopAffect()
    {
        base.StopAffect();
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            player.dmgMod = prevDmgMod;
        }
    }
}
