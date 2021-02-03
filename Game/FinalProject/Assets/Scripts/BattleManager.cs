using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public DialogueTrigger startDialogue;
    public void Start()
    {
        startDialogue.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
