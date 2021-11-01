using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FinalProject.Assets.Scripts.Utils.Sound;

public class DialogueManager : InteractionUI
{
    public enum UIStatus{Ready,Busy}
    public UIStatus status;
    private Queue<string> sentences;
    public Animator animator;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private void Awake() {
        sentences = new Queue<string>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //StartDialogue(new Dialogue());
    }

    public void StartDialogue (Dialogue dialogue)
    {
        Minimap.MinimapWindow.instance.Hide();
        nameText.text = dialogue.name;
        sentences.Clear();
        animator.SetBool("IsOpen", true);
        foreach(string sentence in dialogue.sentences)
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
        StartCoroutine(AudioManager.instance.SoundSpeakingLoop());
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(0.01f);
        }
        AudioManager.instance.isSpeaking = false;
    }
    
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Minimap.MinimapWindow.instance.Show();
        Exit();
    }
}
