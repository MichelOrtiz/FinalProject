using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Condition", menuName = "Interaction/Conditions/Match Answer")]
public class CAnsQs : InterCondition
{
    [SerializeField] InterQuestion interaction;
    [SerializeField] string matchAnswer;
    protected override bool checkIsDone()
    {
        foreach(Answer ans in interaction.question.answers){
            if(ans.text.Equals(matchAnswer)){
                Debug.Log("Found matching answer");
                return ans.isChosed;
            }
        }
        return false;
    }
}
