using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgePerfecto : Ability
{
    [SerializeField]private Rigidbody2D body;
    [SerializeField]protected KeyCode altHotkey;
    [SerializeField]private bool atacableee;
    private float prevGravity;
    private float timeKeyPressed;
    public float doubleTimeTap;
    public float movimiento;
    public float speed;
    protected Atacado atacado;
    float currentDashTime;
    Vector2 target;
    int nKeyPressed;
    public override void UseAbility()
    {
        if(player.currentStamina < staminaCost)return;
        base.UseAbility();
        player.isDodging = true;
        prevGravity = body.gravityScale;
        body.gravityScale = 0;
        body.velocity = new Vector2(0f,0f);
    }


    protected override void Start()
    {
        base.Start();
        body=player.gameObject.GetComponent<Rigidbody2D>();
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
        atacableee = atacado.Dodgeable;
        if(atacado.Dodgeable == true){
            Debug.Log("DodgeEable");
            if(player.isDodging){
                target = new Vector2(movimiento, 0f);
                currentDashTime += Time.deltaTime;
                if(currentDashTime >= duration){
                    currentDashTime=0;
                    body.gravityScale = prevGravity;
                    player.isDodging = false;
                    Debug.Log("DodgeEnd");
                }
            }
            else{
                if(Input.GetKeyDown(hotkey)){
                    UseAbility();
                }
                if(Input.GetKeyDown(altHotkey)){
                    UseAbility();
                }
            }
        }
    }
    private void FixedUpdate() {
        if(player.isDodging){
            body.AddForce(target * speed);
        }
    }
}
