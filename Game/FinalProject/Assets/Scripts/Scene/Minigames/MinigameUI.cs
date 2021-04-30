using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    public static MinigameUI instance;
    public GameObject background;
    public GameObject canvas;
    private GameObject currentMinigame;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void endMinigame(){
        currentMinigame.SetActive(false);
        background.SetActive(false);
        Destroy(currentMinigame);
    }


    public void recieveMinigame(GameObject minigame){
        currentMinigame = Instantiate(minigame, new Vector3(0, 0, 90), Quaternion.identity);
        currentMinigame.transform.SetParent(canvas.transform, false);
        background.SetActive(true);
        timerBar.SetMaxTime(maxTime);
        StartCoroutine(TimePassing(1f, 0.05f));
    }
    
    public float maxTime = 10;
    public float currentTime;
    public TimerBar timerBar;
    private bool timePassing = false;
    [SerializeField]private bool losingTime;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Change to timerBar
    public IEnumerator TimePassing(float timePassed, float time)
    {
        yield return new WaitForSeconds (timePassed);
        if (!timePassing)
        {
            if (currentTime>0)
            {
            currentTime -= time;
            timerBar.SetTime(currentTime);
            }
            if (currentTime<0)
            {
                timerBar.SetTime(0);
                endMinigame();
                Debug.Log("el minijuego se acabo");
            }
        }
        yield return new WaitForSeconds(timePassed);
    }
}

//inventory.instance.AddMoney(int i);