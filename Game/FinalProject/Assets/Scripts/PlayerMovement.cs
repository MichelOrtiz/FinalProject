using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float moveSpeed;
    private float moveSpeedSprint;
    private float moveInput;
    private bool isGrounded;    
    public bool isJumping;
    private float jumpTimeCounter;

    public Transform FeetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpTime;
    public float jumpForce;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate(){
        moveInput = Input.GetAxisRaw("Horizontal");
        Move();
        Jump();
    }

    void Update()
    {
        if (moveInput>0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }else if (moveInput<0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(FeetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }   
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter>0){
                rigidbody2d.velocity = Vector2.up * jumpForce;
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
            //transform.position += movement * Time.deltaTime * moveSpeedSprint * 5;     
            rigidbody2d.velocity = new Vector2(movement.x, movement.y)*6;

        }else
        {
            //transform.position += movement * Time.deltaTime * moveSpeed * 5;   
            rigidbody2d.velocity = new Vector2(movement.x, movement.y)*3;
        }
    }
}