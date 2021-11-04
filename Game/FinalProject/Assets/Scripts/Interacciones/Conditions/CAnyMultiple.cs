using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Condition", menuName = "Interaction/Conditions/AnyOfManyConditions")]
public class CAnyMultiple : InterCondition
{
    [SerializeField] List<InterCondition> conditions;
    bool isClear = false;
    protected override bool checkIsDone()
    {
        isClear = false;
        foreach(InterCondition condition in conditions){
            if(condition.isDone){
                isClear = true;
            }
        }
        return isClear;
    }
    public override void RestardValues(GameObject gameObject)
    {
        isClear = false;
        foreach(InterCondition con in conditions){
            con.RestardValues(gameObject);
        }
    }
}
