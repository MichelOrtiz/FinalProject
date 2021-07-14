using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyMovement : MonoBehaviour
{
    [Header("Main")]
    [SerializeReference] private string facingDirection;


    [Header("Time")]
    [SerializeField] private float waitTime;
    private float curWaitTime;


    [Header("Speed")]
    [SerializeField] private float defaultSpeedMultiplier;
    [SerializeReference] private float defaultSpeed;
    [SerializeField] private float chaseSpeedMultiplier;
    public float ChaseSpeedMultiplier 
    { 
        get { return chaseSpeedMultiplier; }
        set { chaseSpeedMultiplier = value; }
    }
    
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float currentSpeed;


    [Header("Jump")]
    [SerializeField] private Vector2 jumpForce;
    [SerializeField] private Vector2 jumpForceMultiplier;
    [SerializeField] private ForceMode2D forceMode;


    [Header("References")]
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private EnemyCollisionHandler collisionHandler;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private Rigidbody2D rigidbody2d;
    

    private PlayerManager player;
    private Entity entity;

    public void SetChaseSpeed(float speed)
    {
        chaseSpeedMultiplier = speed;
        chaseSpeed = chaseSpeedMultiplier * Entity.averageSpeed;
    }

    void Awake()
    {
        chaseSpeed = chaseSpeedMultiplier * Entity.averageSpeed;
        defaultSpeed = defaultSpeedMultiplier * Entity.averageSpeed;
        if (jumpForceMultiplier.magnitude > 0)
        {
            jumpForce *= jumpForceMultiplier;
        }
        curWaitTime = waitTime;
        if (groundChecker != null)
        {
            groundChecker.GroundedHandler += groundChecker_Grounded;
        }
    }

    void Start()
    {
        player = PlayerManager.instance;
        entity = GetComponent<Entity>();
        if (entity == null)
        {
            entity = transform.parent.GetComponentInChildren<Entity>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        facingDirection = entity.facingDirection;
    }

    void FixedUpdate()
    {
        
    }


    public void Jump()
    {
        if (groundChecker.isGrounded)
        {
            Vector2 jumpDirection = new Vector2(facingDirection == "right"? jumpForce.x : - jumpForce.x, jumpForce.y) * rigidbody2d.gravityScale;
            rigidbody2d.AddForce(jumpDirection, forceMode);
        }
    }

    public void GoToInGround(Vector2 target, bool chasing, bool checkNearEdge)
    {
        Vector2 direction = (Vector2) rigidbody2d.position + (target.x > rigidbody2d.transform.position.x ? Vector2.right : Vector2.left);
        float speed = chasing? chaseSpeed : defaultSpeed;
        if (checkNearEdge)
        {
            if (!groundChecker.isNearEdge && groundChecker.isGrounded)
            {
                rigidbody2d.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            }
        }
        else
        {
            if ( groundChecker.isGrounded || groundChecker.isNearEdge)
            {
                rigidbody2d.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            }
        }
    }

    public void GoTo(Vector2 target, bool chasing, bool gravity)
    {
        rigidbody2d.position = Vector2.MoveTowards(entity.GetPosition(), target, Time.deltaTime * (chasing? chaseSpeed : defaultSpeed) * (gravity ? rigidbody2d.gravityScale : 1) );   
        //rigidbody2d.transform.position += (Vector3)target * Time.deltaTime * defaultSpeed;
    }

    public void Translate(Vector2 target, bool chasing)
    {
        rigidbody2d.transform.position += (Vector3)target * (chasing ? chaseSpeed : defaultSpeed) * Time.deltaTime; 
    }

    public void Translate(Vector2 target, float speed)
    {
        rigidbody2d.transform.position += (Vector3)target * speed * Time.deltaTime; 
    }

    public void DefaultPatrol()
    {
        if (groundChecker.isGrounded && (groundChecker.isNearEdge || fieldOfView.inFrontOfObstacle))
        {
            
            //StopMovement();
            if (curWaitTime > 0)
            {
                //isWalking = false;
                curWaitTime -= Time.deltaTime;
                return;
            }
            ChangeFacingDirection();
            curWaitTime = waitTime;
        }
        else
        {
            //GoToInGround(Vector2.right, chasing: false, checkNearEdge: true);
            //rigidbody2d.transform.Translate(Vector2.right * Time.deltaTime * defaultSpeed);
            rigidbody2d.transform.position += rigidbody2d.transform.right * Time.deltaTime * defaultSpeed;
            //isWalking = true;
        }
    }

    public void DefaultPatrol(string groundTag)
    {
        if (groundChecker.isGrounded && (groundChecker.IsNearEdge(groundTag) || fieldOfView.inFrontOfObstacle))
        {
            if (curWaitTime > 0)
            {
                curWaitTime -= Time.deltaTime;
                return;
            }
            ChangeFacingDirection();
            curWaitTime = waitTime;
        }
        else
        {
            rigidbody2d.transform.position += rigidbody2d.transform.right * Time.deltaTime * defaultSpeed;
        }
    }


    public void DefaultPatrolInAir()
    {
        if (fieldOfView.inFrontOfObstacle)
        {
            StopMovement();
            if (curWaitTime > 0)
            {
                //isWalking = false;
                curWaitTime -= Time.deltaTime;
                return;
            }
            ChangeFacingDirection();
            curWaitTime = waitTime;
        }
        else
        {
            //rigidbody2d.transform.position += rigidbody2d.transform.forward * Time.deltaTime * defaultSpeed;
            rigidbody2d.transform.Translate(Vector2.right * Time.deltaTime * defaultSpeed);
            //isWalking = true;
        }
    }

    /// <summary>
    /// Rotates the enity Y axis
    /// </summary>
    public void ChangeFacingDirection()
    {
        entity.transform.eulerAngles = new Vector3(0, facingDirection == "left"? 0:180);
    }

    public void StopMovement()
    {
        if (!entity.isFalling)
        {
            rigidbody2d.velocity = new Vector2();
        }
    }
    public void StopAllMovement()
    {
        rigidbody2d.velocity = new Vector2();
    }
    
    void groundChecker_Grounded(string groundTag)
    {
        rigidbody2d.velocity = new Vector2();
    }
}
