using UnityEngine;
using System;

[CreateAssetMenu(fileName="New EnemyStatsModifier", menuName = "States/new EnemyStatsModifier")]
public class EnemyStatsModifier : State
{
    [SerializeField] private bool hasTime;

    [Header("Speed")]
    [SerializeField] private float defaultSpeedMultiplier; 
    private float defaultSpeed;
    [SerializeField] private float chaseSpeedMultiplier;
    private float defChaseSpeed;


    [Header("Physics")]
    [SerializeField] private Vector2 jumpForceMultiplier;
    private Vector2 defJumpForce;

    
    [Header("Field Of View")]
    [SerializeField] private float newFovDistance;
    private float defFovDistance;

    
    [Header("Damage")]
    [SerializeField] private float damageMultiplier;
    private float defaultDamage;




    private Enemy enemy;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        if (manager.hostEntity is Enemy)
        {
            enemy = (Enemy) manager.hostEntity;
            
            defaultSpeed = enemy.EnemyMovement.DefaultSpeed;
            defJumpForce = enemy.EnemyMovement.JumpForce;
            defChaseSpeed = enemy.EnemyMovement.ChaseSpeed;
            defFovDistance = enemy.FieldOfView.ViewDistance;
            defaultDamage = enemy.Damage;

            enemy.EnemyMovement.DefaultSpeed *= defaultSpeedMultiplier;
            enemy.EnemyMovement.JumpForce *= jumpForceMultiplier;
            enemy.EnemyMovement.ChaseSpeed *= chaseSpeedMultiplier;
            enemy.FieldOfView.ViewDistance = newFovDistance;
            enemy.Damage *= damageMultiplier;

        }    
    }

    public override void Affect()
    {
        if (hasTime)
        {
            if (currentTime > duration)
            {
                StopAffect();
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    public override void StopAffect()
    {
        enemy.EnemyMovement.DefaultSpeed = defaultSpeed;
        enemy.EnemyMovement.JumpForce = defJumpForce;
        enemy.EnemyMovement.ChaseSpeed = defChaseSpeed;
        enemy.FieldOfView.ViewDistance = defFovDistance;
        enemy.Damage = defaultDamage;
        base.StopAffect();
    }
}