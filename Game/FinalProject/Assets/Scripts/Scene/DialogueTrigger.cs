using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private float intRadius;
    public Dialogue dialog;
    public void TriggerDialogue()
    {    
        DialogueManager.instance.StartDialogue(dialog);
        Debug.Log("Trigger " + dialog.name);
    }
    private void Update() {
        //PlayerManager.instance.inputs
        if(Input.GetKeyDown(KeyCode.E)){
            float distance = Vector2.Distance(this.transform.position, PlayerManager.instance.GetPosition());
            if(distance <= intRadius)TriggerDialogue();
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, intRadius);
    }
}
