using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBBossFight : BossFight
{
    private MBMinigameManager minigameManager;
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    new void Update()
    {
        if (indexStage == 0)
        {
            if (FindObjectOfType<MBPartsHandler>() && FindObjectOfType<MBPartsHandler>().IsAssembled)
            {
                NextStage();
            }
        }
        else if(minigameManager == null && !isCleared)
        {
            try
            {
                minigameManager = FindObjectOfType<MBMinigameManager>();   
                minigameManager.AllMinigamesCompletedHandler += minigameManager_AllMinigamesCompleted; 
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("Minigame manager not found in scene (maybe not a problem ;) )");
            }
            
        }
    }

    void minigameManager_AllMinigamesCompleted()
    {
        EntityDestroyFx.Instance.StartDestroyFx(minigameManager.currentHost);
        EndBattle();
    }

}