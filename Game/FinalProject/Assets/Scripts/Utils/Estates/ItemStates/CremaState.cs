using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New State", menuName = "States/Items/Crema")]
public class CremaState : State
{
    [SerializeField] private float gravityMultiplier;
    PlayerManager player = null;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            player = manager.hostEntity.GetComponent<PlayerManager>();
            player.currentGravity *= gravityMultiplier;
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
            player.currentGravity = PlayerManager.defaultGravity;
        }
    }
    
}
