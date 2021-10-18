using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionUI : InteractionUI
{
    // Start is called before the first frame update
    Question question;
    [SerializeField] TextMeshProUGUI texto_pregunta;
    [SerializeField] GameObject ansPrefab;
    [SerializeField] Transform ansHolder;
    protected override void Start()
    {
       base.Start(); 
    }
    public void SetQuestion(Question newQuestion){
        question = newQuestion;
        UpdateUI();
    }
    void UpdateUI(){
        texto_pregunta.text = question.text;
        foreach(Answer ans in question.answers){
            ans.isChosed = false;
            GameObject x = Instantiate(ansPrefab);
            x.transform.SetParent(ansHolder,false);
            AnsSlot slot = x.GetComponent<AnsSlot>();
            slot.SetAnswer(ans);
            slot.onButtonPressed += Exit;
        }
    }
}
