using UnityEngine;
using System.Collections.Generic;

public class ItemInteractionManager : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private ItemInteraction itemInteraction;

    private State currentState;
    private byte index;
    private List<State> states;

    [SerializeReference] private bool interacting;

    void Start()
    {
        index = 0;
        if (entity == null)
        {
            entity = transform.parent.GetComponentInChildren<Entity>();
        }
    }

    void Update()
    {
        if (interacting)
        {
            if (!entity.statesManager.currentStates.Contains(currentState))
            {
                
                entity.statesManager.AddState(currentState);
            }
        }
    }


    public void Interact(Item item)
    {
        var itemState = itemInteraction.itemStates.Find(i => i.item == item);
        if (itemState != null)
        {
            if (itemState.states != null)
            {

                states = itemState.states;
                interacting = true;

                UpdateCurrentState();
            }
            else
            {
                // Make entity leave the scene
                entity.DestroyEntity();
            }
        }
    }


    void UpdateCurrentState()
    {
        if (states != null && states.Count > 0)
        {
            if (currentState != null)
            {
                index++;
            }
            if (index <= states.Count - 1 || (index == 0 ))
            {
                currentState = states[index];
                currentState.StoppedAffect += currentState_StoppedAffect;
            }
            else
            {
                interacting = false;
            }
        }
    }
    
    void currentState_StoppedAffect()
    {
        UpdateCurrentState();
    }
}