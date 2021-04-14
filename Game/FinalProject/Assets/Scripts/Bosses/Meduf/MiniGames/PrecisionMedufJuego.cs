using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecisionMedufJuego : MonoBehaviour
{
    int random;
    public float x, y;
    public float angle =0;
    public float speed=(2*Mathf.PI)/5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    float radius=5;
    [SerializeField] Rigidbody2D Body;
    public Vector2 position;
    bool reloj;
    public bool moviendose;
    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        reloj = true;
    }
     void Update()
    {
        Moverse();
    }
    void Moverse(){
        if (reloj)
        {
            Body.rotation = x;
            x++;
        }else
        {
            Body.rotation = y;
            y--;
        }
    }
}

