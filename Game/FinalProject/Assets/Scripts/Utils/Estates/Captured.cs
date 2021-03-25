using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captured : State
{
    PlayerManager player;
    Vector3 capturedPos;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        capturedPos = manager.hostEntity.GetPosition();
        player = PlayerManager.instance;
    }
    public override void Affect()
    {
        manager.hostEntity.transform.position = capturedPos;
        player.TakeTirement(Time.deltaTime);
        currentTime += Time.deltaTime;
        if(currentTime>=duration){
            StopAffect();
        }
        if(Input.GetKeyDown(KeyCode.A)){
            duration--;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            duration--;
        }
    }
}
