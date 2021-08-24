using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesManager : MonoBehaviour
{
    public Entity hostEntity;
    public Entity enemy;
    public List<State> currentStates = new List<State>();
    public delegate void StatusCheck();
    public StatusCheck statusCheck;   
    private void Start() {
        hostEntity=gameObject.GetComponent<Entity>();
    } 
    void Update()
    {
        if(statusCheck!=null){
            statusCheck.Invoke();
        }
    }

    /// <summary>
    /// Adds and starts a new state to the entity manager
    /// </summary>
    /// <param name="newState">State to add</param>
    /// <returns>The instantiated state (could be different from the original)</returns>
    public State AddState(State newState){
        if(newState != null){
            if (!currentStates.Contains(newState))
            {
                if (newState.onEffect)
                {
                    Debug.Log(gameObject + " manager cloned " + newState);
                    newState = Instantiate(newState);
                }


                newState.StartAffect(this);
                currentStates.Add(newState);

                return newState;
            }
        }

        return null;
    }
    public State AddState(State newState, Entity newEnemy){
        if(newState != null){
            if (!currentStates.Contains(newState))
            {
                if (newState.onEffect)
                {
                    Debug.Log(gameObject + " manager cloned " + newState);
                    newState = Instantiate(newState);
                }

                enemy=newEnemy;
                newState.StartAffect(this);
                currentStates.Add(newState);

                return newState;

            }
        }
        return null;
    }
    public void RemoveState(State state){
        if(currentStates.Contains(state)){
            //state.StopAffect();
            currentStates.Remove(state);
        }
    }
}
