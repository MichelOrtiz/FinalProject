using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigame : MonoBehaviour{
    Overlord overlord;
    //public string name;
    [SerializeField] private bool isUI;
    [SerializeField] private GameObject minigameObject;
    [SerializeField] private int sceneIndex;
    public bool MinigameCompleted;
    [SerializeField] private int rewardMoney;


    [SerializeField] private bool hasTime;
    [SerializeField] private float time;
    private TimerBar timerBar;
    private float currentTime;

    private MasterMinigame minigame;


    public virtual void StartMinigame(){
        
        if(isUI){
            MinigameUI.instance.recieveMinigame(minigameObject);
            
        }else{
            SceneManager.LoadScene(sceneIndex);
        }

        minigame = ScenesManagers.FindObjectOfType<MasterMinigame>();
        if (minigame != null)
        {
            minigame.WinMinigameHandler += minigame_WinMinigame;
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
        if(hasTime){
            if(currentTime<=0){
                currentTime = time;
                EndMinigame();
            }else{
                currentTime -= Time.deltaTime;
                timerBar.SetTime(currentTime);
            }
        } 
    }

    public virtual void EndMinigame(){
        if(isUI){
            MinigameUI.instance.endMinigame();
        }else{
            
        }
        Destroy(gameObject);
    }

    protected virtual void minigame_WinMinigame()
    {
        Debug.Log("MinigameCompleted");
        MinigameCompleted = true;
        Inventory.instance.AddMoney(rewardMoney);
        EndMinigame();
    }
}
