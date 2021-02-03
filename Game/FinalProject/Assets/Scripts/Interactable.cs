using UnityEngine;

public class Interactable : MonoBehaviour
{
    private DialogueTrigger dialogue;
    private bool hasInter;

    public void Start()
    {
        hasInter = false;
        dialogue = GetComponent<DialogueTrigger>();
    }

    public void Fight()
    {
        dialogue.TriggerDialogue();
        hasInter = true;
        
    }
    public void Update()
    {
        if (hasInter)
        {
            if (DialogueManager.instance.state == DialogueManager.DialogState.End)
            {
                SceneController.instance.LoadScene(1);
            }
        }
    }

}
