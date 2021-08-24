using UnityEngine;
using System.Collections.Generic;

public class ItemInteractionManager : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private ItemInteraction itemInteraction;

    [SerializeField] private List<ItemState> itemStates;

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

    public void Interact(Item item)
    {
        var itemState = itemInteraction.itemStates.Find(i => i.item == item);
        if (itemState != null)
        {
            if (itemState.states != null && itemState.states[0] != null)
            {

                states = itemState.states;
                //interacting = true;

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
        Debug.Log("Update called: " + transform.parent.gameObject);
        if (states != null && states.Count > 0)
        {
            if (currentState != null)
            {
                index++;
            }
            if (index <= states.Count - 1 || (index == 0 ))
            {
                currentState = states[index];

                if (currentState == null)
                {
                    entity.DestroyEntity();
                }
                else
                {
                    currentState = entity.statesManager.AddState(currentState);
                    currentState.StoppedAffect += UpdateCurrentState;
                }

            }
            else
            {
                //interacting = false;
            }
        }
    }
    
    /*void currentState_StoppedAffect()
    {
        UpdateCurrentState();
    }*/
}