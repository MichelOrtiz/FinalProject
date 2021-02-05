using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RandomGenerator;
public class Entity : MonoBehaviour
{
    protected const string LEFT = "left";
    protected const string RIGHT = "right";

    #region Normal States
    public bool isWalking = false;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isFlying = false;
    public bool isFalling = false;
    #endregion

    #region Special States
    [SerializeField] protected State state;
    public bool isParalized = false;
    public bool isCaptured = false;
    public bool isInFear = false;
    public bool isDizzy = false;
    public bool isBrainFrozen = false;
    public bool isResting = false;
    public bool isChasing = false;
    #endregion

    #region Physic Parameters
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float jumpTime;
    [SerializeField] protected float fallingCriteria;
    
    // Velocidad promedio
    [SerializeField] protected const float averageSpeed = 5f;
    
    #endregion
    
    #region Layers, rigids, etc...
    public Rigidbody2D rigidbody2d;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsObstacle;
    [SerializeField] protected Transform feetPos;    
    [SerializeField] protected float checkFeetRadius;
    #endregion

    #region Unity stuff
    protected void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);
        isFalling = rigidbody2d.velocity.y < - fallingCriteria;
        UpdateAnimation();
    }

    public void UpdateAnimation()
    {
        animator.SetBool("Is Walking", isWalking);
        animator.SetBool("Is Paralized", isParalized);
        animator.SetBool("Is Captured", isCaptured);
        animator.SetBool("Is In Fear", isInFear);
        animator.SetBool("Is Brain Frozen", isBrainFrozen);
        animator.SetBool("Is Resting", isResting);
        animator.SetBool("Is Chasing", isChasing);
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    #endregion

    #region Self state methods

    // not working
    public void Fear()
    {
        isInFear = true;
        StopAllCoroutines();
        var jumpForce = RandomGenerator.NewRandom(2, 7);
        var jumps = RandomGenerator.NewRandom(2, 5);

        //Vector3 vector;
        for (int i = 0; i < jumps; i++)
        {
            var x = RandomGenerator.NewRandom(-5, 5);
            if (isGrounded)
            {
                rigidbody2d.velocity = new Vector3(rigidbody2d.velocity.x, jumpForce);
            }
        }
        isInFear = false;
    }
    #endregion
}