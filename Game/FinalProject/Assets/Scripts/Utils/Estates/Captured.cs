using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Paralisis", menuName = "States/new Captured")]
public class Captured : State
{
    [SerializeField]private float staminaLost;
    PlayerManager player;
    public State penalization;
    Vector3 capturedPos;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        if(manager.enemy!=null)manager.enemy.enabled=false;
        player = PlayerManager.instance;
        player.SetEnabledPlayer(false);
        capturedPos = manager.hostEntity.GetPosition();
        player.ResetAnimations();
        player.isCaptured = true;
    }
    public override void Affect()
    {
        manager.hostEntity.transform.position = capturedPos;
        player.TakeTirement(Time.deltaTime * staminaLost);
        currentTime += Time.deltaTime;
        if(currentTime>=duration){
            StopAffect();
        }
        if(player.currentStamina <= 5){
            StopAffect();
        }
        if(Input.GetKeyDown(PlayerManager.instance.inputs.controlBinds["MOVERIGHT"])){
            currentTime++;
        }
        if(Input.GetKeyDown(PlayerManager.instance.inputs.controlBinds["MOVELEFT"])){
            currentTime++;
        }
    }
    public override void StopAffect(){
        player.isCaptured = false;
        player.SetImmune();
        base.StopAffect();
        Debug.Log("captured stopped");
        if(manager.enemy!=null)manager.enemy.enabled=true;
        if(manager.enemy!=null){
            manager.enemy.statesManager.AddState(penalization);
        }
        player.walkingSpeed = PlayerManager.defaultwalkingSpeed;
        player.SetEnabledPlayer(true);
        player.inputs.ResetHotbarInputs();
        GunProjectile.instance.StopAiming();
        //player.ResetAnimations();
    }
}
