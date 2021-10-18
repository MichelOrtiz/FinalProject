using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/Dialogue")]
public class InterDialogue : Interaction
{
    [SerializeField] GameObject UI;
    [SerializeField] Dialogue dialogue;
    public override void DoInteraction()
    {
        if(condition != null){
            if(condition.isDone){
                OpenDialogue();
                return;
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            OpenDialogue();
        }
    }
    void OpenDialogue(){
        GameObject x = Instantiate(UI);
        DialogueManager dialogueManager = x.GetComponent<DialogueManager>();
        dialogueManager.currentInteraction = this;
        dialogueManager.StartDialogue(dialogue);
    }
}
