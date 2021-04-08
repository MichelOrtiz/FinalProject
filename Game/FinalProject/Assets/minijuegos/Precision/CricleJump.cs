using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricleJump : MonoBehaviour
{
    int random;
    public float x, y;
    public float angle =0;
    public float speed=(2*Mathf.PI)/5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    float radius=5;
    Rigidbody2D Body;
    public Vector2 position;
    bool reloj;
    public bool moviendose;
    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Body.position = new Vector2(0,0);
        reloj = true;
    }
     void Update()
    {
        Moverse();
    }
    void Moverse(){
        if (reloj)
        {
            angle += speed*Time.deltaTime;
            if (angle>=3)
            {
                reloj=false;
                speed=(2*Mathf.PI)/Random.Range(1,5);

            }
        }else
        {
            angle -= speed*Time.deltaTime;
            if (angle<=0)
            {
                speed=(2*Mathf.PI)/Random.Range(1,5);
                reloj=true;
            }
        }
        x = Mathf.Cos(angle)*radius;
        y = Mathf.Sin(angle)*radius;
        position = new Vector2(x,y);
        Body.position = position;
    }
}
