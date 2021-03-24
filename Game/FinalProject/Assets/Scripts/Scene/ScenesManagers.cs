using System;
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
    public static List<T> GetObjectsOfType<T>()
    {
        List<T> objects = new List<T>();
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (var gameObject in gameObjects)
        {
            if (gameObject.TryGetComponent<T>(out T obj))
            {
                objects.Add(obj);
            }
        }
        return objects;
    }
}