using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RandomGenerator;
public class Entity : MonoBehaviour
{
    protected const string LEFT = "left";
    protected const string RIGHT = "right";
    protected const string UP = "up";
    protected const string DOWN = "down";
    [SerializeField] public string facingDirection;


    #region Normal States
    [Header("Normal states")]
    public StateNames currentState;
    public bool isWalking = false;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isFlying = false;
    public bool isFalling = false;
    public bool isInWater = false;
    public bool isInIce = false;
    public bool isInSnow = false;
    public bool isInConvey = false;
    public bool isInDark = false;
    #endregion 

    #region Special States
    [Header("Special states")]
    public bool isParalized = false;
    public bool isCaptured = false;
    public bool isInFear = false;
    public bool isDizzy = false;
    public bool isBrainFrozen = false;
    public bool isResting = false;
    public bool isChasing = false;
    public StatesManager statesManager;
    #endregion

    #region Physic Parameters
    [Header("Physic parameters")]
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float jumpTime;
    [SerializeField] protected float fallingCriteria;
    
    // Velocidad promedio
    [SerializeField] public static float averageSpeed = 5f;
    
    #endregion
    
    #region Layers, rigids, etc...
    [Header ("General additions")]
    public Rigidbody2D rigidbody2d;
    protected Animator animator;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask[] whatIsObstacle;
    [SerializeField] public Transform feetPos;


    [SerializeField] protected float checkFeetRadius;
    public GroundChecker groundChecker;
    public CollisionHandler collisionHandler;
    #endregion

    protected virtual void collisionHandler_EnterContact(GameObject contact){}
    protected virtual void collisionHandler_StayInContact(GameObject contact){}
    protected virtual void collisionHandler_ExitContact(GameObject contact){}
    

    #region Unity stuff

    protected void Start()
    {
        facingDirection = transform.rotation.y == 0? RIGHT:LEFT;

        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        statesManager = gameObject.GetComponent<StatesManager>();

        if (collisionHandler != null)
        {
            collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterContact;
            collisionHandler.StayTouchingContactHandler += collisionHandler_StayInContact;
            collisionHandler.ExitTouchingContactHandler += collisionHandler_ExitContact;
        }
    }

    protected void Update()
    {
        facingDirection = transform.rotation.y == 0? RIGHT:LEFT;
        //isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);

        if (groundChecker != null)
        {
            isGrounded = groundChecker.isGrounded;
        }

        isFalling = rigidbody2d.velocity.y < - fallingCriteria;
        try
        {
            UpdateAnimation();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void UpdateAnimation()
    {
        if (animator != null && animator.runtimeAnimatorController)
        {
            animator.SetBool("Is Grounded", isGrounded);
            animator.SetBool("Is Walking", isWalking);
            animator.SetBool("Is Falling", isFalling);
            animator.SetBool("Is Jumping", isJumping);
            animator.SetBool("Is Flying", isFlying);
            animator.SetBool("Is Paralized", isParalized);
            animator.SetBool("Is Captured", isCaptured);
            animator.SetBool("Is In Fear", isInFear);
            animator.SetBool("Is Brain Frozen", isBrainFrozen);
            animator.SetBool("Is Resting", isResting);
            animator.SetBool("Is Chasing", isChasing);
            
        }
    }

    public Vector3 GetPosition()
    {
        //return rigidbody2d.position;
        return this.transform.position;
    }

    #endregion

    #region Self state methods

    public void Push(float xForce, float yForce)
    {
        //rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0f);
        Vector2 force = GetPosition() * new Vector2(xForce, yForce);
        rigidbody2d.AddForce(force, ForceMode2D.Force);
    }


    protected bool RayHitObstacle(Vector2 start, Vector2 end)
    {
        foreach (var obstacle in whatIsObstacle)
        {
            if (Physics2D.Linecast(start, end, obstacle))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Rotates the enity Y axis
    /// </summary>
    protected void ChangeFacingDirection()
    {
        transform.eulerAngles = new Vector3(0, facingDirection == LEFT? 0:180);
    }
    
    #endregion
}