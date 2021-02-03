using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTrigger : MonoBehaviour
{
    public Question pregunta;
    public void TriggerQuestion()
    {
        QuestionManager.instance.StartQuestion(pregunta);
    }
}
