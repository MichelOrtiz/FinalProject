using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonMiniJuegoMeduf : MonoBehaviour
{
    public DireccionMiniJuegoMeduf Direccion;
    public GameObject password;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnMouseDown(){
        password.GetComponent<password>().AddPressedCheck(Direccion);
    }
}
