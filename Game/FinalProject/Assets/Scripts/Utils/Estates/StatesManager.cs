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

    public void AddState(State newState){
        if(newState != null){
            if (!currentStates.Contains(newState))
            {
                newState.StartAffect(this);
                currentStates.Add(newState);
            }
        }
    }
    public void AddState(State newState, Entity newEnemy){
        if(newState != null){
            if (!currentStates.Contains(newState))
            {
                enemy=newEnemy;
                newState.StartAffect(this);
                currentStates.Add(newState);
            }
        }
    }
    public void RemoveState(State state){
        if(currentStates.Contains(state)){
            //state.StopAffect();
            currentStates.Remove(state);
        }
    }
}
