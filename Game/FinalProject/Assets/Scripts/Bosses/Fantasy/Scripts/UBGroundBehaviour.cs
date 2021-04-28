using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UBGroundBehaviour : Entity, ILaser
{
    [SerializeField] private float speedMultiplier;
    private float speed;
    
    [SerializeField] private Vector2 positionToGo;
    [SerializeField] private float destinationRadius;
    
    private bool reachedDestination;
    private PlayerManager player;

    #region LaserBeam
    [Header("Laser Beam")]
    [SerializeField] private Transform shotPos;
    public Transform ShotPos { get => shotPos; }
    private Vector2 endPos;
    public Vector2 EndPos { get => endPos; }
    [SerializeField] private float intervalToShot;
    private float timeToShot;

    //[SerializeField] private LineRenderer laser;
    [SerializeField] private GameObject laserPrefab;
    private Laser laser;

    [SerializeField] private float laserDamage;

    #endregion
    

    /*public delegate void ReachedDestination();
    public event ReachedDestination ReachedDestinationHandler;
    protected virtual void OnReachedDestination()
    {
        ReachedDestinationHandler?.Invoke();
        reachedDestination = true;
        ShootLaser(shotPos.position, player.GetPosition());
    }*/

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        speed = averageSpeed * speedMultiplier;
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    new void Update()
    {
        endPos = player.GetPosition();
        if (!reachedDestination)
        {
            if (Vector2.Distance(GetPosition(), positionToGo) <= destinationRadius)
            {
                //OnReachedDestination();
                reachedDestination = true;
                endPos = player.GetPosition();
                ShootLaser(shotPos.position, endPos);
            }
        }


        base.Update();
    }

    void FixedUpdate()
    {
        if (!reachedDestination)
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), positionToGo, speed * Time.deltaTime);
        }
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
}
