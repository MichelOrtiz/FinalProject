using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VientoScript : MonoBehaviour
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

    public void Toggle(){
        switchedOn = !switchedOn;
    }
    public void Direction(){
        clockwise = !clockwise;
    }
    public bool isOn(){
        return switchedOn;
    }

    public void OnTriggerStay2D(Collider2D other){
        if (clockwise)
        {
            other.transform.position  += Vector3.left * speed * Time.fixedDeltaTime;

        }else
        {
            other.transform.position  += Vector3.right * speed * Time.fixedDeltaTime;
            
        }
    }
    public void OnTriggerExit2D(Collider2D other){
        if (clockwise)
        {
            other.transform.position  += Vector3.left * speed * Time.fixedDeltaTime;

        }else
        {
            other.transform.position  += Vector3.right * speed * Time.fixedDeltaTime;
            
        }
    }
}
