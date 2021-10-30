using UnityEngine;
using System.Collections.Generic;
public class CaveBoss : BossFight
{
    [SerializeField] private List<ThreadBreaker> threadBreakers;
    [SerializeField] private List<GameObject> bossBehaviours;
    private GameObject currentBoss;
    [SerializeField] private Vector2 startBossEntityPosition;

    private byte scissorsActivated;    
    private Vector2 currentPos;

    private CaveBossBehaviour caveBossBehaviour;


    new void Awake()
    {
        base.Awake();
        foreach (var scissor in threadBreakers)
        {
            scissor.ActivatedHandler += threadBreaker_Activated;
        }
    }


    new void Start()
    {
        base.Start();
        indexStage = 0;
        currentStage=stages[indexStage];
        currentStage.Generate();
        currentPos = startBossEntityPosition;
        
        
        
        UpdateCurrentBoss();
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
        else
        {
            EndBattle();
        }
    }

    void UpdateCurrentBoss()
    {
        currentBoss = currentStage.GenerateSingle(bossBehaviours[indexStage], currentPos);
        caveBossBehaviour = currentBoss.GetComponent<CaveBossBehaviour>();

        if (caveBossBehaviour != null)
        {
            caveBossBehaviour.FinishedHandler += caveBoss_FinishedBehaviour;
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
        currentPos = lastPos;
        NextStage();
    }
    
}