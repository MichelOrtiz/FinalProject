using UnityEngine;
using System;

public class ItemInteractionManager : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private ItemInteraction itemInteraction;

    void Start()
    {
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
            entity.statesManager.AddState(itemState.state);
        }
    }
}