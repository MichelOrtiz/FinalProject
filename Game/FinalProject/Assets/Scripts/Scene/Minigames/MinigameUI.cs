using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    public static MinigameUI instance;
    public GameObject background;
    public GameObject canvas;
    private GameObject currentMinigame;
    public TimerBar timerBar;

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
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
//inventory.instance.AddMoney(int i);