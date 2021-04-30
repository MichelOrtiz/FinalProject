using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    public float maxTime = 100;
    public float currentTime;
    public TimerBar timerBar;
    private bool timePassing = false;
    [SerializeField]private bool losingTime;

    // Start is called before the first frame update
    void Start()
    {
        timerBar.SetMaxTime(maxTime);
        StartCoroutine(TimePassing(1f, 0.05f));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Change to timerBar
    public IEnumerator TimePassing(float timeDrowned, float drown)
    {
        yield return new WaitForSeconds (timeDrowned);
        if (!timePassing)
        {
            if (currentTime>0)
            {
            currentTime -= drown;
            timerBar.SetTime(currentTime);
            }
            if (currentTime<0)
            {
                timerBar.SetTime(0);
            }
        }
        yield return new WaitForSeconds(timeDrowned);
    }
}
