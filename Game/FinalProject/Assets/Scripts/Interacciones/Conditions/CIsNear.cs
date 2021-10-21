using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Condition", menuName = "Interaction/Conditions/IsNear")]
public class CIsNear : InterCondition
{
    [SerializeField] float radius;
    protected override bool checkIsDone()
    {
        float distance = Vector2.Distance(PlayerManager.instance.GetPosition(),gameObject.transform.position);
        return (distance <= radius);
    }
}
