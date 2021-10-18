using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/Inteface")]
public class InterUI : Interaction
{
    [SerializeField]protected GameObject UIElement;
    public override void DoInteraction()
    {
        if(condition!=null){
            if(condition.isDone){
                OpenUIElement();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            OpenUIElement();
        }
    }
    protected virtual void OpenUIElement(){
        GameObject x = Instantiate(UIElement);
        x.GetComponent<InteractionUI>().currentInteraction = this;
    }
}
