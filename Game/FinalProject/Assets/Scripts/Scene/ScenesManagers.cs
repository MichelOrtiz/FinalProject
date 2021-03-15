using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManagers : MonoBehaviour
{
    // Declare any public variables that you want to be able 
    // to access throughout your scene
   
    public PlayerManager player;
    public static ScenesManagers Instance { get; private set; } // static singleton

    void Awake() {
        if (Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Cache references to all desired variables
        player = FindObjectOfType<PlayerManager>();
    }
}