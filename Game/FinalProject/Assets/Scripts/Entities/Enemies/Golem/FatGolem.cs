using UnityEngine;
public class FatGolem : FatType
{
    [Header("Self Additions")]
    [SerializeField] private float timeBtwPShot;
    private float curTimeBtwPShot;
    [SerializeField] private float timeBtwLShot;
    private float curTimeBtwLShot;


    [Header("ProjectileStuff")]
    [SerializeField] private float projectileKnockbackDuration;
    [SerializeField] private float projectileKnockbackForce;


    new void Awake()
    {
        base.Awake();
        projectileShooter.ProjectileTouchedPlayerHandler += projectileShooter_ProjectileTouchedPlayer;
    }

    protected override void ChasePlayer()
    {
        if (curTimeBtwPShot > timeBtwPShot)
        {
            projectileShooter.ShootProjectileAndSetDistance(player.GetPosition());
            curTimeBtwPShot = 0;
        }
        else
        {
            curTimeBtwPShot += Time.deltaTime;
        }
        if (curTimeBtwLShot > timeBtwLShot)
        {
            Debug.Log("should shoot");
            laserShooter.ShootLaser(player.GetPosition());
            curTimeBtwLShot = 0;
        }
        else
        {
            curTimeBtwLShot += Time.deltaTime;
        }
    }


    void projectileShooter_ProjectileTouchedPlayer()
    {
        Vector2 knockbackDir = (player.GetPosition() - GetPosition()).normalized;
        player.Knockback(projectileKnockbackDuration, projectileKnockbackForce, knockbackDir);
    }

}