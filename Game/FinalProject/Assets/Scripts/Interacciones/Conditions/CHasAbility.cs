using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Condition", menuName ="Interaction/Conditions/HasAbility")]
public class CHasAbility : InterCondition
{
    [SerializeField] private Ability.Abilities ability;
    protected override bool checkIsDone()
    {
        return PlayerManager.instance.abilityManager.IsUnlocked(ability);
    }
}
