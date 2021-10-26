using UnityEngine;
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
    public bool isRunning = false;
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
    public Animator animator;
    public AnimationManager animationManager;
    [SerializeField] protected LayerMask[] whatIsObstacle;
    [SerializeField] public Transform feetPos;


    public GroundChecker groundChecker;
    public CollisionHandler collisionHandler;
    public SomePhysics physics;

    public Transform emotePos;
    
    #endregion

    protected virtual void collisionHandler_EnterContact(GameObject contact){}
    protected virtual void collisionHandler_StayInContact(GameObject contact){}
    protected virtual void collisionHandler_ExitContact(GameObject contact){}
    protected virtual void groundChecker_Grounded(string groundTag){}
    protected virtual void groundChecker_ExitGround(){}

    protected void Awake()
    {
        statesManager = gameObject.GetComponent<StatesManager>();
        
        statesManager?.StopAll();
    }

    #region Unity stuff
    protected void Start()
    {
        if (transform.rotation.z != 0)
        {
            facingDirection = transform.rotation.x == 0? RIGHT:LEFT;
        }
        else
        {
            facingDirection = transform.rotation.y == 0? RIGHT:LEFT;
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (rigidbody2d == null)
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        if (collisionHandler != null)
        {
            collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterContact;
            collisionHandler.StayTouchingContactHandler += collisionHandler_StayInContact;
            collisionHandler.ExitTouchingContactHandler += collisionHandler_ExitContact;
        }

        if (groundChecker != null)
        {
            groundChecker.GroundedHandler += groundChecker_Grounded;
            groundChecker.ExitGroundHandler += groundChecker_ExitGround;
        }

    }

    protected void Update()
    {
        if (transform.rotation.z != 0)
        {
            facingDirection = transform.rotation.x == -1? LEFT:RIGHT;
        }
        else
        {
            var rotation = Mathf.RoundToInt(transform.rotation.y);
            facingDirection = rotation == 0? RIGHT:LEFT;
        }

        if (groundChecker != null)
        {
            isGrounded = groundChecker.isGrounded;
        }

        isFalling = rigidbody2d.velocity.y < - fallingCriteria;
    }


    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    #endregion

    #region Self state methods
    protected void  ChangeFacingDirection()
    {
        var rotation = Mathf.RoundToInt(transform.eulerAngles.z);
        if (rotation == 0 || rotation == 180)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x , transform.eulerAngles.y + 180, rotation);
        }
        else 
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x + 180, transform.eulerAngles.y, rotation);
        }

    }
    
    #endregion


    #region Physics related
    public void Push(float xForce, float yForce)
    {
        //rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0f);
        Vector2 force = GetPosition() * new Vector2(xForce, yForce);
        rigidbody2d.AddForce(force, ForceMode2D.Force);
    }

    public void Knockback(float knockbackDuration, float knockbackForce, Vector2 knockbackDir)
    {
        physics.StartKnockback(knockbackDuration, knockbackForce, knockbackDir);
    }

    public float GetJumpForce(){
        return jumpForce;
    }
    public void SetJumpForce(float newJumpForce){
        jumpForce = newJumpForce;
    }

    #endregion

    public void DestroyEntity()
    {
        EntityDestroyFx.Instance.StartDestroyFx(this);
        Destroy(gameObject);
    }
}