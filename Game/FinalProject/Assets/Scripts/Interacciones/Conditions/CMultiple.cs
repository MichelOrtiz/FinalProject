using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Condition", menuName = "Interaction/Conditions/ManyConditions")]
public class CMultiple : InterCondition
{
    [SerializeField] List<InterCondition> conditions;
    bool isClear = false;
    protected override bool checkIsDone()
    {
        isClear = true;
        foreach(InterCondition condition in conditions){
            if(!condition.isDone){
                isClear = false;
            }
        }
        return isClear;
    }
    public override void RestardValues()
    {
        isClear = false;
        foreach(InterCondition con in conditions){
            con.RestardValues();
        }
    }
}
