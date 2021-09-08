using UnityEngine;
using System;
public class LaserShooter : MonoBehaviour, ILaser
{
    [SerializeField] private GameObject laserPrefab;
    private Laser laser;
    public Laser Laser { get => laser; }
    [SerializeField] private float laserDamage;
    [SerializeField] private State laserEffect;

    [SerializeField] private Transform shotPos;
    public Transform ShotPos { get => shotPos; }
    private Vector2 endPos;
    public Vector2 EndPos { get => endPos; }

    private Entity entity;
    private PlayerManager player;
    private Transform endPosHolder;

    #region events
    public event Action OnLaserAttack = delegate { };
    #endregion

    void Start()
    {
        player = PlayerManager.instance;

        entity = transform.parent.GetComponent<Entity>();
        laserPrefab.GetComponent<Laser>().targetWarningAvailable = player.abilityManager.IsUnlocked(Ability.Abilities.VisionFutura);

    }

    void Update()
    {
        if (laser != null)
        {
            if (laser.chaseOnReachedEndPos || laser.chaseTargetPosition)
            {
                if (endPosHolder!=null) endPos = endPosHolder.position;
            }
        }
    }

    public void LaserAttack()
    {
        if (laserEffect != null)
        {
            player.statesManager.AddState(laserEffect, entity);
        }
        player.TakeTirement(laserDamage);
        OnLaserAttack?.Invoke();
    }

    public void ShootLaser(Vector2 to)
    {
        endPos = to;
        ShootLaser(shotPos.position, to);
    }

    public void ShootLaser(Vector2 from, Vector2 to)
    {
        endPos = to;
        laser = Instantiate(laserPrefab, from, laserPrefab.transform.rotation).GetComponent<Laser>();
        laser.Setup(from, to, this);
    }

    public void ShootLaserAndSetEndPos(Transform endPos)
    {
        endPosHolder = endPos;
        ShootLaser(shotPos.position, endPos.position);
    }
}
