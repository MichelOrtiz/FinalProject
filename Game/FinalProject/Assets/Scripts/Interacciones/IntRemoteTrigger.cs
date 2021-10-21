using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntRemoteTrigger : InteractionTrigger
{
    protected override void Start()
    {
        cola = new Queue<Interaction>();
        busy = false;
    }
    [SerializeField] Transform triggerPoint;
    protected override void TriggerInteraction()
    {
        if(busy) return;
        busy = true;
        Debug.Log("Interaction Triggered Remotetly");
        cola.Clear();
        foreach (Interaction inter in interactions)
        {
            cola.Enqueue(inter);
            inter.onEndInteraction += NextInteraction;
            inter.gameObject = this.gameObject;
            if(inter.condition != null){
                inter.condition.RestardValues(gameObject);
            }
        }
        NextInteraction();
    }
    protected override void Update()
    {
        distance = Vector2.Distance(PlayerManager.instance.GetPosition(),triggerPoint.position);
        if(distance <= radius && !busy) TriggerInteraction();
        updateForInteractions?.Invoke();
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(triggerPoint.position, radius);
    }
}
