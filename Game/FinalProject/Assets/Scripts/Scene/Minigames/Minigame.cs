using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Minigame : MonoBehaviour{
    Overlord overlord;
    //public string name;
    [SerializeField] private bool isUI;
    [SerializeField] private GameObject minigameObject;
    //public GameObject MinigameObject { get => minigameObject; }
    [SerializeField] private int sceneIndex;
    public bool MinigameCompleted;
    [SerializeField] private int rewardMoney;


    [SerializeField] private bool hasTime;
    [SerializeField] private float time;
    


    private TimerBar timerBar;
    private float currentTime;
    [SerializeField] private float endWaitTime;
    private bool ended;

    public MasterMinigame MasterMinigame { get; set; }

    public delegate void MinigameEnded();
    public event MinigameEnded MinigameEndedHandler;

    /// <summary>
    /// Called when the minigame ends, either won or not
    /// </summary>
    protected virtual void OnMinigameEnded()
    {
        MinigameEndedHandler?.Invoke();
    }


    public virtual void StartMinigame(){
        GameObject minigame;
        if(isUI)
        {
            minigame = MinigameUI.instance.RecieveMinigame(minigameObject);
            Pause.PauseGame();
            PlayerManager.instance.SetEnabledPlayer(false);
            MasterMinigame = minigame.GetComponentInChildren<MasterMinigame>();
        }else{
            SceneController.instance.LoadScene(sceneIndex);
            MasterMinigame = ScenesManagers.FindObjectOfType<MasterMinigame>();
        }
        
        if (MasterMinigame == null)
        {
            Debug.Log("mrda");
        }
    }

    void Start()
    {
        overlord = (Overlord)PlayerManager.instance.abilityManager.abilities.Find(a => a.abilityName == Ability.Abilities.Overlord);

        timerBar = MinigameUI.instance.timerBar;
        if (overlord.IsOverlording && overlord.isUnlocked)
        {
            time += time * 0.5f;
        }
        timerBar.SetMaxTime(time);

        currentTime = time;
        MinigameCompleted = false;
    }

    void Update()
    {
        if(hasTime && !ended){
            if(currentTime<=0){
                currentTime = time;
                EndMinigame(false);
            }else{
                currentTime -= Time.unscaledDeltaTime;
                timerBar.SetTime(currentTime);
            }
        }
    }

    public virtual void EndMinigame(bool isCompleted)
    {
        MinigameCompleted = isCompleted;
        ended = true;
        if(isUI)
        {
            MinigameUI.instance?.EndMinigame(endWaitTime);
            StartCoroutine(EndCoroutine(endWaitTime));
        }
        else
        {
            // for now
            EndMinigameForGood();
        }
    }

    IEnumerator EndCoroutine(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        EndMinigameForGood();
    }

    void EndMinigameForGood()
    {
        
        if (MinigameCompleted)
        {
            Debug.Log("Minigame <<" + minigameObject + ">> completed!");
            Inventory.instance.AddMoney(rewardMoney);
        }
        if (isUI)
        {
            MinigameUI.instance.DestroyMinigame();
            Pause.ResumeGame();
            PlayerManager.instance.SetEnabledPlayer(true);
        }
        OnMinigameEnded();
        Destroy(gameObject);
    }
    

    /*public void WinMinigame()
    {
        MinigameCompleted = true;
        Inventory.instance.AddMoney(rewardMoney);
        EndMinigame();
    }*/
}
