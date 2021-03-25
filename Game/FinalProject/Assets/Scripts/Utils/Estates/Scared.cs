using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Paralisis", menuName = "States/new Scare")]
public class Scared : State
{
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpInterval;
    private float timeBetwenJumps;
    public override void Affect(){
        currentTime += Time.deltaTime;
        timeBetwenJumps += Time.deltaTime;
        if(currentTime >= duration){
            StopAffect();
        }
        if(timeBetwenJumps>=jumpInterval){
            int random = RandomGenerator.NewRandom(0,1);
            if(random==0){
                if(manager.hostEntity.isGrounded){
                    //make jump
                    Rigidbody2D rb = manager.hostEntity.GetComponent<Rigidbody2D>();
                    rb.AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
                }
            }
            timeBetwenJumps=0;
        }
        
    }
    
}
