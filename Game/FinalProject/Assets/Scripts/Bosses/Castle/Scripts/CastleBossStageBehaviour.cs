using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBossStageBehaviour : BossFight
{
    [SerializeField] private List<GameObject> bossBehaviours;
    [SerializeField] private Vector2 startBossEntityPosition;
    private GameObject currentBehaviour;
    private Vector2 currentPos;

    new void Start()
    {
        indexStage = 0;
        currentStage=stages[indexStage];
        currentStage.Generate();
        currentPos = startBossEntityPosition;
        
        UpdateCurrentBehaviour();
    }

    new void Update()
    {
        base.Update();
    }

    public override void NextStage()
    {
        if(indexStage<stages.Count-1)
        {

            indexStage++;

            currentStage.Destroy();
            currentStage=stages[indexStage];
            
            currentStage.Generate();
            UpdateCurrentBehaviour();
        }
        else{
            Debug.Log("Lo hiciste ganaste!!!1");
            currentStage.Destroy();
            isCleared=true;
            //give ability
            //AbilityManager.instance.AddAbility(reward);
        }
    }

    void UpdateCurrentBehaviour()
    {
        currentBehaviour = currentStage.GenerateSingle(bossBehaviours[indexStage], currentPos);
        //caveBossBehaviour = currentBoss.GetComponent<CaveBossBehaviour>();

        /*if (caveBossBehaviour != null)
        {
            caveBossBehaviour.FinishedHandler += caveBoss_FinishedBehaviour;
        }*/
    }

    void castleBoss_FinishedFight(Vector2 lastPos)
    {
        Debug.Log("current pos = " + lastPos);

        currentPos = lastPos;
        NextStage();
    }
}
