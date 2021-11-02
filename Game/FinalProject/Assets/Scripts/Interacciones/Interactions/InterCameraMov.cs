using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/MoveCamera")]
public class InterCameraMov : Interaction
{
    CameraFollow cam = CameraFollow.instance;
    [SerializeField] InterCondition whenEnd;
    [SerializeField] float duration;
    float countdown;
    [SerializeField] Vector3 target;
    [SerializeField] float zoom;
    public override void DoInteraction()
    {
        if(condition != null){
            if(condition.isDone){
                MoveCamera();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            MoveCamera();
        }
    }
    void MoveCamera(){
        cam = CameraFollow.instance;
        cam.cinematicTarget = target;
        cam.isCinematic = true;
        countdown = duration;
        gameObject.GetComponent<InteractionTrigger>().updateForInteractions += Countdown;
        onEndInteraction?.Invoke();
    }
    void Countdown(){
        if(countdown <= 0){
            cam.cinematicTarget = PlayerManager.instance.GetPosition();
            cam.isCinematic = false;
            gameObject.GetComponent<InteractionTrigger>().updateForInteractions -= Countdown;
            Debug.Log("Coutdown ended");
        }else if(whenEnd == null){
            countdown -= Time.deltaTime;
        }
        if(!gameObject.GetComponent<InteractionTrigger>().busy){
            countdown = 0;
        }
        if(whenEnd != null && whenEnd.isDone){
            countdown = 0;
        }
    }
    public override void RestardCondition()
    {
        base.RestardCondition();
        if(whenEnd != null){
            whenEnd.RestardValues(gameObject);
        }
    }
}
