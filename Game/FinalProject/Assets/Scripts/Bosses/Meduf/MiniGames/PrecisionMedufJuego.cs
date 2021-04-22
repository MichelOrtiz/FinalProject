using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecisionMedufJuego : MonoBehaviour
{
    int random, randomreloj;
    public float x, y;
    public float angle =0;
    public float speed=(2*Mathf.PI)/5;
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
            random=Random.Range(1,3);
            Body.rotation = x;
            x+=random;
            if (x>randomreloj)
            {
                randomreloj=Random.Range(-500,-750);
                reloj = false;
            }
        }else
        {
            random=Random.Range(1,3);
            Body.rotation = x;
            x-=random;
            if (x<randomreloj)
            {
                randomreloj=Random.Range(500,750);
                reloj = true;
            }
        }
    }
}

