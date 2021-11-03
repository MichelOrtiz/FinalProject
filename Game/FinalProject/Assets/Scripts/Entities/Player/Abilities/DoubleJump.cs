using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : Ability
{
    public override KeyCode hotkey {get => PlayerManager.instance.inputs.controlBinds["MOVEJUMP"];}
    public Rigidbody2D body;
    private float prevGravity;
    public float speed;
    public float jumpForce;
    public override void UseAbility()
    {
        base.UseAbility();
        if(player.currentStamina < staminaCost + 0.1f)return;
        body.velocity = new Vector2(0f, 0f);
        body.velocity = new Vector2(body.velocity.x, body.gravityScale + jumpForce);
        //body.AddForce(new Vector2(0f, jumpForce * speed), ForceMode2D.Impulse);
        player.isJumping = true;
        prevGravity = body.gravityScale;
        body.gravityScale = 0;
        isInCooldown = true;
        player.isDoubleJumping = true;
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
        if((Input.GetKeyDown(hotkey) && !player.isJumping)){
            UseAbility();
        }
    }
}