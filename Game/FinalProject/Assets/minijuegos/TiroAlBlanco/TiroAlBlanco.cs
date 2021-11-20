using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroAlBlanco : MasterMinigame
{
    Overlord overlord;
    [SerializeField] GameObject spa;
    [SerializeField] private byte scoreToWin;
    
    [SerializeField] private float time;
    private float currentTime;
    void Start()
    {
        overlord = (Overlord)PlayerManager.instance.abilityManager.abilities.Find(a => a.abilityName == Ability.Abilities.Overlord);
        if (overlord.IsOverlording && overlord.isUnlocked)
        {
            time += time * 0.5f;
        }

        currentTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreController.score >= scoreToWin)
        {
            //spa.SetActive(false);

            OnWinMinigame();
        }
        if(currentTime<=0){
            currentTime = time;
            spa.SetActive(false);
        }else{
            currentTime -= Time.unscaledDeltaTime;
        }
    }
}
