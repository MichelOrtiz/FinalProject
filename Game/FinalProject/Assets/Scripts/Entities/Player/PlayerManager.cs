using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimap;

public class PlayerManager : Entity
{
    #region Main Parameters
    public float maxStamina = 100;
    public float maxOxygen = 100;
    public float walkingSpeed;
    public float defaultwalkingSpeed = 7;
    public const float defaultGravity = 2.5f;
    public float currentGravity;
    public float defaultMass = 10;
    public float currentMass;
    public GameObject dodgePerectCollider;
    public const float defaultDmgMod = 1;
    public float dmgMod {get;set;}
    //private GameObject[] players;
    #endregion

    #region Constant change Parameters
    public float currentStamina {get;set;}
    public float currentOxygen {get;set;}
    private float moveInput; 
    private float jumpTimeCounter;
    #endregion

    #region States
    //public bool isRunning;
    public bool isSwiming;
    public bool isStruggling;
    public bool isImmune;
    public bool isAiming;
    public bool isDashingH;
    public bool isDashingV;
    public bool isDoubleJumping;
    public bool isDodging;
    public bool DeathActive;

    public bool isInvisible;
    #endregion

    #region Layers, rigids, etc
    public GameObject camara;
    public GameObject Darkness;
    #endregion

    #region states params // might be in a different class

    private bool tirementRunning = false;
    private bool tirementDrowning = false;
    #endregion
    [SerializeField]private bool loosingStamina;
    [SerializeField]private bool loosingOxygen;
    private float regenCooldown;
    private bool MinimapaActivada;
    ConveyScript convey;

    #region Dialogues
        private RaycastHit2D hit;
        private void SearchInteraction()
        {
            Vector2 startPoint = transform.position;
            Vector2 interactPoint = transform.position;

            if(transform.rotation.y == 0)
            {
                startPoint.x += 0.6f;
                interactPoint.x += 2f;
            }
            else
            {
                startPoint.x -= 0.6f;
                interactPoint.x -= 2f;
            }
            
            hit = Physics2D.Raycast(startPoint, transform.forward, Vector2.Distance(startPoint,interactPoint));
            if (hit.collider != null)
            {
                DialogueTrigger trigger = hit.collider.GetComponent<DialogueTrigger>();
                if(trigger!=null){
                    trigger.TriggerDialogue();
                }
            }
        }
    #endregion

    #region Inputs
    public PlayerInputs inputs;
    #endregion
    public State inmunityState;
    public AbilityManager abilityManager;
    public static PlayerManager instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance==null)instance=this;
        else if(instance!=this)Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    new void Start()
    {
        base.Start();
        currentStamina = maxStamina;
        currentOxygen = maxOxygen;
        //FindStartPos();
        regenCooldown = 5;
        currentGravity = defaultGravity;
        currentMass = defaultMass;
        dmgMod = defaultDmgMod;
        inputs = gameObject.GetComponent<PlayerInputs>();
        MinimapaActivada = true;

        GunProjectile.instance.ObjectShot += gun_ObjectShot;
    }

    new void Update()
    {
        SetAnimationStates();
        rigidbody2d.mass = currentMass;
        /*animator.SetBool("Is Running", isRunning);
        animator.SetBool("Is Aiming", isAiming);*/
        isStruggling = false;
        isWalking = inputs.movementX!=0 && isGrounded && !isRunning;
        //isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);
        isFalling = rigidbody2d.velocity.y < 0f;
        //UpdateAnimation();

        if (!isCaptured)
        {
            if (!isFlying && !isDashingH && !isDashingV)
            {
                rigidbody2d.gravityScale = currentGravity;
                Move();
                Jump();
            }
            else if(!isDashingH && !isDashingV)
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
        if(DeathActive){
            if (currentStamina<1)
            {
                FindRespawnPos();
                currentStamina = maxStamina;
                currentOxygen = maxOxygen;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MinimapaActivada = !MinimapaActivada;
            if (MinimapaActivada)
            {
                MinimapWindow.instance?.Show();
            }else{
                MinimapWindow.instance?.Hide();
            }
        }
        base.Update();
    } 

    public void Jump()
    {
        if(!inputs.enabled)return;
        if ((isGrounded && inputs.jump) || (isInWater && inputs.jump))
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.gravityScale + jumpForce);
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }   
        if (inputs.jump && isJumping)
        {
            //animationManager.RestartAnimation();

            if (jumpTimeCounter>0)
            {
                //animationManager.RestartAnimation("Nico_jump");
                animationManager.RestartAnimation();
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.gravityScale + jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            //isFalling = true;
            isJumping = false;
        }
        //if(isGrounded)isJumping=false;
    }

    void Move()
    {
        if(!inputs.enabled)return;
        if (isInIce)
        {
            rigidbody2d.AddForce(new Vector2(inputs.movementX * walkingSpeed, rigidbody2d.velocity.y));
        }
        else
        {
            rigidbody2d.velocity = new Vector2(inputs.movementX * walkingSpeed, rigidbody2d.velocity.y);    
        }

        
    }

    void Flying()
    {
        if(!inputs.enabled)return;
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
        }else{
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
            }
            if (currentOxygen<0)
            {
                currentOxygen = 0;
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
            currentStamina -= damage * dmgMod;
            loosingStamina = true;
        }
        if (currentStamina<0)
        {
            currentStamina = 0;
        }
        
    }
    public void RegenStamina(float regen)
    {
        if (currentStamina<100)
        {
        currentStamina += regen;
        }
        if (currentStamina>100)
        {
            currentStamina = 100;
        }
    }
    void RegenOxygen(float regen)
    {
        if (currentOxygen<100)
        {
        currentOxygen += regen;
        }
        if (currentOxygen>100)
        {
            currentOxygen = 100;
        }
    }
    #endregion


    public void SetImmune()
    {
        statesManager.AddStateDontRepeat(inmunityState);
    }
/*
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
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
        //transform.position = loadlevel.instance.startPosition.transform.position;
    }*/
    void FindRespawnPos()
    {
        try
        {
            var spawnPos = GameObject.FindWithTag("RespawnPos").transform.position;
            if (spawnPos != null)
            {
                transform.position = spawnPos;
            }
        }
        catch (System.NullReferenceException)
        {
            
        }
    }


    #region Animatioooon
    // Not proud of this but what can I do
    void SetAnimationStates()
    {
        if (isAiming)
        {
            animationManager.ChangeAnimation("Nico_pre_pointing");
        }
        else if (animationManager.previousState != "Nico_pre_pointing")
        {
            if (isGrounded && !isFlying)
            {
                if (isWalking)
                {
                    animationManager.ChangeAnimation("Nico_walk");
                }
                else if (isRunning)
                {
                    animationManager.ChangeAnimation("Nico_run");
                }
                else 
                {
                    animationManager.ChangeAnimation("Nico_idle");
                }
            }
            else
            {
                if (isJumping)
                {
                    animationManager.ChangeAnimation("Nico_jump");
                }
                else if (isFlying)
                {
                    animationManager.ChangeAnimation("Nico_fly");
                }
                else if (isFalling)
                {
                    animationManager.ChangeAnimation("Nico_fall");
                }
            }
        }
    }

    void gun_ObjectShot()
    {
        Debug.Log("object shot");
        animationManager.ChangeAnimation("Nico_throw");
        animationManager.SetNextAnimation("Nico_idle");
    }

    #endregion
}