using UnityEngine;
[CreateAssetMenu(fileName="New ProjectileStatsModifier", menuName = "States/new ProjectileStatsModifier")]
public class ProjectileStatsModifier : State
{
    [SerializeField] private float speedMultiplier;
    private float defSpeed;
    [SerializeField] private float damageMultiplier;
    private float defDamage;


    private Projectile projectile;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);

        if (manager.hostEntity.TryGetComponent<ProjectileShooter>(out var projectileShooter))
        {
            projectile = projectileShooter.Projectile;
            defSpeed = projectile.speedMultiplier;
            defDamage = projectile.damage;

            projectile.speedMultiplier *= speedMultiplier;
            projectile.damage *= defDamage;
        }
    }

    public override void Affect()
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

    public override void StopAffect()
    {
        if (projectile != null)
        {
            projectile.speedMultiplier = defSpeed;
            projectile.damage = defDamage;
        }
        base.StopAffect();
    }
}