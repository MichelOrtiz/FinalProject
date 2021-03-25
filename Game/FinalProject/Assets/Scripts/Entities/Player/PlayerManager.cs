using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Entity
{
    #region Main Parameters
    public float maxStamina = 100;
    public float maxOxygen = 100;
    public float walkingSpeed;
    public float defaultwalkingSpeed = 7;
    public float defaultGravity = 2.5f;
    public float currentGravity;
    public float defaultMass = 10;
    public float currentMass;

    private GameObject[] players;
    #endregion

    #region Constant change Parameters
    public float currentStamina;
    public float currentOxygen;
    private float moveInput; 
    private float jumpTimeCounter;
    #endregion

    #region States
    public bool isRunning;
    public bool isSwiming;
    public bool isStruggling;
    public bool isImmune;
    public bool isAiming;
    public bool isDashing;
    public bool isDoubleJumping;

    #endregion

    #region Layers, rigids, etc
    public GameObject camara;
    public StaminaBar staminaBar;
    public OxygenBar oxygenBar;
    #endregion

    #region states params // might be in a different class
    private float buttonCool = 0.8f;
    private static int lButtonCount = 0;
    private static int rButtonCount = 0;
    
    [SerializeField] private float coolDownAfterAttack;

    private bool tirementRunning = false;
    private bool tirementDrowning = false;
    #endregion

    #region Dialogue Trigger
    public DialogueTrigger dialogue;
    private RaycastHit2D hit;

    [SerializeField]private bool loosingStamina;
    [SerializeField]private bool loosingOxygen;
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

    #region Inputs
        PlayerInputs inputs;
    #endregion
    [SerializeField]private State inmunityState;
    public AbilityManager abilityManager;
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
        currentOxygen = maxOxygen;
        oxygenBar.SetMaxOxygen(maxOxygen);
        FindStartPos();
        regenCooldown = 5;
        currentGravity = defaultGravity;
        currentMass = defaultMass;
        inputs=gameObject.GetComponent<PlayerInputs>();
    }

    new void Update()
    {
        rigidbody2d.mass = currentMass;
        animator.SetBool("Is Running", isRunning);
        animator.SetBool("Is Aiming", isAiming);
        isStruggling = false;
        isWalking = inputs.movementX!=0 && isGrounded;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);
        isFalling = rigidbody2d.velocity.y < - fallingCriteria;
        //UpdateAnimation();

        if (!isCaptured)
        {
            if (!isFlying && !isDashing)
            {
                rigidbody2d.gravityScale = currentGravity;
                Move();
                Jump();
            }
            else if(!isDashing)
            {
                rigidbody2d.gravityScale = 0;
                Flying();
            }
            
        }
        else
        {
            rigidbody2d.Sleep();
        }
        
        if (inputs.movementX>0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if (inputs.movementX<0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
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
        if (loosingOxygen)
        {
            if (regenCooldown > 0)
            {
                regenCooldown -= Time.deltaTime;
            }
            else
            {
                regenCooldown = 7;
                loosingOxygen = false;
            }
        }
        else
        {
            StartCoroutine(Regeneration(1f, 0.05f));
        }
        if (isInWater)
        {
            StartCoroutine(Drowning(1f, 0.05f));
        }
        base.Update();
    } 

    public void Jump()
    {
        if ((isGrounded && inputs.jump) || (isInWater && inputs.jump))
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.gravityScale + jumpForce);
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }   
        if (inputs.jump)
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
        if(isGrounded)isJumping=false;
    }

    void Move()
    {
        
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), rigidbody2d.velocity.y, 0f);  
        
        rigidbody2d.velocity = new Vector2(inputs.movementX * walkingSpeed, rigidbody2d.velocity.y);
    }

    void Flying()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        //rigidbody2d.gravityScale(isFlying? 0:26 )// no recuerdo como iba esto
        
        rigidbody2d.velocity = new Vector2(inputs.movementX, inputs.movementY)*walkingSpeed;
        
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
        if (isInWater)
        {
            tirementDrowning = true;
            yield return new WaitForSeconds(timeTired);
            TakeTirement(damage);
            tirementDrowning = false;
            yield return null;
        }
    }
    public IEnumerator Drowning(float timeDrowned, float drown)
    {
        yield return new WaitForSeconds (timeDrowned);
        if (!loosingOxygen)
        {
            if (currentOxygen>0)
            {
            currentOxygen -= drown;
            oxygenBar.SetOxygen(currentOxygen);
            }
            if (currentOxygen<0)
            {
                oxygenBar.SetOxygen(0);
            }
        }
        yield return new WaitForSeconds(timeDrowned);
    }
        /*if (isInWater)
        {
            if (currentOxygen>0)
            {
                currentOxygen -= 3;
                loosingOxygen = true;
                oxygenBar.SetOxygen(currentOxygen);
            }
            if (currentOxygen<0)
            {
                oxygenBar.SetOxygen(0);
            }
        }*/
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
        if (!isInWater)
        {
             yield return new WaitForSeconds (timeRegen);
            RegenOxygen(regen);
        }
    }
    #endregion

    // Call these methods to increase or decrease stamina directly
    #region Direct stamina changes
    public void TakeTirement(float damage)
    {
        if (currentStamina>0)
        {
            statesManager.AddState(inmunityState);
            currentStamina -= damage;
            loosingStamina = true;
            staminaBar.SetStamina(currentStamina);
        }
        if (currentStamina<0)
        {
            staminaBar.SetStamina(0);
        }
        
    }
    public void RegenStamina(float regen)
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
    void RegenOxygen(float regen)
    {
        if (currentOxygen<100)
        {
        currentOxygen += regen;
        oxygenBar.SetOxygen(currentOxygen);
        }
        if (currentOxygen>100)
        {
            oxygenBar.SetOxygen(100);
        }
    }
    #endregion



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