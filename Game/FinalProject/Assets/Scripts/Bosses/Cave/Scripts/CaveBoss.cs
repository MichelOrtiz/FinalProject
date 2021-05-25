using UnityEngine;
using System.Collections.Generic;
public class CaveBoss : BossFight
{
    [SerializeField] private List<ThreadBreaker> threadBreakers;
    [SerializeField] private List<GameObject> bossBehaviours;
    private GameObject currentBoss;
    [SerializeField] private Vector2 startBossEntityPosition;

    private byte scissorsActivated;    
    private List<Entity> currentEntities;
    private Vector2 currentPos;

    private CaveBossBehaviour caveBossBehaviour;

    private byte timesNextStageCalled;

    void Awake()
    {
        foreach (var scissor in threadBreakers)
        {
            scissor.ActivatedHandler += threadBreaker_Activated;
        }
    }


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
        caveBossBehaviour = currentBoss.GetComponent<CaveBossBehaviour>();

        if (caveBossBehaviour != null)
        {
            //caveBossBehaviour.FinishedHandler += caveBoss_FinishedBehaviour;
        }
    }

    void threadBreaker_Activated()
    {
        if (scissorsActivated < threadBreakers.Count-1)
        {
            scissorsActivated++;
        }
        else
        {
            foreach (var scissor in threadBreakers)
            {
                Destroy(scissor.gameObject);
            }
            NextStage();
        }
    }

    void caveBoss_FinishedBehaviour(Vector2 lastPos)
    {
        Debug.Log("current pos = " + lastPos);

        currentPos = lastPos;
        NextStage();
    }
    
}