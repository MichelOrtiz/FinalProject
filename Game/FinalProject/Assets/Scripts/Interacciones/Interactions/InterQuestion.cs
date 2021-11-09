using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/Question")]
public class InterQuestion : Interaction
{
    public Question question;
    [SerializeField] GameObject QuestionUI;

    public override void DoInteraction()
    {
        if(condition != null){
            if(condition.isDone){
                OpenUI();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            OpenUI();
        }
    }
    void OpenUI(){
        GameObject x = Instantiate(QuestionUI);
        QuestionUI questionUI = x.GetComponent<QuestionUI>();
        questionUI.currentInteraction = this;
        questionUI.SetQuestion(question);
    }
    public override void RestardCondition(){
        base.RestardCondition();
        foreach(Answer ans in question.answers){
            ans.isChosed = false;
        }
    }
}
