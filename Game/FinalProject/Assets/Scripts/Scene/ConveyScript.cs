using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyScript : MonoBehaviour
{
    PlayerManager player;
    public bool switchedOn = true;
    public bool clockwise = true;
    public float speed;
    

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    /*void FixedUpdate(){
        if(!rb) return;
        
        Vector2 position = rb.position;
        if (clockwise)
        {
            rb.position += Vector2.left * speed * Time.fixedDeltaTime;
            if(transform.localScale.x == -speed) transform.localScale = new Vector3(speed, transform.localScale.y, transform.localScale.x);
        }else
        {
            rb.position += Vector2.right * speed * Time.fixedDeltaTime;
            if(transform.localScale.x == speed) transform.localScale = new Vector3(-speed, transform.localScale.y, transform.localScale.x);
        }
        rb.MovePosition(position);
    }*/
    public void Toggle(){
        switchedOn = !switchedOn;
    }
    public void Direction(){
        clockwise = !clockwise;
    }
    public bool isOn(){
        return switchedOn;
    }

    public void OnCollisionStay2D(Collision2D other){
        if (clockwise)
        {
            other.transform.position  += Vector3.left * speed * Time.fixedDeltaTime;

        }else
        {
            other.transform.position  += Vector3.right * speed * Time.fixedDeltaTime;
            
        }
    }
    public void OnCollisionExit2D(Collision2D other){
        if (clockwise)
        {
            other.transform.position  += Vector3.left * speed * Time.fixedDeltaTime;

        }else
        {
            other.transform.position  += Vector3.right * speed * Time.fixedDeltaTime;
            
        }
    }
}
