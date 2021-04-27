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

    public static List<GameObject> GetGameObjectsOfScript<T>()
    {
        List<T> objects = GetObjectsOfType<T>();
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        List<GameObject> gameObjects = new List<GameObject>();

        foreach (var gameObject in allGameObjects)
        {
            if (gameObject.GetComponent<T>() != null)
            {
                gameObjects.Add(gameObject);
            }
        }

        return gameObjects;
    }

    public static List<T> ArrayToList<T>(T[] array)
    {
        List<T> objects = new List<T>();
        foreach (var element in array)
        {
            objects.Add(element);
        }
        return objects;
    }

    public static List<GameObject> FindMatchedObjects(List<GameObject> objects)
    {
        List<GameObject> result = new List<GameObject>();
        List<GameObject> sceneObjects = new List<GameObject>(FindObjectsOfType<GameObject>());
        foreach (var obj in objects)
        {
            if (sceneObjects.Contains(obj))
            {
                result.Add(sceneObjects[sceneObjects.IndexOf(obj)]);
            }
        }
        return result;
    }


    public static List<GameObject> FindMatchedObjects<T>(List<T> objects)
    {
        List<GameObject> result = new List<GameObject>();
        List<GameObject> scriptObjects = GetGameObjectsOfScript<T>();
        foreach (var sc in scriptObjects)
        {
            if (sc.TryGetComponent<T>(out T obj))
            {
                if (objects.Contains(obj))
                {
                    result.Add(sc);
                }
            }
        }
        return result;
    }

    public static void SetListActive(List<GameObject> gameObjects, bool active)
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(active);
            }
        }
    }

   /* public static void SetScriptActive<MonoBehaviour>(GameObject gameObject)
    {
        if (gameObject.GetComponent<MonoBehaviour>() != null)
        {
            gameObject.GetComponent<MonoBehaviour>().enabled = false;
        }
    }*/

    public static bool IsFullListActive(List<GameObject> gameObjects)
    {
        return !gameObjects.Exists(g => !g.activeInHierarchy);
    }

    public static void InvertListActive(List<GameObject> gameObjects)
    {
        SetListActive(gameObjects, !IsFullListActive(gameObjects));
    }
}