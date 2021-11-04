using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] protected List<Interaction> interactions;
    protected Queue<Interaction> cola;
    public Interaction currentInter;
    public Interaction lastInter { 
            get{
                if(currentInter==null) return interactions[interactions.Count-1];
                int i = interactions.FindIndex( x => x == currentInter);
                if(i > 0){
                    return interactions[i-1];
                }else{
                    return null;
                }
            } 
        }
    [SerializeField] protected float radius;
    protected float distance;
    public bool busy;
    public delegate void RunInUpdate();
    public RunInUpdate updateForInteractions;
    protected virtual void Start()
    {
        cola = new Queue<Interaction>();
        busy = false;
        foreach(Interaction inter in interactions){
            inter.gameObject = this.gameObject;
        }
    }
    protected virtual void Update(){
        distance = Vector2.Distance(PlayerManager.instance.GetPosition(),transform.position);
        if(distance <= radius)
        {
            PlayerManager.instance.inputs.Interact -= TriggerInteraction;
            PlayerManager.instance.inputs.Interact += TriggerInteraction;
        }else{
            PlayerManager.instance.inputs.Interact -= TriggerInteraction;
        }     
        updateForInteractions?.Invoke();
    }
    protected virtual void TriggerInteraction(){
        if(distance > radius || busy) return;
        busy = true;
        cola.Clear();
        foreach (Interaction inter in interactions)
        {
            inter.onEndInteraction -= NextInteraction;
            inter.onEndInteraction += NextInteraction;
            inter.gameObject = this.gameObject;
            inter.RestardCondition();
            cola.Enqueue(inter);
        }
        NextInteraction();
    }
    protected virtual void NextInteraction(){
        if(cola.Count == 0){
            currentInter = null;
            Debug.Log("No hay mas interacciones");
            busy = false;
            return;
        }
        Interaction inter = cola.Dequeue();
        currentInter = inter;
        Debug.Log("Current interaction: " + currentInter.name);
        inter.RestardCondition();
        inter.DoInteraction();
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
