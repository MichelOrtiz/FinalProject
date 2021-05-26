using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigame : MonoBehaviour{
    Overlord overlord;
    public string name;
    [SerializeField] private bool isUI;
    [SerializeField] private GameObject minigameObject;
    [SerializeField] private int sceneIndex;

    [SerializeField] private bool hasTime;
    [SerializeField] private float time;
    private TimerBar timerBar;
    private float currentTime;
    

    public virtual void StartMinigame(){
        if(isUI){
            MinigameUI.instance.recieveMinigame(minigameObject);
            
        }else{
            SceneManager.LoadScene(sceneIndex);
        }
    }

    void Start()
    {
        timerBar = MinigameUI.instance.timerBar;
        if (overlord.IsOverlording)
        {
            timerBar.SetMaxTime(time + (time*.5f));
        }else
        {
            timerBar.SetMaxTime(time);
        }
        currentTime = time;
    }

    void Update()
    {
        Debug.Log(currentTime);
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
}
