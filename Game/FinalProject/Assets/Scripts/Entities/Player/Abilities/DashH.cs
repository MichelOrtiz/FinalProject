using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashH : Ability
{
    public Rigidbody2D body;
    private float prevGravity;
    private float timeKeyPressed;
    public float doubleTimeTap;
    public float movimiento;
    public float speed;
    float currentDashTime;
    Vector2 target;
    int nKeyPressed;
    public override void UseAbility()
    {
        base.UseAbility();
        player.isDashing=true;
        body.velocity = new Vector2(body.velocity.x, 0f);
        body.AddForce(new Vector2(movimiento * speed, 0f), ForceMode2D.Impulse);
        prevGravity = body.gravityScale;
        body.gravityScale = 0;
        isInCooldown = true;
    }

        
        protected override void Update(){
            if (isInCooldown)
            {
                time += Time.deltaTime;
                if (time >= cooldownTime)
                {
                    isInCooldown = false;
                    time = 0;
                }
            }
            this.enabled = isUnlocked;
            if(player.isDashing){
                currentDashTime += Time.deltaTime;
                if(currentDashTime >= duration){
                    currentDashTime=0;
                    player.isDashing = false;
                    body.gravityScale = prevGravity;
                }
            }
            else{
                if(timeKeyPressed!=0){
                    timeKeyPressed+=Time.deltaTime;
                    if(timeKeyPressed>=doubleTimeTap){
                        timeKeyPressed=0;
                        nKeyPressed=0;
                    }
                }
                if(Input.GetKeyDown(hotkey)){
                    nKeyPressed++;
                    timeKeyPressed+=Time.deltaTime;
                    //Debug.Log("Presionado "+hotkey.ToString()+" nTimes: " + nKeyPressed);
                }
                
                if(nKeyPressed>=2){
                    nKeyPressed=0;
                    UseAbility();
                }
            }
            
        }
        
    
}
