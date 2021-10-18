using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnsSlot : MonoBehaviour
{
    Answer answer;
    [SerializeField] TextMeshProUGUI ans_Txt;
    [SerializeField] Button button;
    public delegate void OnButtonPressed();
    public OnButtonPressed onButtonPressed;
    // Start is called before the first frame update
    public void SetAnswer(Answer ans){
        answer = ans;
        UpdateUI();
    }
    void UpdateUI(){
        ans_Txt.text = answer.text;
        button.onClick.AddListener(OnClickButton);
    }
    public void OnClickButton(){
        answer.isChosed = true;
        onButtonPressed?.Invoke();
    } 
}
