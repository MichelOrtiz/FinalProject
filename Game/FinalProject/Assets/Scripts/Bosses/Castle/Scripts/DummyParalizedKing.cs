using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyParalizedKing : Entity
{
    [SerializeField] private State paralizedState;
    private BossFight bossFight;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        bossFight = GetComponent<BossFight>();

        statesManager.hostEntity = this;
        statesManager.AddState(paralizedState);

    }

    // Update is called once per frame
    new void Update()
    {
        if (bossFight != null)
        {
            if (statesManager.currentStates.Count == 0)
            {
                bossFight.NextStage();
            }
        }
    }
}
