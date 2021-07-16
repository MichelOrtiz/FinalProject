using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arandaña : NormalType
{
    BoxCollider2D collider2d;
    ContactPoint2D contact;

    #region rays for ground collision
    
    [SerializeField] private float relativeGravity;
    private Vector3 gravityDirection;
    
    [SerializeField] private string verticalDirection; 
    #endregion

    protected new void Start()
    {
        base.Start();
        collider2d = GetComponent<BoxCollider2D>();
        //gravityDirection = GetPosition();
        //middleRay = Physics2D.Linecast(GetPosition(), GetPosition() + Vector3.down * 2f, 1 << LayerMask.NameToLayer("Ground"));
    }

    protected new void Update()
    {
        gravityDirection = feetPos.position;
        if (transform.rotation.z != 0 && transform.rotation.z != 180)
        {
            verticalDirection = (transform.rotation.z < 90 && transform.rotation.z > 0) ? UP:DOWN;
            if (facingDirection == RIGHT)
            {
                gravityDirection = verticalDirection == DOWN ? Vector3.left : Vector3.right;
            }
            else
            {
                gravityDirection = verticalDirection == DOWN ? Vector3.right : Vector3.left;
            }
        }
        else
        {
            verticalDirection = "";
            gravityDirection = transform.rotation.z == 0 ? Vector3.down : Vector3.up;
        }
        gravityDirection += feetPos.position;
        //gravityDirection = Vector3.MoveTowards(feetPos.position, feetPos.position + gravityDirection, relativeGravity);
        gravityDirection = Vector3.MoveTowards(feetPos.position, gravityDirection,  relativeGravity);
        Debug.DrawLine(feetPos.position, gravityDirection, Color.green);
        base.Update();
    }

    protected new void FixedUpdate()
    {
        //rigidbody2d.AddForce(averagePoint * 5f, ForceMode2D.Force);
        
        base.FixedUpdate();
    }

    protected override void MainRoutine()
    {
        if (isGrounded)
        {
            //rigidbody2d.velocity = new Vector3(feetPos.position + Vector3.down * Time.deltaTime;
            //transform.Translate(feetPos.position + gravityDirection * Time.deltaTime);

            rigidbody2d.AddForce(gravityDirection, ForceMode2D.Impulse);
            //transform.Translate(facingDirection == RIGHT? Vector3.right : Vector3.left * Time.deltaTime * normalSpeed); 
            //rigidbody2d.velocity = Vector3.MoveTowards(GetPosition(), feetPos.position + gravityDirection, relativeGravity * Time.deltaTime);
        }
        /*else
        {
            ChangeVerticalDirection();
        }*/
        
        
       
        //transform.Translate(facingDirection == RIGHT? Vector3.right : Vector3.left * Time.deltaTime * normalSpeed);

        if (fieldOfView.inFrontOfObstacle)
        {
            ChangeFacingDirection();
        }
    }
    
    private void ChangeVerticalDirection()
    {
        transform.eulerAngles = new Vector3(0, facingDirection == LEFT? 180:0, 45);
    }
}