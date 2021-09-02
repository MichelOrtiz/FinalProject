using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgePerfecto : Ability
{
    [SerializeField]private Rigidbody2D body;
    [SerializeField]private GameObject nico;
    public GameObject Atacado;
    private float prevGravity;
    private float timeKeyPressed;
    public float movimiento;
    public float speed;
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
        if(player.isDodging){
            if (nico.transform.localRotation.eulerAngles.y == 0)
            {
                target = new Vector2(movimiento, 0f);  
                Debug.Log("der");
            }else
            {
                target = new Vector2(-movimiento, 0f); 
                Debug.Log("izq");
            } 
            currentDashTime += Time.deltaTime;
            if(currentDashTime >= duration){
                currentDashTime=0;
                body.gravityScale = prevGravity;
                player.isDodging = false;
            }
            player.collisionHandler.gameObject.layer = LayerMask.NameToLayer("DodgePerfect");
            player.groundChecker.gameObject.layer = LayerMask.NameToLayer("DodgePerfect");
            player.collisionHandler.gameObject.tag = "Untagged";
            player.groundChecker.gameObject.tag = "Untagged";
        }else if(!player.isInvisible)
        {
            
            player.collisionHandler.gameObject.layer = LayerMask.NameToLayer("Default");
            player.groundChecker.gameObject.layer = LayerMask.NameToLayer("Fake");
            player.collisionHandler.gameObject.tag = "Player";
            player.groundChecker.gameObject.tag = "Player";
        }
        if(Atacado.gameObject.GetComponent<Atacado>().Dodgeable == true){
            if(player.isDodging){
                if (nico.transform.localRotation.eulerAngles.y == 0)
                {
                    target = new Vector2(movimiento, 0f);  
                    Debug.Log("der");
                }else
                {
                    target = new Vector2(-movimiento, 0f); 
                    Debug.Log("izq");
                } 
                currentDashTime += Time.deltaTime;
                if(currentDashTime >= duration){
                    currentDashTime=0;
                    body.gravityScale = prevGravity;
                    player.isDodging = false;
                }
            }
            else{
                if(Input.GetKeyDown(hotkey)){
                    if(movimiento<0){
                        movimiento*=-1;                     
                    }
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
