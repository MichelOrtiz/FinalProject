using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        if(isUI)
        {
            MinigameUI.instance.RecieveMinigame(minigameObject);
            Pause.PauseGame();
        }else{
            SceneManager.LoadScene(sceneIndex);
        }

        MasterMinigame = ScenesManagers.FindObjectOfType<MasterMinigame>();
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
        if(hasTime){
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
        if(isUI)
        {
            MinigameUI.instance.EndMinigame(endWaitTime);
            Pause.ResumeGame();
        }else{
            
        }
        if (isCompleted)
        {
            Debug.Log("Minigame <<" + minigameObject + ">> completed!");
            Inventory.instance.AddMoney(rewardMoney);
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
