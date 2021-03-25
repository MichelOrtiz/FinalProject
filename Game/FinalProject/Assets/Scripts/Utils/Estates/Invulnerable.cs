using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Paralisis", menuName = "States/new Invulnerable")]
public class Invulnerable : State
{
    public override void StartAffect(StatesManager newManager){
        base.StartAffect(newManager);
        manager.hostEntity.gameObject.tag="Invinsible";
    }
    public override void Affect(){
        currentTime += Time.deltaTime;
        if(currentTime>=duration){
            StopAffect();
        }
    }
    public override void StopAffect()
    {
        base.StopAffect();
        manager.hostEntity.gameObject.tag = "Player";
    }
}
