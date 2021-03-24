using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : Ability
{
    public Rigidbody2D body;
    private float prevGravity;
    public float speed;
    public float jumpForce;
    public override void UseAbility()
    {
        base.UseAbility();
        body.velocity = new Vector2(0f, 0f);
        body.AddForce(new Vector2(0f, jumpForce * speed), ForceMode2D.Impulse);
        player.isJumping = true;
        prevGravity = body.gravityScale;
        body.gravityScale = 0;
        isInCooldown = true;
        player.isDoubleJumping = true;
    }

    public void Jump()
    {
        
            /*jumpTimeCounter = jumpTime;
            
      
        if ((Input.GetKey(KeyCode.Space) && isJumping == true))
        {
            if (jumpTimeCounter>0){
                body.velocity = new Vector2(body.velocity.x, body.gravityScale + jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                player.isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.isJumping = false;
        }*/
    }
        
    protected override void Update(){
        if (player.isGrounded)
        {
            isInCooldown=false;
            player.isDoubleJumping = false;
        }
        if (isInCooldown)
        {
            return;   
        }
        this.enabled = isUnlocked;
        if((Input.GetKeyDown(hotkey) && !player.isGrounded)){
            UseAbility();
        }
    }
}