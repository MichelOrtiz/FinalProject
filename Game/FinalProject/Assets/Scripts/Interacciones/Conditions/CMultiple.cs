using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Condition", menuName = "Interaction/Conditions/ManyConditions")]
public class CMultiple : InterCondition
{
    [SerializeField] List<InterCondition> conditions;
    protected override bool checkIsDone()
    {
        foreach(InterCondition condition in conditions){
            if(!condition.isDone){
                return false;
            }
        }
        return true;
    }
}
