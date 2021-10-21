using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public Interaction currentInteraction;
    protected virtual void Start(){
        //Detener al jugador
        PlayerManager.instance.SetEnabledPlayer(false);
    }
    protected virtual void Exit(){
        currentInteraction.onEndInteraction?.Invoke();
        Destroy(gameObject);
        //Permitir movimiento al jugador
        PlayerManager.instance.SetEnabledPlayer(true);
    }
}
