using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float moveSpeed;
    private float moveSpeedSprint;
    private float moveInput;
    public bool isGrounded;    
    public bool isJumping;
    private float jumpTimeCounter;

    public Transform FeetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpTime;
    public float jumpForce;
    public float runningSpeed;
    public float walkingSpeed;
    public Animator animator;
    public bool isFlying;
    public GameObject camara;
    public float maxStamina = 100;
    public float currentStamina;
    public StaminaBar staminaBar;
    public bool isRunning;
    public bool isStruggling;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }
    
    void FixedUpdate(){
    }

    void Update()
    {
        isStruggling = false;
        moveInput = Input.GetAxisRaw("Horizontal");
        camara.transform.position = new Vector3 (transform.position.x, transform.position.y, -10f);
        if (!isFlying)
        {
            rigidbody2d.gravityScale = 26;
            Move(); // y esto ? 
            Jump();
        }
        else
        {
            rigidbody2d.gravityScale = 0;
            Flying();
        }
        
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            isFlying = !isFlying; //proGamer move
        } // no funciona bien. no puedo moverme normalmente
        
        animator.SetBool("Is Grounded", isGrounded);//yeah
        animator.SetBool("Is Walking", moveInput!=0 && isGrounded); // Walking animation
        animator.SetBool("Is Jumping", isJumping); // Jumping animation
        animator.SetBool("Is Falling", rigidbody2d.velocity.y < -0.1);
        animator.SetBool("Is Flying", isFlying);
        if (moveInput>0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }else if (moveInput<0)
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
        isGrounded = Physics2D.OverlapCircle(FeetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
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
        else // es en otro archivo
        {
            //transform.position += movement * Time.deltaTime * moveSpeed * 5;   
            rigidbody2d.velocity = new Vector2(movement.x, movement.y)*walkingSpeed;
            isRunning = false;
        }
    }
    void Flying()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        //rigidbody2d.gravityScale(isFlying? 0:26 )// no recuerdo como iba estp no puedo hacer nada, solo flotar cuando salto
        
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
    IEnumerator Tirement(int timeTired, float damage){
        yield return new WaitForSeconds (timeTired);
        if (isRunning)
        {
            TakeTirement(damage);
        }
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
}