using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public Interaction currentInteraction;
    protected virtual void Start(){
        //Detener al jugador
    }
    protected virtual void Exit(){
        currentInteraction.onEndInteraction?.Invoke();
        //Permitir movimiento al jugador
    }
}
