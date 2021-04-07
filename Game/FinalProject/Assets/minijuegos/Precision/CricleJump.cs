using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricleJump : MonoBehaviour
{
    int random;
    public float x, y;
    public float angle =0;
    float speed=(2*Mathf.PI)/5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    float radius=5;
    Rigidbody2D Body;
    public Vector2 position;
    bool reloj;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Body.position = new Vector2(0,0);
        reloj=true;
    }
     void Update()
    {
        if (reloj)
        {
            angle += speed*Time.deltaTime;
            if (angle>=3)
            {
                reloj=false;
            }
        }else
        {
            angle -= speed*Time.deltaTime;
            if (angle<=0)
            {
                reloj=true;
            }
        }
        x = Mathf.Cos(angle)*radius;
        y = Mathf.Sin(angle)*radius;
        position = new Vector2(x,y);
        Body.position = position;
    }
}
