using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Entity
{
    #region Main Parameters
    public float maxStamina = 100;
    public float runningSpeed;

    #endregion

    #region Constant change Parameters
    public float currentStamina;
    private float moveInput; 
    private float jumpTimeCounter;
    #endregion

    #region States
    public bool isRunning;
    public bool isStruggling;
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
    public bool isImmune;

    private bool tirementRunning = false;
    #endregion


    
    


    new void Start()
    {
        base.Start();

        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }
    
    void FixedUpdate(){
    }

    new void Update()
    {
        
        isStruggling = false;
        
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);
        isFalling = rigidbody2d.velocity.y < - fallingCriteria;
        //UpdateAnimation();

        moveInput = Input.GetAxisRaw("Horizontal");

        camara.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);

        if (!isCaptured)
        {
            if (!isFlying)
            {
                rigidbody2d.gravityScale = 26;
                Move();
                Jump();
            }
            else
            {
                rigidbody2d.gravityScale = 0;
                Flying();
            }
            
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                isFlying = !isFlying;
            }
        }
        else
        {
            rigidbody2d.Sleep();
        }
        
        
        animator.SetBool("Is Grounded", isGrounded);//yeah
        animator.SetBool("Is Walking", moveInput!=0 && isGrounded); // Walking animation
        animator.SetBool("Is Jumping", isJumping); // Jumping animation
        animator.SetBool("Is Falling", isFalling);
        animator.SetBool("Is Flying", isFlying);

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
        Timer(1,.01f,.005f);
        // animator.SetBool("Turn Left", moveInput<0 ); // Checks if the player turned left to start the turning animation
        
    } 

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
            isJumping = true;
            jumpTimeCounter = jumpTime;
            
        }   
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter>0){
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else{
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
        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);   
        if(Input.GetKey(KeyCode.LeftShift)){
            if(currentStamina>10){
            //transform.position += movement * Time.deltaTime * moveSpeedSprint * 5;     
                rigidbody2d.velocity = new Vector2(movement.x, movement.y)*runningSpeed;
                isRunning = true;
            }
            else
            {
                rigidbody2d.velocity = new Vector2(movement.x, movement.y)*walkingSpeed;
                isRunning = true;
            }
        }
        else
        {
            //transform.position += movement * Time.deltaTime * moveSpeed * 5;   
            rigidbody2d.velocity = new Vector2(movement.x, movement.y)*walkingSpeed;
            isRunning = false;
        }
    }
    void Flying()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        //rigidbody2d.gravityScale(isFlying? 0:26 )// no recuerdo como iba esto
        
        rigidbody2d.velocity = new Vector2(movement.x, movement.y)*walkingSpeed;
        
    }
    void TakeTirement(float damage){
        isStruggling = true;
        if (currentStamina>0)
        {
            currentStamina -= damage;
            staminaBar.SetStamina(currentStamina);
        }
        if (currentStamina<0)
        {
            staminaBar.SetStamina(0);
        }
    }
    void RegenStamina(float regen){
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
    public IEnumerator Tirement(int timeTired, float damage)
    {
        tirementRunning = true;
        yield return new WaitForSeconds (timeTired);
        TakeTirement(damage);
        tirementRunning = false;
        yield return null;
    }

    IEnumerator Regeneration(int timeRegen, float regen){
        
        yield return new WaitForSeconds (timeRegen);
        if (!isRunning)
        {
            RegenStamina(regen);
        }
        yield return new WaitForSeconds(timeRegen);
    }

    void Timer(int time, float damage, float regen){
        if(isRunning){
            StartCoroutine (Tirement(time,damage));
        }
        else{
            StopCoroutine (Tirement(time, damage));
            if (!isStruggling)
            {
                StartCoroutine (Regeneration(time,regen));
            }
        }
    }

    public void Captured(int nTaps, int damagePerSecond)
    {
        if(!tirementRunning)
        {
            StartCoroutine(Tirement(1, damagePerSecond));
        }
        isCaptured = true;
        int halfTaps = nTaps/2;
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        Debug.Log($"left: {lButtonCount} right: {rButtonCount}");

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

    private IEnumerator Immunity()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityTime);
        isImmune = false;
    }

}