using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHead : MonoBehaviour
{
    [SerializeField]private float throwTime;
    private float currentTime;
    private void OnCollisionStay2D(Collision2D other) {
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
    }
}