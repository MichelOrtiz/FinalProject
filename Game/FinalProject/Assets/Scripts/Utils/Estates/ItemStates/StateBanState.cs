using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New State", menuName = "States/BanState")]
public class StateBanState : State
{
    [SerializeField]
    private State banState;
    public override void Affect()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= duration){
            StopAffect();
        }
    }
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            manager.bannedStates.Add(banState);
        }
        else if (manager.hostEntity.GetComponent<Enemy>() != null)
        {
            StopAffect();
        }
    }
    public override void StopAffect()
    {
        base.StopAffect();
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
           manager.bannedStates.Remove(banState);
        }
        
    }
}
