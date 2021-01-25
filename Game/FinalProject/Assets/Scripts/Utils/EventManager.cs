using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private const float coolDownAfterAttack = 2f;
    
    public static event EventHandler AfterPlayerCaptured;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void EventManager_AfterPlayerCaptured(object sender, EventArgs e)
    {
         
    }
}
