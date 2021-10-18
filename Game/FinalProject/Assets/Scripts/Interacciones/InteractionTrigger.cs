using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] List<Interaction> interactions;
    Queue<Interaction> cola;
    [SerializeField] float radius;
    float distance;
    bool busy;
    void Start()
    {
        PlayerManager.instance.inputs.Interact += TriggerInteraction;
        cola = new Queue<Interaction>();
        busy = false;
    }
    void Update(){
        distance = Vector2.Distance(PlayerManager.instance.GetPosition(),transform.position);
    }
    void TriggerInteraction(){
        if(distance > radius && !busy) return;
        busy = true;
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
            busy = false;
            return;
        }
        Interaction inter = cola.Dequeue();
        inter.DoInteraction();
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
