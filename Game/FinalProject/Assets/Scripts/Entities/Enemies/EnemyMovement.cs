using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyMovement : MonoBehaviour
{
    [Header("Main")]
    [SerializeReference] private string facingDirection;
    [SerializeReference] private bool movingHorizontal;
    [SerializeReference] private bool movingVertical;


    [Header("Time")]
    [SerializeField] private float waitTime;
    public float WaitTime { get => waitTime;  }
    private float curWaitTime;


    [Header("Speed")]
    [SerializeField] private float defaultSpeedMultiplier;
    [SerializeReference] private float defaultSpeed;
    public float DefaultSpeed
    {
        get { return defaultSpeed; }
        set { defaultSpeed = value; }
    }
    [SerializeField] private float chaseSpeedMultiplier;
    [SerializeField] private float chaseSpeed;
    public float ChaseSpeed
    {
        get { return chaseSpeed; }
        set { chaseSpeed = value; }
    }
    [SerializeField] private float currentSpeed;


    [Header("Jump")]
    [SerializeField] private Vector2 jumpForce;
    public Vector2 JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }
    
    [SerializeField] private Vector2 jumpForceMultiplier;
    [SerializeField] private ForceMode2D forceMode;

    [SerializeField] private KnockbackState jumpState;


    [Header("Push")]
    [SerializeField] private Vector2 pushForce;
    public Vector2 PushForce
    {
        get { return pushForce; }
        set { pushForce = value; }
    }
    
    [SerializeField] private Vector2 pushMultiplier;
    [SerializeField] private ForceMode2D pushForceMode;

    


    [Header("References")]
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private EnemyCollisionHandler collisionHandler;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private Rigidbody2D rigidbody2d;
    private Rigidbody2D tempRigidbody;
    private Vector2 patrolDestination;
    private Vector2 startPosition;
    

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
        if (pushMultiplier.magnitude > 0)
        {
            pushForce *= pushMultiplier;
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

    void Update()
    {
        facingDirection = entity.facingDirection;
    }

    void FixedUpdate()
    {
        Vector3 vel = transform.rotation * rigidbody2d.velocity;
        movingHorizontal = vel.x != 0;
        movingVertical = vel.y != 0;
    }


    public void Jump()
    {
        if (groundChecker.isGrounded)
        {
            Vector2 jumpDirection = new Vector2(facingDirection == "right"? jumpForce.x : - jumpForce.x, jumpForce.y) * rigidbody2d.gravityScale;
            rigidbody2d.AddForce(jumpDirection, forceMode);
        }
    }

    public void JumpByKnockback()
    {
        if (groundChecker.isGrounded)
        {
            var jump = Instantiate(jumpState);
            entity.statesManager.AddState(jump);
        }
    }


    public void GoToInGround(Vector2 target, bool chasing, bool checkNearEdge)
    {
        float speed = chasing? chaseSpeed : defaultSpeed;
        GoToInGround(target, speed, checkNearEdge);
    }

    public void GoToInGround(Vector2 target, float speed, bool checkNearEdge)
    {
        Vector2 direction = (Vector2) rigidbody2d.position + (target.x > rigidbody2d.transform.position.x ? Vector2.right : Vector2.left);
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

    public void GoTo(Vector2 target, float speed, bool gravity)
    {
        rigidbody2d.position = Vector2.MoveTowards(entity.GetPosition(), target, Time.deltaTime * speed );   
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

    public void PushTo(Vector2 target)
    {
        rigidbody2d.AddForce(target * pushForce * Time.deltaTime, forceMode);
    }

    public void DefaultPatrol()
    {
        if (groundChecker.isGrounded && (groundChecker.isNearEdge || fieldOfView.inFrontOfObstacle))
        {
            
            //StopMovement();
            if (curWaitTime > 0)
            {
                //isWalking = false;
                StopMovement();

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
            entity.animationManager?.ChangeAnimation("walk");

            rigidbody2d.transform.position += rigidbody2d.transform.right * Time.deltaTime * defaultSpeed;
        }
    }

    public void DefaultPatrol(string groundTag)
    {
        if (groundChecker.isGrounded && (groundChecker.IsNearEdge(groundTag) || fieldOfView.inFrontOfObstacle))
        {
            if (curWaitTime > 0)
            {
                //entity.animationManager.ChangeAnimation("idle");
                StopMovement();
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

    public void DefaultPatrol(bool checkNearEdge, bool checkInFrontOfObstacle)
    {
        if (groundChecker.isGrounded && (checkNearEdge? groundChecker.isNearEdge : false || checkInFrontOfObstacle? fieldOfView.inFrontOfObstacle : false))
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

    public void AddForce(float force, Vector2 direction, ForceMode2D forceMode)
    {
        rigidbody2d.AddForce(direction * force, ForceMode2D.Force);
    }


    public void ChangeFacingDirection()
    {
        var _transform = entity.transform;
        var rotation = Mathf.RoundToInt(_transform.eulerAngles.z);
        if (rotation == 0 || rotation == 180)
        {
            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x , _transform.eulerAngles.y + 180, rotation);
        }
        else 
        {
            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x + 180, _transform.eulerAngles.y, rotation);
        }
    }

    public void StopMovement()
    {
        if (!entity.isFalling)
        {
            entity.animationManager?.ChangeAnimation("idle");
            StopAllMovement();
        }
    }
    public void StopAllMovement()
    {
        //entity.animationManager?.ChangeAnimation("idle");

        rigidbody2d.velocity = Vector3.zero;
        rigidbody2d.angularVelocity = 0;

        /*if (movingHorizontal || movingVertical)
        {
            StartCoroutine(ChangeToKinematicRigidbody(0.2f));
        }*/
    }

    /*private IEnumerator ChangeToKinematicRigidbody(float seconds)
    {
        var oldDrag = rigidbody2d.
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(seconds);
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }*/
    private IEnumerator ChangeToKinematicRigidbody(float seconds)
    {
        var oldDrag = rigidbody2d.drag;
        var oldMass = rigidbody2d.mass;
        var oldSpeed = rigidbody2d.velocity;
        var oldRot = rigidbody2d.angularVelocity;
        
        rigidbody2d.isKinematic = true;  
        
        yield return new WaitForSeconds(seconds);

        rigidbody2d.isKinematic = false;
        rigidbody2d.drag = oldDrag;
        rigidbody2d.mass = oldMass;
        rigidbody2d.velocity = oldSpeed;
        rigidbody2d.angularVelocity = oldRot;
    }
    
    void groundChecker_Grounded(string groundTag)
    {
        StopAllMovement();
        startPosition = entity.GetPosition();
    }
}
