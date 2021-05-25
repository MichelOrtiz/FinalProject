using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class CastleBStageFight : BossFight
{
    [SerializeField] private List<GameObject> behaviours;
    public GameObject CurrentBoss { get; set; }
    [SerializeField] private Vector2 startBossEntityPosition;
    public Vector2 startBossPosition { get => startBossEntityPosition; set => startBossEntityPosition = value; }
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
        }
        else
        {
            indexStage = 0;
        }
        currentStage.Destroy();
        currentStage=stages[indexStage];
        
        currentStage.Generate();
        UpdateCurrentBoss();
    }

    void UpdateCurrentBoss()
    {
        CurrentBoss = currentStage.GenerateSingle(behaviours[indexStage], currentPos);
        var behaviour = CurrentBoss.GetComponentInChildren<IBossFinishedBehaviour>();

        // Intended to NOT be aware of the event "Finished Attack" of Seeker, if the parent is Seeker and follows Player
        if (behaviour is CastleBSeeker c)
        {
            if (c.FollowsPlayer)
            {
                var behaviours = CurrentBoss.GetComponentsInChildren<IBossFinishedBehaviour>();
                
                foreach (var b in behaviours)
                {
                    // search for children only
                    if (!(b is CastleBSeeker))
                    {
                        behaviour = b;
                    }
                }
            }
        }

        bossBehaviour = behaviour;

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

    public void DestroyCurrentStage()
    {
        currentStage.Destroy();
    }
}
