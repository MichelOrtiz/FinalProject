using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName="New MultipleStates", menuName = "States/new MultipleStates")]
public class MultipleStates : State
{
    [SerializeField] private List<State> states;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        if (states != null && states.Count > 0)
        {
            foreach (var state in states)
            {
                manager.hostEntity.statesManager.AddState(state);
            }
        }
    }

    public override void Affect()
    {
        if (currentTime > duration)
        {
            StopAffect();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    public override void StopAffect()
    {

        base.StopAffect();
    }
}