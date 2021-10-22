using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashH : Ability
{
    [SerializeField]private Rigidbody2D body;
    public override KeyCode hotkey {get => PlayerManager.instance.inputs.controlBinds["MOVERIGHT"];}
    protected KeyCode altHotkey {get => PlayerManager.instance.inputs.controlBinds["MOVELEFT"];}
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
        if(player.currentStamina < staminaCost)return;
        base.UseAbility();
        player.isDashingH = true;
        prevGravity = body.gravityScale;
        body.gravityScale = 0;
        body.velocity = new Vector2(0f,0f);
    }


    protected override void Start()
    {
        base.Start();
        body=player.gameObject.GetComponent<Rigidbody2D>();
        currentDashTime = 0;
    }
    protected override void Update(){
        this.enabled = isUnlocked;
        if (isInCooldown)
        {
            time += Time.deltaTime;
            if (time >= cooldownTime)
            {
                isInCooldown = false;
                time = 0;
            }
        }
        if(player.isDashingH){
            target = new Vector2(movimiento, 0f);
            currentDashTime += Time.deltaTime;
            if(currentDashTime >= duration){
                currentDashTime=0;
                body.gravityScale = prevGravity;
                player.isDashingH = false;
                Debug.Log("DashEnd");
            }
        }
        else{
            if(timeKeyPressed!=0){
                timeKeyPressed += Time.deltaTime;
                if(timeKeyPressed >= doubleTimeTap){
                    timeKeyPressed=0;
                    nKeyPressed=0;
                }
            }
            if(Input.GetKeyDown(hotkey)){
                if(movimiento<0){
                    movimiento*=-1;
                    nKeyPressed=0;
                }
                nKeyPressed++;
                timeKeyPressed+=Time.deltaTime;
                if(nKeyPressed>=2){
                    nKeyPressed=0;
                    UseAbility();
                }
            }
            if(Input.GetKeyDown(altHotkey)){
                if(movimiento>0){
                    movimiento*=-1;
                    nKeyPressed=0;
                }
                nKeyPressed++;
                timeKeyPressed+=Time.deltaTime;
                if(nKeyPressed>=2){
                    nKeyPressed=0;
                    UseAbility();
                }
            }
            
            
        }
        
    }
    private void FixedUpdate() {
        if(player.isDashingH){
            body.AddForce(target * speed);
        }
    }
    
}
