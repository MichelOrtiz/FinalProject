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
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float currentSpeed;


    [Header("Jump")]
    [SerializeField] private Vector2 jumpForce;
    [SerializeField] private Vector2 forceMultiplier;
    [SerializeField] private ForceMode2D forceMode;


    [Header("References")]
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private EnemyCollisionHandler collisionHandler;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private Rigidbody2D rigidbody2d;
    

    private PlayerManager player;
    private Entity entity;

    void Awake()
    {
        chaseSpeed = chaseSpeedMultiplier * Entity.averageSpeed;
        defaultSpeed = defaultSpeedMultiplier * Entity.averageSpeed;
        //curWaitTime = waitTime;
    }

    void Start()
    {
        player = PlayerManager.instance;
        entity = GetComponent<Entity>();
        if (entity == null)
        {
            entity = transform.parent.GetComponentInChildren<Entity>();
        }
        /*groundChecker = enemy.groundChecker;
        collisionHandler = enemy.eCollisionHandler;
        fieldOfView = enemy.FieldOfView;*/
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
            rigidbody2d.AddForce(jumpForce, forceMode);
        }
    }

    public void FollowPlayerInGround()
    {
        Vector2 direction = (Vector2) transform.position + (player.GetPosition().x > transform.position.x ? Vector2.right : Vector2.left);
        if (!groundChecker.isNearEdge && !collisionHandler.touchingPlayer && groundChecker.isGrounded)
        {
            rigidbody2d.position = Vector2.MoveTowards(transform.position, direction, chaseSpeed * Time.deltaTime);
        }
    }

    public void GoTo(Vector2 target, bool chasing)
    {
        rigidbody2d.position = Vector2.MoveTowards(transform.position, target, chasing? chaseSpeed : defaultSpeed);        
    }

    public void DefaultPatrol()
    {
        Debug.Log("default patrol");
        if (fieldOfView.inFrontOfObstacle || groundChecker.isNearEdge)
        {
            rigidbody2d.velocity = new Vector2();
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
            Debug.Log("default patrol moving");

            rigidbody2d.transform.Translate(Vector3.right * Time.deltaTime * defaultSpeed);
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
        rigidbody2d.velocity = new Vector2();
    }
}
