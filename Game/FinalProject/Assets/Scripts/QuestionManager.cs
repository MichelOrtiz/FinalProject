using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    #region Singleton
    public static QuestionManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion
    public GameObject answerPrefab;
    public Animator animator;
    public Text questionText;
    public List<string> answers = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        answers = new List<string>();
    }

    public void StartQuestion(Question q)
    {
        animator.SetBool("IsOpen", true);
        answers.Clear();
        foreach(string answer in q.respuesta)
        {
            answers.Add(answer);
        }
    }
    public void EndQuestion()
    {
        animator.SetBool("IsOpen", false);
    }
    public void GenerateAnswerUI(Vector3 place)
    {
        Instantiate(answerPrefab, place, Quaternion.identity);
    }
}
