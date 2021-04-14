using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class password : MonoBehaviour
{
    public int direccion, cant;
    public string x;
    [SerializeField] ArrayList contraseña;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown(){
        switch (direccion)
        {
            case 1:
                x="Arriba";
                cant++;
            break;
            case 2:
                x="Derecha";
                cant++;
            break;
            case 3:
                x="Izquierda";
                cant++;
            break;    
            case 4:
                x="Abajo";
                cant++;
            break;
        }
    }
}
