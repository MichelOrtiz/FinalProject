using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : BossFight
{
    [SerializeField] private float intervalBwtStages;
    
    // Hits until the boss gets beaten
    public int maxHits;
    private int currentHits;
    private float timeBtwStages;
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
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if (!isCleared)
        {
            if (timeBtwStages >= intervalBwtStages)
            {
                int randomStage = RandomGenerator.NewRandom(0, stages.Count-1);
                ChangeToStage(stages[randomStage]);
                
                timeBtwStages = 0;
            }
            else
            {
                timeBtwStages += Time.deltaTime;
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
