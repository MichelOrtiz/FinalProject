using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UBGroundBehaviour : UBBehaviour, ILaser
{
    #region TargetPosition
    [Header("Target Position")]
    [SerializeField] private Vector2 positionToGo;
    [SerializeField] private float destinationRadius;
    private bool reachedDestination;
    #endregion

    #region LaserBeam
    [Header("Laser Beam")]
    [SerializeField] private Transform shotPos;
    public Transform ShotPos { get => shotPos; }
    private Vector2 endPos;
    public Vector2 EndPos { get => endPos; }
    [SerializeField] private float intervalToShot;
    private float timeToShot;
    private bool shotLaser;

    //[SerializeField] private LineRenderer laser;
    [SerializeField] private GameObject laserPrefab;
    private Laser laser;

    [SerializeField] private float laserDamage;

    #endregion
    


    new void Start()
    {
        base.Start();
        SetDefaults();
    }

    // Update is called once per frame
    new void Update()
    {
        if (!finishedAttack)
        {
            endPos = player.GetPosition();
            if (ReachedDestination())
            {
                if (!shotLaser)
                {
                    endPos = player.GetPosition();
                    ShootLaser(shotPos.position, endPos);
                    shotLaser = true;
                }
                else if (laser == null)
                {
                    OnFinishedAttack();
                }
            }
        }
        

        base.Update();
    }

    void FixedUpdate()
    {
        if (!ReachedDestination())
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), positionToGo, speed * Time.deltaTime);
        }
    }

    bool ReachedDestination()
    {
        return Vector2.Distance(GetPosition(), positionToGo) <= destinationRadius;
    }

    public void ShootLaser(Vector2 from, Vector2 to)
    {
        laser = Instantiate(laserPrefab, from, Quaternion.identity).GetComponent<Laser>();
        laser.Setup(from, to, this);
    }

    public void LaserAttack()
    {
        player.TakeTirement(laserDamage);
    }

    new void OnFinishedAttack()
    {
        shotLaser = false;
        base.OnFinishedAttack();
    }

    protected override void SetDefaults()
    {
        return;
    }
}
