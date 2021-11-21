using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class PopUpUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI message;
    public Button closeButton;
    public Button exitButton;

    private PopUp popUp = new PopUp();
   //public static PopUpUI Instance { get; private set; }
    public Action closedPopUp;

    public bool showing;

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
            Pause.blocked = true;
        }
        
        title.text = popUp.Title;
        message.text = popUp.Message;
        popUp = new PopUp();

        canvas.SetActive(true);
        showing = true;
    }

    public void Hide()
    {
        canvas.SetActive(false);
        Pause.ResumeGame();

        showing = false;
        Pause.blocked = false;

        closedPopUp?.Invoke();
    }
}
