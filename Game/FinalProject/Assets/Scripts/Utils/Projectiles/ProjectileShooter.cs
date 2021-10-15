using System;
using UnityEngine;
using System.Collections.Generic;
public class ProjectileShooter : MonoBehaviour, IProjectile
{
    public GameObject projectilePrefab;

    
    public Projectile ProjectileFromPrefab
    {
        get
        {
            return projectilePrefab.GetComponentInChildren<Projectile>();
        } 
        set
        {
            var proj = projectilePrefab.GetComponentInChildren<Projectile>();
            proj = value;
        }
    }

    private Projectile projectile;
    public Projectile Projectile { get => projectile; set => projectile = value; }
    [SerializeField] private Transform shotPos;
    public Transform ShotPos { get => shotPos; }
    [SerializeField] private State effectOnPlayer;
    public State EffectOnPlayer { get => effectOnPlayer; }

    [Header("Modifications")]
    [InspectorName("RotateTargetDirection")]
    [SerializeField] private bool rotate;
    [SerializeField] private Transform center;
    [SerializeField] private float startingAngle;
    [SerializeField] private float rotationRate;
    [SerializeField] private float timeToCompleteCircle;
    private float rotationSpeed;
    [InspectorName("RotateClockwise")]
    public bool isClockwise;
    [SerializeField] private byte bustSize;
    [SerializeField] private byte burstAngle;
    [SerializeField] private bool burstClockwise;
    private float centerAngle;
    private Vector2 targetDirection;
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

    void Awake()
    {
        rotationSpeed = (2*Mathf.PI)/timeToCompleteCircle;
    }

    void Start()
    {
        player = PlayerManager.instance;
        if (entity == null)
        {
            entity = transform.parent.GetComponent<Entity>();
        }

        centerAngle = startingAngle;

        projectilePrefab.GetComponent<Projectile>().targetWarningAvailable = player.abilityManager.IsUnlocked(Ability.Abilities.VisionFutura);
    }

    
    void Update()
    {
        if (rotate)
        {
            RotateTargetDirection();
        }
    }

    void RotateTargetDirection()
    {
        if (isClockwise)
        {
            centerAngle -= rotationSpeed * Time.deltaTime;
        }
        else
        {
            centerAngle += rotationSpeed * Time.deltaTime;
        }
        //shotPos.position = center.position + MathUtils.GetVectorFromAngle(centerAngle + rotationRate);
        //targetDirection = shotPos.position;
    }

    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
        if (projectile.damage > 0)
        {
            player.SetImmune();
        }

        player.statesManager.AddState(effectOnPlayer, entity);
        OnProjectileTouchedPlayer();
    }

    public void ShootSeekerProjectile(Transform to)
    {
        projectile = Instantiate(projectilePrefab, shotPos.position, projectilePrefab.transform.rotation).GetComponent<Projectile>();
        var seeker = projectile as SeekerProjectile;
        seeker.Setup(shotPos, to, this);
    }

    public Projectile ShootProjectile(Vector2 to)
    {
        projectile = Instantiate(projectilePrefab, shotPos.position, projectilePrefab.transform.rotation).GetComponent<Projectile>();
        projectile.Setup(shotPos, to, this);
        return projectile;
    }

    public Projectile ShootProjectile(Vector2 from, Vector2 to)
    {
        projectile = Instantiate(projectilePrefab, from, projectilePrefab.transform.rotation).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
        return projectile;
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

    public List<Projectile> ShootRotating()
    {
        float angle = startingAngle;
        var projectiles = new List<Projectile>();
        for (int i = 0; i < bustSize; i++)
        {
            targetDirection = center.position + MathUtils.GetVectorFromAngle(angle);
            var proj = ShootProjectile(center.position, targetDirection + (Vector2) MathUtils.GetVectorFromAngle(centerAngle));
            projectiles.Add(proj);
            if (burstClockwise)
            {
                angle -= burstAngle;
            }
            else
            {
                angle += burstAngle;
            }
        }
        return projectiles;
    }
    
    [Obsolete("maybe delete later idk")]
    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, projectilePrefab.transform.rotation).GetComponent<Projectile>();
        projectile.Setup(shotPos, to, this);
    }


    public void ChangeRotation()
    {
        isClockwise = !isClockwise;
    }
}