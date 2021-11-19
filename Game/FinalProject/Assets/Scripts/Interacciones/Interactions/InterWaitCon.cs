using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/WaitClearCondition")]
public class InterWaitCon : Interaction
{
    [SerializeField] Interaction doThis;
    public override void DoInteraction()
    {
        doThis.onEndInteraction -= this.onEndInteraction;
        doThis.onEndInteraction += this.onEndInteraction;
        doThis.gameObject = this.gameObject;

        if(condition != null){
            gameObject.GetComponent<InteractionTrigger>().updateForInteractions -= CheckIsDone;
            gameObject.GetComponent<InteractionTrigger>().updateForInteractions += CheckIsDone;
            
        }else{
            doThis.DoInteraction();
        }
    }
    public void CheckIsDone(){
        if(condition.isDone){
            Debug.Log("Condition cleared");
            gameObject.GetComponent<InteractionTrigger>().updateForInteractions -= CheckIsDone;
            gameObject.GetComponent<InteractionTrigger>().delayMethods -= DelayThis;
            gameObject.GetComponent<InteractionTrigger>().delayMethods += DelayThis;
            gameObject.GetComponent<InteractionTrigger>().Invoke("DoDelayMethods",2f);
        }
    }
    public void DelayThis(){
        doThis.DoInteraction();
        gameObject.GetComponent<InteractionTrigger>().delayMethods -= DelayThis;
    }
}
