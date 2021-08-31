using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu(fileName="New ItemInteraction", menuName = "Inventory/ItemInteraction")]
public class ItemInteraction : ScriptableObject
{
    public List<ItemState> itemStates;
}