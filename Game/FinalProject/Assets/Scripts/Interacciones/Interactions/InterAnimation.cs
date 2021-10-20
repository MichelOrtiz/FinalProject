using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/PlayAnimationClip")]
public class InterAnimation : Interaction
{
    Animator thingToAnimate;
    [SerializeField] AnimationClip animationClip;
    public override void DoInteraction()
    {
        if(condition != null){
            if(condition.isDone){
                TriggerAnimation();
                return;
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            TriggerAnimation();
        }
    }

    void TriggerAnimation(){
        thingToAnimate =  gameObject.GetComponent<Animator>();
        AnimationEvent evt = new AnimationEvent();
        evt.time = animationClip.length - 0.1f;
        evt.functionName = "NextInteraction";
        
        animationClip.AddEvent(evt);
        Debug.Log("Playing: " + animationClip.name);
        thingToAnimate.Play(animationClip.name);
        
    }
}
