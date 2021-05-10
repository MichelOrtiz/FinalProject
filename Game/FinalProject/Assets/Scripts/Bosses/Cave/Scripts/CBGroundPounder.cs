using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBGroundPounder : Entity, ILaser
{
    #region TargetPosition
    [Header("Target Position")]
    [SerializeField] private Vector2 positionToGo;
    [SerializeField] private float destinationRadius;
    private bool reachedDestination;
    
    [SerializeField] private float xRangeToSeePlayer;

    #endregion

    #region OnPlayerEffects
    [Header("Effects on Player")]
    [SerializeField] private State effectOnPlayer;
    [SerializeField] private float damageAmount;
    private  bool sawPlayer;
    #endregion

    #region Physics
    [Header("Physic Params")]
    [SerializeField] private float speedMultiplier;
    private float speed;

    [SerializeField] private float pushForce;
    #endregion

    #region Collisions
    [Header("Collisions")]
    [SerializeField] private CBRoomManager roomManager;
    private List<GameObject> grounds;

    private bool hitGround;
    [SerializeField] private float waitTimeWhenCollide;
    private float currentWaitTime;

    //private bool touchingGround;
    #endregion

    #region Thread/Laser
    [Header("Thread/Laser")]
    [SerializeField] private GameObject laserPrefab;
    private Laser laser;

    [SerializeField] private Transform shotPos;
    public Transform ShotPos { get => shotPos; }
    [SerializeField] private Transform endPos;
    public Vector2 EndPos { get => endPos.position; }


    #endregion


    // Might inherit later *-*-*-*-*-*
    private PlayerManager player;
    private EnemyCollisionHandler eCollisionHandler;

    

    void Awake()
    {
        eCollisionHandler = (EnemyCollisionHandler) collisionHandler;
        //eCollisionHandler.TouchingGroundHandler += eCollisionHandler_TouchingGround;

        eCollisionHandler.TouchingPlayer += eCollisionHandler_Attack;


        groundChecker.GroundedHandler += groundChecker_Grounded;
        //groundChecker.GroundedGameObjectHandler += groundChecker_GroundedGameObject;


        speed = averageSpeed * speedMultiplier;

       
    }

    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        grounds = roomManager.grounds;

        ShootLaser(shotPos.position, endPos.position);
    }

    // Update is called once per frame
    new void Update()
    {
        // So it is true forever, but only once the enemy reached destination for the first time
        if (!reachedDestination)
        {
            reachedDestination = Vector2.Distance(GetPosition(), positionToGo) <= destinationRadius
                                && player.GetPosition().y < GetPosition().y;
        }

        if (!sawPlayer)
        {
            sawPlayer = Mathf.Abs(GetPosition().x - player.GetPosition().x) <= xRangeToSeePlayer;
        }
            
        if (hitGround)
        {
            if (currentWaitTime > waitTimeWhenCollide)
            {
                hitGround = false;
                currentWaitTime = 0;
            }
            else
            {
                StopMoving();
                currentWaitTime += Time.deltaTime;
                return;
            }
        }

        
        base.Update();
    }

    
    void FixedUpdate()
    {
        if (!hitGround)
        {
            if (!reachedDestination)
            {
                rigidbody2d.position = Vector2.MoveTowards(GetPosition(), positionToGo, speed * Time.deltaTime);
            }
            else if (sawPlayer)
            {
                Push(0, -pushForce);
            }
        }
    }


    void StopMoving()
    {
        rigidbody2d.velocity = new Vector2();
    }


    void groundChecker_Grounded(string groundTag)
    {
        if (groundTag == "Ground")
        {
            StopMoving();
            hitGround = true;
            reachedDestination = false;
            sawPlayer = false;
        }
    }

    void eCollisionHandler_Attack()
    {
        player.TakeTirement(damageAmount);
        //player.statesManager.AddState(effectOnPlayer);
    }

    public void ShootLaser(Vector2 from, Vector2 to)
    {
        laser = Instantiate(laserPrefab, from, Quaternion.identity).GetComponent<Laser>();
        laser.Setup(from, to, this);
    }

    public void LaserAttack()
    {
        return;
    }
}
