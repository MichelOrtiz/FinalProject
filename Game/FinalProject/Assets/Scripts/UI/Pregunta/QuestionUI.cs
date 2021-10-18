using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionUI : InteractionUI
{
    // Start is called before the first frame update
    Question question;
    [SerializeField] GameObject ansPrefab;
    [SerializeField] Transform ansHolder;
    protected override void Start()
    {
       base.Start(); 
    }
    public void SetQuestion(Question newQuestion){
        question = newQuestion;
    }
    void UpdateUI(){
        foreach(Answer ans in question.answers){
            GameObject x = Instantiate(ansPrefab);
            x.transform.SetParent(ansHolder,false);
            AnsSlot slot = x.GetComponent<AnsSlot>();
            slot.SetAnswer(ans);
            slot.onButtonPressed += Exit;
        }
    }
}
