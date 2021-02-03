using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    public static DialogueManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion
    public enum DialogState { Ready,Start,End};
    public DialogState state;
    private Queue<string> sentences = new Queue<string>();
    public GameObject panel;
    public Animator animator;
    public Text nameText;
    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        state = DialogState.Ready;
    }

    public void StartDialogue (Dialogue d)
    {
        state = DialogState.Start;
        animator.SetBool("IsOpen", true);
        nameText.text = d.name;
        sentences.Clear();
        foreach(string sentence in d.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentences();
    }
    public void DisplayNextSentences()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        state = DialogState.End;
    }
}
