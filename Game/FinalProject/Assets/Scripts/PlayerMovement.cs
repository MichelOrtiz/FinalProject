using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpVelocity = 15f;
    private Rigidbody2D rigidbody2d;
    public Animator animator;
    
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Jump();
        if (Input.anyKey)
        {
            animator.SetBool("Is Walking", true);
        }
        else
        {
            animator.SetBool("Is Walking", false);

        }
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed * 5;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            rigidbody2d.velocity =  Vector2.up * jumpVelocity;
        }
    }
}