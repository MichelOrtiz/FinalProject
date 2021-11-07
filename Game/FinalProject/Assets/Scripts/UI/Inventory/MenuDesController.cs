using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDesController : MonoBehaviour
{
    InventoryUI inventarioUI;
    
    void Start()
    {
        inventarioUI = InventoryUI.instance;
    }

    
    void Update()
    {
        float distance = Vector2.Distance(transform.position,Input.mousePosition);
        bool validPos = distance < 35;
        if(Input.GetMouseButtonDown(0) && !validPos){
            gameObject.SetActive(false);
        }
    }
    
}
