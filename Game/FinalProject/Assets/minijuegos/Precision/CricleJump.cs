using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricleJump : MonoBehaviour
{
    int random;
    public float x, y;
    public float angle =0;
    public float speed=(2*Mathf.PI)/5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    [SerializeField] float radius=300;
    [SerializeField] Transform center;
    [SerializeField] Transform diana;
    Rigidbody2D Body;
    public Vector2 position;
    bool reloj;
    public bool moviendose;
    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Body.position = new Vector2(806, 226);
        radius = Vector2.Distance(diana.position,center.position);
        reloj = true;
    }
     void Update()
    {
        Moverse();
    }
    void Moverse(){
        if (reloj)
        {
            angle += speed*Time.unscaledDeltaTime;
            if (angle>=3)
            {
                reloj=false;
                speed=(2*Mathf.PI)/Random.Range(1,5);

            }
        }else
        {
            angle -= speed*Time.unscaledDeltaTime;
            if (angle<=0)
            {
                speed=(2*Mathf.PI)/Random.Range(1,5);
                reloj=true;
            }
        }
        x = Mathf.Cos(angle)*radius + center.position.x;
        y = Mathf.Sin(angle)*radius + center.position.y;
        Debug.Log("x: " + x);
        Debug.Log("y: " + y);
        position = new Vector2(x,y);
        transform.position = position;
    }
}
