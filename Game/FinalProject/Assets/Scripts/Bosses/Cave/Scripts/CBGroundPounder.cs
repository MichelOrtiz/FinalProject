using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBGroundPounder : CaveBossBehaviour, ILaser, IProjectile
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
    //[SerializeField] private CBRoomManager roomManager;
    //private List<GameObject> grounds;
    private bool hitGround;
    [SerializeField] private byte maxGroundHits;
    [SerializeReference]private byte currentGroundHits;
    //[SerializeField] private byte maxProjectileHits;
    //private byte projectileHits;
    [SerializeField] private float waitTimeWhenCollide;
    private float currentWaitTime;
    private bool touchingPlayer;
    //private bool touchingGround;
    #endregion

    #region Thread/Laser
    [Header("Thread/Laser")]
    [SerializeField] private GameObject laserPrefab;
    private Laser laser;

    [SerializeField] private Transform shotPos;
    public Transform ShotPos { get => shotPos; }
    [SerializeField] private Vector2 endPos;
   // private Transform endPosClone;
    public Vector2 EndPos { get => endPos; }
    #endregion

    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    [SerializeField] private Transform projectileShotPos;
    private Vector2 shotPoint;
    [SerializeField] private float timeBtwShot;
    private float currentTimeBtwShot;
    [SerializeField] private int minAnglePerShot;
    [SerializeField] private int maxAnglePerShot;
    [SerializeField] private int minAngleBtwProjectiles;
    [SerializeField] private Transform center;
    [SerializeField] private byte shotsPerBurst;

    private bool hit;
    #endregion


    // Might inherit later *-*-*-*-*-*
    private PlayerManager player;
    private EnemyCollisionHandler eCollisionHandler;

    

    void Awake()
    {
        eCollisionHandler = (EnemyCollisionHandler) collisionHandler;
        //eCollisionHandler.TouchingGroundHandler += eCollisionHandler_TouchingGround;
        eCollisionHandler.EnterTouchingContactHandler += eCollisionHandler_EnterCollision;
        eCollisionHandler.ExitTouchingContactHandler += eCollisionHandler_ExitCollision;


        eCollisionHandler.TouchingPlayer += eCollisionHandler_Attack;


        groundChecker.GroundedHandler += groundChecker_Grounded;
        //groundChecker.GroundedGameObjectHandler += groundChecker_GroundedGameObject;


        speed = averageSpeed * speedMultiplier;

       
    }

    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;

        /*if (roomManager == null)
        {
            roomManager = FindObjectOfType<CBRoomManager>();
        }

        grounds = roomManager.grounds;*/
        ShootLaser(shotPos.position, endPos);
    }

    // Update is called once per frame
    new void Update()
    {
        if (!sawPlayer)
        {
            sawPlayer = reachedDestination && Mathf.Abs(GetPosition().x - player.GetPosition().x) <= xRangeToSeePlayer;
        }
        // So it is true forever, but only once the enemy reached destination for the first time
        if (!reachedDestination)
        {
            reachedDestination = Vector2.Distance(GetPosition(), positionToGo) <= destinationRadius
                                && player.GetPosition().y < GetPosition().y;
        }
        else
        {
            if (currentTimeBtwShot > timeBtwShot)
            {
                ShootProjectiles();
                currentTimeBtwShot = 0;
            }
            else
            {
                currentTimeBtwShot += Time.deltaTime;
            }
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
                GoToDestination();
            }
            else if (sawPlayer)
            {
                //if (!touchingPlayer)
                {
                    Push(0, -pushForce);
                }
                /*else
                {
                    //StopMoving();
                    reachedDestination = false;
                    GoToDestination();
                }*/
            }
        }
    }

    void GoToDestination()
    {
        rigidbody2d.position = Vector2.MoveTowards(GetPosition(), positionToGo, speed * Time.deltaTime);
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

            currentGroundHits++;
            if (currentGroundHits >= maxGroundHits)
            {
                rigidbody2d.gravityScale = 1;
                Destroy(FindObjectOfType<LineRenderer>());
                Destroy(FindObjectOfType<Laser>());
            }
        }
    }

    void eCollisionHandler_Attack()
    {
        touchingPlayer = true;
        player.TakeTirement(damageAmount);
        //player.statesManager.AddState(effectOnPlayer);
    }

    void eCollisionHandler_EnterCollision(GameObject contact)
    {
        if (contact.tag == "Player")
        {
            StopMoving();
        }
        if (contact.tag == "Ceiling")
        {
            if (currentGroundHits >= maxGroundHits)
            {
               OnFinished(GetPosition());
            }
            hit = true;
        }
    }
    void eCollisionHandler_ExitCollision(GameObject contact)
    {
        if (contact.tag == "Player")
        {
            touchingPlayer = false;
        }
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

    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
    }

    public void ShootProjectiles()
    {
        int lastAngle = 0;
        int angle = 0;
        byte shotsDone = 0;
        while (shotsDone < shotsPerBurst)
        {
            do
            {
                angle = Random.Range(minAnglePerShot, maxAnglePerShot);
            }
            while (Mathf.Abs(lastAngle - angle) < minAngleBtwProjectiles);
            
            shotPoint = center.position + MathUtils.GetVectorFromAngle(angle);

            ShotProjectile(center.position, shotPoint );
            shotsDone++;

            lastAngle = angle;
        }
    }

    public void ShotProjectile(Transform from, Vector3 to){}  
    public void ShotProjectile(Vector2 from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }
}
