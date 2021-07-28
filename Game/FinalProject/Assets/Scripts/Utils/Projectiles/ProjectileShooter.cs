using System;
using UnityEngine;
public class ProjectileShooter : MonoBehaviour, IProjectile
{
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    public Projectile Projectile { get => projectile; }
    [SerializeField] private Transform shotPos;
    public Transform ShotPos { get => shotPos; }
    [SerializeField] private State effectOnPlayer;
    /*
    [SerializeField] private bool canKnockbackPlayer;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockBackForce;
    */
    #region Events
    public delegate void ProjectileTouchedPlayer();
    public event ProjectileTouchedPlayer ProjectileTouchedPlayerHandler;
    protected virtual void OnProjectileTouchedPlayer()
    {
        ProjectileTouchedPlayerHandler?.Invoke();
    }

    //public event Action OnProjectileDestroyed = delegate { };
    #endregion

    private PlayerManager player;
    private Entity entity;

    void Start()
    {
        player = PlayerManager.instance;
        if (entity == null)
        {
            entity = transform.parent.GetComponent<Entity>();
        }
    }

    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
        player.statesManager.AddState(effectOnPlayer, entity);
        OnProjectileTouchedPlayer();
    }


    public void ShootProjectile(Vector2 to)
    {
        projectile = Instantiate(projectilePrefab, shotPos.position, projectilePrefab.transform.rotation).GetComponent<Projectile>();
        projectile.Setup(shotPos, to, this);
    }

    public void ShootProjectile(Vector2 from, Vector2 to)
    {
        projectile = Instantiate(projectilePrefab, from, projectilePrefab.transform.rotation).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }

    public void ShootProjectile(Vector2 to, string colliderTag)
    {
        projectile = Instantiate(projectilePrefab, shotPos.position, projectilePrefab.transform.rotation).GetComponent<Projectile>();
        projectile.Setup(shotPos, to, this, colliderTag);
    }

    public void ShootProjectile(Vector2 from, Vector2 to, string colliderTag)
    {
        projectile = Instantiate(projectilePrefab, from, projectilePrefab.transform.rotation).GetComponent<Projectile>();
        projectile.Setup(from, to, this, colliderTag);
    }

    public void ShootProjectileAndSetDistance(Vector2 to)
    {
        ShootProjectile(to);
        projectile.MaxShotDistance = Vector2.Distance(shotPos.position, to);
    }
    public void ShootProjectileAndSetDistance(Vector2 from, Vector2 to)
    {
        ShootProjectile(from, to);
        projectile.MaxShotDistance = Vector2.Distance(from, to);
    }

    public void ShootProjectileAndSetDistance(Vector2 to, string colliderTag)
    {
        ShootProjectile(to, colliderTag);
        projectile.MaxShotDistance = Vector2.Distance(shotPos.position, to);
    }
    public void ShootProjectileAndSetDistance(Vector2 from, Vector2 to, string colliderTag)
    {
        ShootProjectile(from, to, colliderTag);
        projectile.MaxShotDistance = Vector2.Distance(from, to);
    }

    [Obsolete("maybe delete later idk")]
    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, projectilePrefab.transform.rotation).GetComponent<Projectile>();
        projectile.Setup(shotPos, to, this);
    }
}