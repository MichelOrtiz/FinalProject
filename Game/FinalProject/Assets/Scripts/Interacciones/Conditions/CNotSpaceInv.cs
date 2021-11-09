using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Condition", menuName ="Interaction/Conditions/!HasInvSpaceAmount")]
public class CNotSpaceInv : InterCondition
{
    [SerializeField] int amount;
    protected override bool checkIsDone()
    {
        int spaceLeft = Inventory.instance.capacidad - Inventory.instance.items.Count;
        return spaceLeft < amount;
    }
}

