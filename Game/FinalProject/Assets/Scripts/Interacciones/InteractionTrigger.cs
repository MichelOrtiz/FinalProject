using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] protected List<Interaction> interactions;
    protected Queue<Interaction> cola;
    protected Interaction lastInter;
    [SerializeField] protected float radius;
    protected float distance;
    protected bool busy;
    public delegate void RunInUpdate();
    public RunInUpdate updateForInteractions;
    protected virtual void Start()
    {
        PlayerManager.instance.inputs.Interact += TriggerInteraction;
        cola = new Queue<Interaction>();
        busy = false;
    }
    protected virtual void Update(){
        distance = Vector2.Distance(PlayerManager.instance.GetPosition(),transform.position);
        updateForInteractions?.Invoke();
    }
    protected virtual void TriggerInteraction(){
        if(distance > radius && !busy) return;
        busy = true;
        cola.Clear();
        foreach (Interaction inter in interactions)
        {
            inter.onEndInteraction += NextInteraction;
            inter.gameObject = this.gameObject;
            if(inter.condition != null){
                inter.condition.RestardValues(gameObject);
            }
            
            cola.Enqueue(inter);
        }
        NextInteraction();
    }
    protected void NextInteraction(){
        if(cola.Count == 0){
            Debug.Log("No hay mas interacciones");
            //busy = false;
            return;
        }
        Interaction inter = cola.Dequeue();
        if(inter == lastInter)return;
        inter.DoInteraction();
        lastInter = inter;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
