using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : ScriptableObject {
    public enum InteractionType{Dialogue,Interface,GiveItem,None}
    public InterCondition condition = null;
    public InteractionType type = InteractionType.None;
    public delegate void OnEndInteraction();
    public OnEndInteraction onEndInteraction;    
    public abstract void DoInteraction();
}


