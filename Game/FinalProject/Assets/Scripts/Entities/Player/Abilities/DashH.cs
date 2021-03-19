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
    Vector2 target;
    int nKeyPressed;
    public override void UseAbility()
    {
        base.UseAbility();
            prevGravity = body.gravityScale;
            target = new Vector2(player.GetPosition().x + movimiento, player.GetPosition().y);
            //body.gravityScale = 0;
            body.velocity = target * speed;
            timeKeyPressed=0;
    }

        
        protected override void Update(){
            this.enabled = isUnlocked;
            if(nKeyPressed>=2){
                nKeyPressed=0;
                timeKeyPressed=0;
                UseAbility();
            }
            if(timeKeyPressed!=0f){
                timeKeyPressed+=Time.deltaTime;
            }
            if(timeKeyPressed>=duration){
                nKeyPressed=0;
                timeKeyPressed=0;
            }

            if(Input.GetKeyDown(hotkey)){
                timeKeyPressed+=Time.deltaTime;
                nKeyPressed++;
                Debug.Log("Intentando usar dash " + nKeyPressed);
            }

            if((Vector2)player.GetPosition() == target){
                target = new Vector2();
                body.gravityScale = prevGravity;
            }
        }
        
    
}
