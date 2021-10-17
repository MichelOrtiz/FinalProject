using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/Interaction", order = 0)]
public class Interaction : ScriptableObject {
    public InterCondition condition = null;
    enum InteractionType
    {
        Dialogue,
        UI,
        Mision,
        GG,
        Condition,
        None
    }
    [SerializeField] InteractionType type = InteractionType.None;
    [SerializeField] GameObject UI;
    [SerializeField] Dialogue dialogue;
    [SerializeField] WorldState mision;
    [SerializeField] Item giveObject;
    public delegate void OnEndInteraction();
    public OnEndInteraction onEndInteraction;    
    public void DoInteraction(){
        switch(type){
            case InteractionType.Dialogue:{
                OpenDialogueBox();
                break;
            }
            case InteractionType.UI:{
                OpenUIElement();
                break;
            }
            case InteractionType.Mision:{
                GiveMision();
                onEndInteraction?.Invoke();
                break;
            }
            case InteractionType.GG:{
                GenerateAndGive();
                onEndInteraction?.Invoke();
                break;
            }
            case InteractionType.Condition:{
                DealCondition();
                onEndInteraction?.Invoke();
                break;
            }
            default:{
                Debug.Log("What am I supposed to do?");
                onEndInteraction?.Invoke();
                break;
            }
        }
    }
    void OpenDialogueBox(){
        if(FindObjectOfType<DialogueManager>() != null){
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            FindObjectOfType<DialogueManager>().currentInteraction = this;
        }else{
            Instantiate(UI);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            FindObjectOfType<DialogueManager>().currentInteraction = this;
        }
    }
    void OpenUIElement(){
        GameObject x = Instantiate(UI);
        x.GetComponent<InteractionUI>().currentInteraction = this;
    }
    void GiveMision(){
        List<WorldState> worldStates =
        SaveFilesManager.instance.currentSaveSlot.WorldStates;
        foreach (WorldState ws in worldStates)
        {
            if(ws.id == mision.id){
                Debug.Log("Ya se tenia el worldState");
                return;
            }
        }
        worldStates.Add(mision);
    }
    void GenerateAndGive(){
        Inventory.instance.Add(giveObject);
    }
    void DealCondition(){

    }
}


