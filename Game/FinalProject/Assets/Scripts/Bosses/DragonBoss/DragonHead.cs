using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHead : MonoBehaviour
{
    //[SerializeField]private float throwTime;
    [SerializeField] private float damage; 
    [SerializeField] private KnockbackState knockbackState;
    private State stateInstantiated;
    private float currentTime;

    private PlayerManager player;



    void Start()
    {
        player = PlayerManager.instance;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.TakeTirement(damage);
            stateInstantiated = player.statesManager.AddState(knockbackState);
            stateInstantiated.StoppedAffect += knockback_Stopped;
            player.SetImmune();
        }

        if (other.gameObject.GetComponent<ObjProjectile>())
        {
            Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.GetComponent<ObjProjectile>())
        {
            Destroy(other.gameObject);
        }
    }

    

    void knockback_Stopped()
    {
        player.SetImmune();
    }

    /*private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            currentTime+=Time.deltaTime;
            if(currentTime>throwTime){
                ThrowPlayer();
                currentTime=0;
            }
            
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            currentTime=0;
        }
    }

    void ThrowPlayer(){
        PlayerManager.instance.TakeTirement(50f);
        PlayerManager.instance.gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(-320f,0f));
        currentTime=0;
    }*/
}