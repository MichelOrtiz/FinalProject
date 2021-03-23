using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashH : Ability
{
    public Rigidbody2D body;
    private float prevGravity;
    private float timeKeyPressed;
    public float movimiento;
    public float speed;
    bool isDashing;
    float currentDashTime;
    Vector2 target;
    int nKeyPressed;
    public override void UseAbility()
    {
        base.UseAbility();
        body.velocity = new Vector2(body.velocity.x, 0f);
        body.AddForce(new Vector2(movimiento, 0f), ForceMode2D.Impulse);
        prevGravity = body.gravityScale;
        body.gravityScale = 0;
        isDashing=true;
    }

        
        protected override void Update(){
            this.enabled = isUnlocked;
            if(isDashing){
                currentDashTime += Time.deltaTime;
                if(currentDashTime >= duration){
                    currentDashTime=0;
                    isDashing = false;
                    body.gravityScale = prevGravity;
                }
            }
            else{
                if(Input.GetKeyDown(hotkey)){
                    nKeyPressed++;
                    Debug.Log("Presionado "+hotkey.ToString()+" nTimes: " + nKeyPressed);
                }
                if(!Input.GetKeyDown(hotkey)){
                    nKeyPressed = 0;
                }
                if(nKeyPressed>=2){
                    nKeyPressed=0;
                    UseAbility();
                }
            }
            
        }
        
    
}
