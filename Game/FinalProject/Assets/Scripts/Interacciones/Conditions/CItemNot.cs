using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Condition", menuName = "Interaction/Conditions/!HasItem")]
public class CItemNot : InterCondition
{
    [SerializeField] Item hasItem;
    protected override bool checkIsDone()
    {
        if(Inventory.instance.items.Contains(hasItem)){
            return false;
        }else{
            return true;
        }
    }
}
