using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName="New MultipleStates", menuName = "States/new MultipleStates")]
public class MultipleStates : State
{
    [SerializeField] private List<State> states;
    private byte index;
    public override void StartAffect(StatesManager newManager)
    {
        
        //manager.RemoveEmotes();
        base.StartAffect(newManager);


        //var addedStates = new List<State>();

        if (states != null && states.Count > 0)
        {
            foreach (var state in states)
            {
                var st = manager.hostEntity.statesManager.AddState(state);
                st.StoppedAffect += state_StoppedAffect;
            }
        }
    }

    public override void Affect()
    {
        /*if (currentTime > duration)
        {
            //StopAffect();
        }
        else
        {
            //currentTime += Time.deltaTime;
        }*/
    }

    public override void StopAffect()
    {
//
        base.StopAffect();
    }

    void state_StoppedAffect()
    {
        // Stops this (multipleState) running after all its states have ended
        if (++index >= states.Count)
        {
            StopAffect();
        }
    }
}