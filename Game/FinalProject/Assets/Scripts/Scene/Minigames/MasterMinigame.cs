using UnityEngine;
using System;
public abstract class MasterMinigame : MonoBehaviour
{
    
    public delegate void WinMinigame();
    public event WinMinigame WinMinigameHandler;
    protected virtual void OnWinMinigame()
    {
        if (WinMinigameHandler == null)
        {
            Debug.Log("WinMinigameHandler null again smh");
        }
        Debug.Log("win");
        WinMinigameHandler?.Invoke();
    }

    public delegate void LoseMinigame();
    public event LoseMinigame LoseMinigameHandler;
    protected virtual void OnLoseMinigame()
    {
        LoseMinigameHandler?.Invoke();
        Debug.Log("loose");

    }

    protected void Start() {
        float time = FindObjectOfType<Minigame>().time;
        Invoke("AboutToEnd", time);
    }

    protected virtual void AboutToEnd(){}
}