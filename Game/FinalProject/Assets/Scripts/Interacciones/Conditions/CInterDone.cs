using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Condition", menuName = "Interaction/Conditions/IsInterDone")]
public class CInterDone : InterCondition
{
    [SerializeField] Interaction interaction;
    protected override bool checkIsDone()
    {
        if(interaction.gameObject == null) return false;
        InteractionTrigger trigger = interaction.gameObject.GetComponent<InteractionTrigger>();
        return interaction == trigger.lastInter;
    }
}
