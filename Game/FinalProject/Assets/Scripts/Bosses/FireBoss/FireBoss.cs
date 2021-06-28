using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : BossFight
{
    [SerializeField] private float intervalBwtStages;
    private float curTimeBtwStages;

    [SerializeField] private float transitionTime;
    private float curTransitionTime;
    
    // Hits until the boss gets beaten
    public int maxHits;
    private int currentHits;
    private int currentStageIndex;
    private enum Zone
    {
        First,
        Second,
        Third
    }

    private Zone currentZone;
    // Start is called before the first frame update
    new void Start()
    {
        currentStageIndex = 0;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if (!isCleared)
        {
            if (curTimeBtwStages >= intervalBwtStages)
            {
                // Change attack
                if (curTransitionTime > transitionTime)
                {
                    int lastIndex = currentStageIndex;
                    do
                    {
                        currentStageIndex = RandomGenerator.NewRandom(0, stages.Count-1);
                    }
                    while (currentStageIndex == lastIndex);
                    
                    ChangeToStage(stages[currentStageIndex]);
                    
                    curTimeBtwStages = 0;
                    curTransitionTime = 0;
                }
                else
                {
                    curTransitionTime += Time.deltaTime;
                }
                
            }
            else
            {
                
                curTimeBtwStages += Time.deltaTime;
            }
        }
        
        base.Update();        
    }
    
    void ChangeToStage(Stage stage)
    {
        currentStage.Destroy();
        currentStage = stage;
        stage.Generate();
    }

    public override void EndBattle()
    {
        base.EndBattle();
        stages.Clear();
    }

    public void AddHit()
    {
        if (currentHits < maxHits)
        {
            currentHits++;
        }
        else
        {
            EndBattle();
        }
    }
}
