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
        WinMinigameHandler?.Invoke();
    }

    public delegate void LoseMinigame();
    public event LoseMinigame LoseMinigameHandler;
    protected virtual void OnLoseMinigame()
    {
        LoseMinigameHandler?.Invoke();
    }
}