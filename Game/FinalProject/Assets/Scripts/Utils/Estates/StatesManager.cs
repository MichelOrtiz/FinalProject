using System;
using System.Collections.Generic;
using UnityEngine;

public class StatesManager : MonoBehaviour
{
    public Entity hostEntity;
    public Entity enemy;
    public List<State> currentStates = new List<State>();
    public List<State> bannedStates = new List<State>();
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
            if (!currentStates.Contains(newState) && !bannedStates.Contains(newState))
            {
                if (newState.onEffect)
                {
                    Debug.Log(gameObject + " manager cloned " + newState);
                    newState = Instantiate(newState);
                    newState.onEffect = false;
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
                    newState.onEffect = false;
                }

                enemy=newEnemy;
                newState.StartAffect(this);
                currentStates.Add(newState);

                return newState;

            }
        }
        return null;
    }


    public State AddStateDontRepeat(State newState)
    {
        if(newState != null){
            if (!currentStates.Exists(s => s.name == newState.name))
            {
                if (newState.onEffect)
                {
                    Debug.Log(gameObject + " manager cloned " + newState);
                    newState = Instantiate(newState);
                    newState.onEffect = false;
                }


                newState.StartAffect(this);
                currentStates.Add(newState);

                return newState;
            }

            return currentStates.Find( s => s.name == newState.name);
        }

        return null;
    }

     public State AddStateDontRepeatName(State newState)
    {
        if(newState != null){
            if (!currentStates.Exists(s => s.ObjectName.Contains(newState.ObjectName)) || !currentStates.Exists(s => newState.ObjectName.Contains(s.ObjectName)) )
            {
                if (newState.onEffect)
                {
                    Debug.Log(gameObject + " manager cloned " + newState);
                    newState = Instantiate(newState);
                    newState.onEffect = false;
                }


                newState.StartAffect(this);
                currentStates.Add(newState);

                return newState;
            }

            return currentStates.Find( s => s.name == newState.name);
        }

        return null;
    }


    public void RemoveState(State state){
        if(currentStates.Contains(state)){
            state.StopAffect();
        }
    }
    

    public void Stop(Predicate<State> predicate)
    {
        var state = currentStates.Find(predicate);
        state?.StopAffect();
    }

    public void StopAll( )
    {
        currentStates.ForEach(s => s.StopAffect());
        if (hostEntity?.emotePos?.childCount > 0)
        {
            if (hostEntity.emotePos.childCount > 0 && !currentStates.Exists( s => s is EmoteSetter))
            {
                    Destroy(hostEntity.emotePos.GetChild(0).gameObject);
            }
        }
    }

    public void StopAll( Type stateType )
    {
        if (stateType.Equals(typeof(State)) || stateType.IsSubclassOf (typeof(State)))
        {
            var states = currentStates.FindAll(s => s.GetType() == stateType);
            states.ForEach(s => s.StopAffect());
        }
    }

    public void StopAll(Type stateType, State exlude)
    {
        if (stateType.Equals(typeof(State)) || stateType.IsSubclassOf (typeof(State)))
        {
            var states = currentStates.FindAll(s => s != exlude && s.GetType() == stateType);
            states.ForEach(s => s.StopAffect());
        }
    }

    public void StopAll(Type stateType, List<State> exclude)
    {
        if (stateType.Equals(typeof(State)) || stateType.IsSubclassOf (typeof(State)))
        {
            var states = currentStates.FindAll(s => !exclude.Contains(s) && s.GetType() == stateType);
            states.ForEach(s => s.StopAffect());
        }
    }

    public void RemoveEmotes()
    {

        var children = new List<GameObject>();
        var count = hostEntity?.emotePos?.childCount;
        
        if (count == 0) return;

        for (int i = 0; i < count; i++)
        {
            children.Add(hostEntity.emotePos.GetChild(i).gameObject);
        }

        int index = 0;
        while(children.Count > 0)
        {
            if (children[index] != null)
            {
                Destroy(children[index]);
                children.Remove(children[index]);
            }
        }
    }
}
