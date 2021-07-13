using UnityEngine;
public class ElectricMedufin : NormalType
{
    [Header("Self Additions")]
    [SerializeField] private State selfStateWhenAttack;
    protected override void Attack()
    {
        statesManager.AddState(selfStateWhenAttack);
        
        base.Attack();
    }
}