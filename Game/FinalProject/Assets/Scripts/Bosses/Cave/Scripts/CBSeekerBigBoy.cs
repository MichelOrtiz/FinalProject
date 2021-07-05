using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBSeekerBigBoy : CaveBossBehaviour
{
    private bool canStart;

    #region OnPlayerEffects
    [Header("Effects on Player")]
    [SerializeField] private State effectOnPlayer;
    [SerializeField] private float damageAmount;

    #endregion
    #region Physics
    [Header("Physic Params")]
    [SerializeField] private float speedMultiplier;
    private float speed;

    [SerializeField] private float pushForce;
    private Vector2 push;
    public bool inPush;
    public RoomComponent currentPush;
    private bool stopped;

    [SerializeField] private float timeBeforePush;
    private float currentTimeBeforePush; 

    private float defaultGravityScale;
    #endregion

    #region Collisions
    [Header("Collisions")]
    [SerializeField] private CBRoomManager roomManager;
    private List<GameObject> walls;
    private List<GameObject> grounds;

    private bool hitWall;
    [SerializeField] private float waitTimeWhenCollide;
    private float currentWaitTime;

    [SerializeField] private float distanceFromPlayerToStop;

    private bool inWall;
    public bool inSameWallThanPlayer;
    public bool inGroundWithPlayer;

    // To change stage
    [SerializeField] private byte maxProjectileHits;
    [SerializeField]private byte projectileHits;



    //private bool touchingGround;
    #endregion

    #region ShockWave
    [SerializeField] private GameObject shockWake;
    #endregion


    private Vector2 lastPlayerPosition;
    private Vector2 direction;


    

    // Might inherit later *-*-*-*-*-*
    private PlayerManager player;
    private EnemyCollisionHandler eCollisionHandler;

    void Awake()
    {
        eCollisionHandler = (EnemyCollisionHandler) collisionHandler;
        //eCollisionHandler.TouchingGroundHandler += eCollisionHandler_TouchingGround;
        eCollisionHandler.EnterTouchingContactHandler += eCollisionHandler_EnterCollision;
        eCollisionHandler.ExitTouchingContactHandler += eCollisionHandler_ExitCollision;

        eCollisionHandler.StayTouchingContactHandler += eCollisionHandler_StayInCollision;

        eCollisionHandler.TouchingPlayerHandler += eCollisionHandler_TouchingPlayer;


        groundChecker.GroundedHandler += groundChecker_Grounded;
        //groundChecker.GroundedGameObjectHandler += groundChecker_GroundedGameObject;


        speed = averageSpeed * speedMultiplier;

       
    }

    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        defaultGravityScale = rigidbody2d.gravityScale;

        if (roomManager == null)
        {
            roomManager = ScenesManagers.FindObjectOfType<CBRoomManager>();
        }

        walls = roomManager.walls;
        grounds = roomManager.grounds;

        inPush = true;
    }

    void HandlePreStart()
    {
        canStart = false;
        while(currentTimeBeforePush <= timeBeforePush)
        {
            currentTimeBeforePush += Time.deltaTime;
        }
        currentTimeBeforePush = 0;
        canStart = true;
    }

    // Update is called once per frame
    new void Update()
    {
        
            
        if (hitWall)
        {
             
            if (currentWaitTime > waitTimeWhenCollide)
            {
                hitWall = false;
                currentWaitTime = 0;
            }
            else
            {
                currentWaitTime += Time.deltaTime;
                return;
            }
        }

        if (!inPush)
        {

            if (inGroundWithPlayer)
            {
                SetPushOn(RoomComponent.Ground);
            }
            else if (inSameWallThanPlayer)
            {
                SetPushOn(RoomComponent.Wall);
            }
            else
            {
                SetPushOn(RoomComponent.Air);
            }
            inPush = true;

        }
        /*if (!player.collisionHandler.Contacts.Exists(c => c.tag == "Ground"))
        {
            inSameWallThanPlayer = false;
        }*/


        base.Update();
    }

    
    void FixedUpdate()
    {
        if (canStart)
        {
            if (inPush)
            {
                rigidbody2d.AddForce(push * Time.deltaTime, ForceMode2D.Impulse);
                HandleStopPushing();
            }
        }
    }

    bool DistancedFromPlayer()
    {
        return Vector2.Distance(GetPosition(), player.GetPosition()) > distanceFromPlayerToStop;
    }

    void StopMoving()
    {
        hitWall = true;
        inPush = false;
        rigidbody2d.velocity = new Vector2();
    }

    void SetPushOn(RoomComponent roomComponent)
    {
        currentPush = roomComponent;

        lastPlayerPosition = player.GetPosition();
        direction = ((Vector3)lastPlayerPosition - GetPosition()).normalized;

        switch (roomComponent)
        {
            case RoomComponent.Ground:
                push = new Vector2(direction.x * pushForce * speed, 0);
                break;
            case RoomComponent.Wall:
                inWall = true;
                push = new Vector2(0, direction.y * pushForce * speed);
                break;
            default:
                push = new Vector2(direction.x * pushForce * speed, direction.y * jumpForce *(rigidbody2d.gravityScale > 0?  rigidbody2d.gravityScale : defaultGravityScale));
                break;
        }
    }

    void HandleStopPushing()
    {
        Vector2 vel = transform.rotation * rigidbody2d.velocity;

        if (inPush)
        {
            switch (currentPush)
            {
                case RoomComponent.Ground:
                    if (inGroundWithPlayer && inSameWallThanPlayer)
                    {
                        StopMoving();
                    }
                    if (vel.x > 0)
                    {
                        if (GetPosition().x > player.GetPosition().x && DistancedFromPlayer() )
                        {
                            StopMoving();
                        }
                    }
                    else
                    {
                        if (GetPosition().x < player.GetPosition().x && DistancedFromPlayer())
                        {
                            StopMoving();
                        }
                    }
                    break;
                case RoomComponent.Wall:
                    if (inGroundWithPlayer)
                    {
                        StopMoving();
                    }
                    if (vel.y > 0)
                    {
                        if (GetPosition().y > player.GetPosition().y && DistancedFromPlayer())
                        {
                            StopMoving();
                        }
                    }
                    else
                    {
                        if (GetPosition().y < player.GetPosition().y && DistancedFromPlayer())
                        {
                            StopMoving();
                        }
                    }
                    break;
            }

        }

        if (currentPush == RoomComponent.Wall && inGroundWithPlayer)
        {
            inPush = false;
        }
    }


    void eCollisionHandler_EnterCollision(GameObject contact)
    {

        // Stops the enemy if touches any of the walls
        
        if (walls.Contains(contact))
        {
            if (inPush)
            {
                if (inSameWallThanPlayer || currentPush == RoomComponent.Air)
                {
                    StopMoving();

                }
            }
            if (!grounds.Contains(contact))
            {
                rigidbody2d.gravityScale = 0;
            }
            
        }
        

        if (contact.tag == "Ceiling")
        {
            inColor = true;
            spriteRenderer.color = colorWhenHit;
            if (projectileHits < maxProjectileHits-1)
            {
                projectileHits++;
            }
            else
            {
                OnFinished(GetPosition());
            }
        }

    }

    void eCollisionHandler_StayInCollision(GameObject contact)
    {
        if (walls.Contains(contact))
        {
            inSameWallThanPlayer = player.collisionHandler.Contacts.Contains(contact);
        }
        if (grounds.Contains(contact))
        {
            inGroundWithPlayer = isGrounded && player.collisionHandler.Contacts.Contains(contact);
        }

    }

    void eCollisionHandler_ExitCollision(GameObject contact)
    {

        // Stops the enemy if touches any of the walls
        
        if (walls.Contains(contact))
        {
            rigidbody2d.gravityScale = defaultGravityScale;
        }
        inWall = false;
        inGroundWithPlayer = false;

        if (!isGrounded)
        {
            shockWake.SetActive(false);
        }
    }

    void groundChecker_Grounded(string groundTag)
    {
        if (groundTag == "Ground")
        {
            StopMoving();
            HandlePreStart();
            shockWake.SetActive(true);
        }
    }

    void eCollisionHandler_TouchingPlayer()
    {
        player.TakeTirement(damageAmount);
        //player.statesManager.AddState(effectOnPlayer);
    }

}
