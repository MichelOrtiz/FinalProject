using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntRemoteTrigger : InteractionTrigger
{
    [SerializeField] protected Transform triggerPoint;
    protected override void Start()
    {
        cola = new Queue<Interaction>();
        busy = false;
    }
    protected override void TriggerInteraction()
    {
        base.TriggerInteraction();
        Debug.Log("Interaction Triggered Remotetly");
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
