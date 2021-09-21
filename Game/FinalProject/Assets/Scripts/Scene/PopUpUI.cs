using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class PopUpUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private Button closeButton;

    private PopUp popUp = new PopUp();
   //public static PopUpUI Instance { get; private set; }
    public Action closedPopUp;

    void Awake()
    {
        /*if (Instance != null)
        {
            return;
        }
        Instance = this;*/

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Hide);
    }

    public PopUpUI SetTitle(string title)
    {
        popUp.Title = title;
        return this;
        //return Instance;
    }

    public PopUpUI SetMessage(string message)
    {
        popUp.Message = message;
        return this;
        //return Instance;
    }

    public void Show(bool pauseGame)
    {
        if (pauseGame)
        {
            Pause.PauseGame();
        }
        
        title.text = popUp.Title;
        message.text = popUp.Message;
        popUp = new PopUp();

        canvas.SetActive(true);
    }

    public void Hide()
    {
        canvas.SetActive(false);
        Pause.ResumeGame();
        closedPopUp?.Invoke();
    }
}
