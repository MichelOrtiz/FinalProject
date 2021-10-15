using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] List<Interaction> interactions;
    Queue<Interaction> cola;
    [SerializeField] float radius;
    float distance;
    
    void Start()
    {
        PlayerManager.instance.inputs.Interact += TriggerInteraction;
        cola = new Queue<Interaction>();
    }
    void Update(){
        distance = Vector2.Distance(PlayerManager.instance.GetPosition(),transform.position);
    }
    void TriggerInteraction(){
        if(distance > radius) return;
        cola.Clear();
        foreach (Interaction inter in interactions)
        {
            cola.Enqueue(inter);
            inter.onEndInteraction += NextInteraction;
        }
        NextInteraction();
    }
    void NextInteraction(){
        if(cola.Count == 0){
            Debug.Log("No hay mas interacciones");
            return;
        }
        Interaction inter = cola.Dequeue();
        if(inter.condition != null){
            if(inter.condition.isDone){
                inter.DoInteraction();
            }else{
                Debug.Log("No se cumple lo necesario para esta interaccion");
                NextInteraction();
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
