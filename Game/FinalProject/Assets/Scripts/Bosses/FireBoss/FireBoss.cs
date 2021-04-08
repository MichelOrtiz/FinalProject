using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : BossFight
{
    [SerializeField] private float intervalBwtStages;
    
    // Hits until the boss gets beaten
    public int maxHits;
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

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (stages.Count > 0)
        {
            switch (other.tag)
            {
                case "fireboss-a1":
                    ChangeToStage(stages[0]);
                    break;
                case "fireboss-a2":
                    ChangeToStage(stages[1]);
                    break;
                case "fireboss-a3":
                    ChangeToStage(stages[2]);
                    break;
            }
        }
    }


    /*public override void NextStage()
    {

    }*/

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
}
