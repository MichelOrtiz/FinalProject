using UnityEngine;
using System.Collections.Generic;

public class ItemInteractionManager : MonoBehaviour
{
    public Entity entity;
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
        //var itemState = itemInteraction.itemStates.Find(i => i.item == item);
        var itemState = itemStates.Find(i => i.item.nombre == item.nombre);
        if (itemState != null)
        {
            if (itemState.states != null && itemState.states[0] != null)
            {

                states = itemState.states;
                index = 0;
                //interacting = true;
                currentState = null;
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
                Debug.Log("current state not null");

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
                //Debug.Log("index " + index);
                //interacting = false;
            }
        }
    }
    
    /*void currentState_StoppedAffect()
    {
        UpdateCurrentState();
    }*/
}