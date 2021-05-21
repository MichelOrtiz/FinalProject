using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBStageFight : BossFight
{
    [SerializeField] private List<GameObject> bossBehaviours;
    private GameObject currentBoss;
    [SerializeField] private Vector2 startBossEntityPosition;
    private Vector2 currentPos;

    private IBossFinishedBehaviour bossBehaviour;


    new void Start()
    {
        indexStage = 0;
        currentStage=stages[indexStage];
        currentStage.Generate();
        currentPos = startBossEntityPosition;
        
        UpdateCurrentBoss();
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
            UpdateCurrentBoss();
        }
        else{
            Debug.Log("Lo hiciste ganaste!!!1");
            currentStage.Destroy();
            isCleared=true;
            //give ability
            //AbilityManager.instance.AddAbility(reward);
        }
    }

    void UpdateCurrentBoss()
    {
        currentBoss = currentStage.GenerateSingle(bossBehaviours[indexStage], currentPos);
        bossBehaviour = currentBoss.GetComponentInChildren<IBossFinishedBehaviour>();

        if (bossBehaviour != null)
        {
            bossBehaviour.FinishedHandler += bossBehaviour_FinishedAttack;
        }
    }

    void bossBehaviour_FinishedAttack(Vector2 lastPos)
    {
        currentPos = lastPos;
        NextStage();
    }
}
