using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Condition", menuName ="Interaction/Conditions/HasMoneyAmount")]
public class CHasMoney : InterCondition
{
    [SerializeField] int amount;
    protected override bool checkIsDone()
    {
        return Inventory.instance.GetMoney() >= amount;
    }
}
