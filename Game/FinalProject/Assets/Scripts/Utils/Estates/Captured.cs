using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Paralisis", menuName = "States/new Captured")]
public class Captured : State
{
    [SerializeField]private float staminaLost;
    PlayerManager player;
    PlayerInputs inputs;
    public State penalization;
    Vector3 capturedPos;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        if(manager.enemy!=null)manager.enemy.enabled=false;
        player = PlayerManager.instance;
        inputs = player.gameObject.GetComponent<PlayerInputs>();
        inputs.enabled=false;
        player.abilityManager.gameObject.SetActive(false);
        capturedPos = manager.hostEntity.GetPosition();
        player = PlayerManager.instance;
        player.GetComponent<PlayerInputs>().enabled=false;

    }
    public override void Affect()
    {
        manager.hostEntity.transform.position = capturedPos;
        player.TakeTirement(Time.deltaTime * staminaLost);
        currentTime += Time.deltaTime;
        if(currentTime>=duration){
            StopAffect();
        }
        if(Input.GetKeyDown(KeyCode.A)){
            currentTime++;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            currentTime++;
        }
    }
    public override void StopAffect(){
        player.SetImmune();
        base.StopAffect();
        Debug.Log("captured stopped");
        inputs.enabled=true;
        if(manager.enemy!=null)manager.enemy.enabled=true;
        player.abilityManager.gameObject.SetActive(true);
        if(manager.enemy!=null){
            manager.enemy.statesManager.AddState(penalization);
        }

    }
}
