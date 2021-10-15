using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] Interaction inter;
    [SerializeField] float radius;
    float distance;
    
    void Start()
    {
        PlayerManager.instance.inputs.Interact += TriggerInteraction;
    }
    void Update(){
        distance = Vector2.Distance(PlayerManager.instance.GetPosition(),transform.position);
    }
    void TriggerInteraction(){
        if(distance > radius) return;
        if(inter.condition != null){
            if(inter.condition){
                inter.DoInteraction();
            }else{
                Debug.Log("No se cumple lo necesario para esta interaccion");
            }
        }else{
            Debug.Log("No se necesita algo para esta interaccion");
            inter.DoInteraction();
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
