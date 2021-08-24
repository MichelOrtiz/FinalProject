using UnityEngine;
[CreateAssetMenu (fileName = "New DamageCancel", menuName = "States/new DamageCancel")]
public class DamageCancel : State
{
    [SerializeField] private bool hasTime;
    [SerializeField] private bool stopAfterAttack;

    private Enemy enemy;
    private float defDamage;
    private EnemyCollisionHandler eCollisionHandler;
    
    private ProjectileShooter projectileShooter;
    private Projectile projectile;


    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);

        if (manager.hostEntity is Enemy)
        {
            enemy = (Enemy) manager.hostEntity;
            eCollisionHandler = enemy.eCollisionHandler;

            if (stopAfterAttack)
            {
                eCollisionHandler.StoppedTouchingHandler += StopAffect;
            }
        
            defDamage = enemy.Damage;
            enemy.Damage = 0;

            projectileShooter = enemy.GetComponentInChildren<ProjectileShooter>();
        }
    

    }
    public override void Affect()
    {
        if (hasTime)
        {
            if (currentTime > duration)
            {
                currentTime = 0;
                StopAffect();
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }

        if (projectileShooter != null && projectileShooter.Projectile != null)
        {
            projectile = projectileShooter.Projectile;
            //projectileShooter.ProjectileTouchedPlayerHandler += StopAffect;
            
            if (projectile.damage == projectile.StartDamage)
            {
                projectileShooter.Projectile.damage = 0;
            }
        } 
    }

    public override void StopAffect()
    {
        enemy.Damage = defDamage;
        
        if (projectileShooter.Projectile != null)
        {
            projectileShooter.Projectile.damage = projectile.StartDamage;
        }

        base.StopAffect();
    }
}