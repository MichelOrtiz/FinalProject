using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBBossFight : BossFight
{
    private MBMinigameManager minigameManager;

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
                minigameManager.AllMinigamesCompleted += minigameManager_AllMinigamesCompleted; 
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("Minigame manager not found in scene (maybe not a problem ;) )");
            }
            
        }
        // delete later
        /*if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            NextStage();
        }
        */
        //base.Update();
    }

    void minigameManager_AllMinigamesCompleted()
    {
        //EntityDestroyFx.Instance.StartDestroyFx(minigameManager.currentHost);
        EndBattle();
    }

}