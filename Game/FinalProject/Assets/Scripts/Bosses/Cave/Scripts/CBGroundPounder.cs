using System.Collections;
using System.Collections.Generic;
using FinalProject.Assets.Scripts.Utils.Sound;
using UnityEngine;

public class CBGroundPounder : CaveBossBehaviour
{
    #region TargetPosition
    [Header("Target Position")]
    [SerializeField] private Vector2 positionToGo;
    [SerializeField] private float destinationRadius;
    private bool reachedDestination;
    
    [SerializeField] private float xRangeToSeePlayer;

    #endregion

    #region OnPlayerEffects
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
    [SerializeField] private LaserShooter laserShooter;
    [SerializeField] private Vector2 endPos;
    #endregion

    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private ProjectileShooter projectileShooter;
    [SerializeField] private float timeBtwShot;
    private float currentTimeBtwShot;
    [SerializeField] private float timeMod;
    [SerializeField] private float timeToChangeRotation;

    private bool hit;
    #endregion


    // Might inherit later *-*-*-*-*-*
    

    new void Awake()
    {
        base.Awake();

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
        laserShooter.ShootLaser(endPos);
        InvokeRepeating("ChangeProjectileRotation", timeToChangeRotation, timeToChangeRotation);
        
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
                projectileShooter.ShootRotating();
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


    protected override void groundChecker_Grounded(string groundTag)
    {
        if (groundTag == "Ground")
        {
            inColor = true;
            StopMoving();
            hitGround = true;
            reachedDestination = false;
            sawPlayer = false;

            currentGroundHits++;
            timeBtwShot *= timeMod;
            
            AudioManager.instance.Play("HitWall");

            if (currentGroundHits >= maxGroundHits)
            {
                rigidbody2d.gravityScale = 1;
                Destroy(FindObjectOfType<LineRenderer>());
                Destroy(FindObjectOfType<Laser>());
            }
        }
    }


    protected override void collisionHandler_EnterContact(GameObject contact)
    {
        if (contact.tag == "Player")
        {
            StopMoving();
        }
        if (contact.tag == "Ceiling")
        {
            //inColor = true;
            if (currentGroundHits >= maxGroundHits)
            {
               OnFinished(GetPosition());
            }
            hit = true;
        }
    }

    void ChangeProjectileRotation()
    {
        projectileShooter.ChangeRotation();
    }
}
