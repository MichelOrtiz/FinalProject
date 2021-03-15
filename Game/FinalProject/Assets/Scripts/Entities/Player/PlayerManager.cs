using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Entity
{
    #region Main Parameters
    public float maxStamina = 100;
    public float walkingSpeed;

    private GameObject[] players;
    #endregion

    #region Dialogue Trigger
    public DialogueTrigger dialogue;
    private RaycastHit2D hit;

    [SerializeField]private bool loosingStamina;
    private float regenCooldown;

    private void SearchInteraction()
        {
            Vector2 startPoint = transform.position;
            Vector2 interactPoint = transform.position;

            if(transform.rotation.y == 0)
            {
                startPoint.x += 0.3f;
                interactPoint.x += 5f;
            }
            else
            {
                startPoint.x -= 0.3f;
                interactPoint.x -= 5f;
            }
            
            hit = Physics2D.Raycast(startPoint, interactPoint, Vector2.Distance(transform.position, interactPoint));
            if (hit.collider != null && !hit.collider.gameObject.CompareTag("Player"))
            {

                Debug.DrawLine(startPoint, interactPoint, Color.blue, 5f);
                DialogueTrigger chat = hit.collider.GetComponent<DialogueTrigger>();
                
                dialogue = chat;

                if(chat != null)
                {
                    SetFocus(chat);
                    return;
                }
                else
                {
                    RemoveFocus();
                }
            }


        }
    private void SetFocus(DialogueTrigger newFocus)
        {
            newFocus.TriggerDialogue();
        }
        private void RemoveFocus()
        {
            dialogue = null;
        }

    #endregion

    #region Constant change Parameters
    public float currentStamina;
    private float moveInput; 
    private float jumpTimeCounter;
    #endregion

    #region States
    public bool isRunning;
    public bool isStruggling;
    public bool isImmune;

    #endregion

    #region Layers, rigids, etc
    public GameObject camara;
    public StaminaBar staminaBar;
    #endregion

    #region states params // might be in a different class
    private float buttonCool = 0.8f;
    private static int lButtonCount = 0;
    private static int rButtonCount = 0;
    
    [SerializeField] private float coolDownAfterAttack;
    [SerializeField] private float immunityTime;

    private bool tirementRunning = false;
    #endregion


    public static PlayerManager instance = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    new void Start()
    {
        base.Start();
        //walkingSpeed = AverageSpeed-3f;
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        FindStartPos();
        regenCooldown = 5;
    }
    
    void FixedUpdate()
    {
    }

    new void Update()
    {
        
        isStruggling = false;
        isWalking = moveInput!=0 && isGrounded;
        
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);
        isFalling = rigidbody2d.velocity.y < - fallingCriteria;
        //UpdateAnimation();

        moveInput = Input.GetAxisRaw("Horizontal");

        if (!isCaptured)
        {
            if (!isFlying)
            {
                rigidbody2d.gravityScale = 2.5f;
                Move();
                Jump();
            }
            else
            {
                rigidbody2d.gravityScale = 0;
                Flying();
            }
            
        }
        else
        {
            rigidbody2d.Sleep();
        }
        
        
        /*animator.SetBool("Is Grounded", isGrounded);
        animator.SetBool("Is Walking", moveInput!=0 && isGrounded);
        animator.SetBool("Is Jumping", isJumping);
        animator.SetBool("Is Falling", isFalling);
        animator.SetBool("Is Flying", isFlying);*/

        if (moveInput>0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if (moveInput<0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            TakeTirement(5);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            SearchInteraction(); 
        }


        // animator.SetBool("Turn Left", moveInput<0 ); // Checks if the player turned left to start the turning animation
        if (loosingStamina)
        {
            if (regenCooldown > 0)
            {
                regenCooldown -= Time.deltaTime;
            }
            else
            {
                regenCooldown = 7;
                loosingStamina = false;
            }
        }
        else
        {
            StartCoroutine(Regeneration(1f, 0.05f));
        }
        base.Update();
    } 

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.gravityScale + jumpForce);
            isJumping = true;
            jumpTimeCounter = jumpTime;
            
        }   
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter>0){
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.gravityScale + jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    void Move()
    {
        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), rigidbody2d.velocity.y, 0f);  
        
        rigidbody2d.velocity = new Vector2(movement.x * walkingSpeed, movement.y);
    }

    void Flying()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        //rigidbody2d.gravityScale(isFlying? 0:26 )// no recuerdo como iba esto
        
        rigidbody2d.velocity = new Vector2(movement.x, movement.y)*walkingSpeed;
        
    }

    // Call these methods to decrease or increase stamina periodically
    #region Periodic stamina change
    /// <summary>
    /// Decreases a certain amount of stamina through given time
    /// </summary>
    /// <param name="timeTired"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public IEnumerator Tirement(int timeTired, float damage)
    {
        tirementRunning = true;
        yield return new WaitForSeconds(timeTired);
        TakeTirement(damage);
        tirementRunning = false;
        yield return null;
    }

    /// <summary>
    /// Increases a certain amount of stamina through given time
    /// </summary>
    /// <param name="timeTired"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    IEnumerator Regeneration(float timeRegen, float regen)
    {
        yield return new WaitForSeconds (timeRegen);
        if (!loosingStamina)
        {
            RegenStamina(regen);
        }
        yield return new WaitForSeconds(timeRegen);
    }
    #endregion

    // Call these methods to increase or decrease stamina directly
    #region Direct stamina changes
    public void TakeTirement(float damage)
    {
        isStruggling = true;
        if (currentStamina>0)
        {
            currentStamina -= damage;
            loosingStamina = true;
            staminaBar.SetStamina(currentStamina);
        }
        if (currentStamina<0)
        {
            staminaBar.SetStamina(0);
        }
    }
    void RegenStamina(float regen)
    {
        if (currentStamina<100)
        {
        currentStamina += regen;
        staminaBar.SetStamina(currentStamina);
        }
        if (currentStamina>100)
        {
            staminaBar.SetStamina(100);
        }
    }
    #endregion

    /*void Timer(int time, float damage, float regen)
    {
        if(isRunning)
        {
            StartCoroutine (Tirement(time,damage));
        }
        else
        {
            StopCoroutine (Tirement(time, damage));
            if (!isStruggling)
            {
                StartCoroutine (Regeneration(time,regen));
            }
        }
    }*/

    #region Self state methods
    public void Captured(int nTaps, float damagePerSecond)
    {
        if(!tirementRunning)
        {
            StartCoroutine(Tirement(1, damagePerSecond));
        }
        isCaptured = true;
        int halfTaps = nTaps/2;

        if (Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Horizontal") == 1)
        {
            if (buttonCool > 0 && lButtonCount >= halfTaps && rButtonCount >= halfTaps)
            {
                isCaptured = false;
                rigidbody2d.WakeUp();
                StartCoroutine(Immunity());
                lButtonCount = 0;
                rButtonCount = 0;
            }
            else
            {

                buttonCool = 0.8f;
                lButtonCount += Input.GetAxisRaw("Horizontal") ==-1? 1 : 0;
                rButtonCount += Input.GetAxisRaw("Horizontal") == 1? 1 : 0;
            }
        }
        if ( buttonCool > 0 )
        {
            buttonCool -= 1 * Time.deltaTime ;
        }
        else
        {
            rButtonCount = 0;
            lButtonCount = 0;
        }
    }

    public IEnumerator Immunity()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityTime);
        isImmune = false;
    }
    #endregion

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        isCaptured = false;
    }


    private void OnLevelWasLoaded(int level){
        base.Start();
        FindStartPos();
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length >1)
        {
            Destroy(players[1]);
        }
    }

    void FindStartPos(){
        transform.position = GameObject.FindWithTag("StartPos").transform.position; //GameObject.FindWithTag("StartPos").transform.position;
    }

}