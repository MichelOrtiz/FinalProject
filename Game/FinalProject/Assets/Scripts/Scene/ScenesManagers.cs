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

    public static List<T> GetComponentsInChildrenList<T>(GameObject gameObject)
    {
        List<T> children = new List<T>();
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform child in transforms)
        {
            if (child != transforms[0])
            {
                if (child.TryGetComponent<T>(out T obj))
                {
                    children.Add(obj);
                }
            }
        }
        return children;
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

    public static GameObject FindGameObject(Predicate<GameObject> predicate)
    {
        GameObject gameObject;
        List<GameObject> gameObjectsInScene = GetGameObjectsOfScript<Transform>();
        
        gameObject = gameObjectsInScene.Find(predicate);
        return gameObject;
    }

    public static bool ExistsGameObject(GameObject gameObject)
    {
        List<GameObject> gameObjectsInScene = GetGameObjectsOfScript<Transform>();
        
        foreach (var gameObjectInScene in gameObjectsInScene)
        {
            if (gameObjectInScene == gameObject)
            {
                return true;
            }
        }
        return false;
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

    public static bool IsFullListActive<T>(List<T> objects)
    {
        var gameObjects = FindMatchedObjects(GetObjectsOfType<T>());
        if (gameObjects != null)
        {
            return IsFullListActive(gameObjects);
        }
        return false;
    }

    // For inspector components
    public static bool IsFullListEnabled<T>(List<T> objects)
    {
        foreach (var obj in objects)
        {
            if (obj is MonoBehaviour)
            {
                var mono = obj as MonoBehaviour;
                if (!mono.enabled) return false; 
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public static void InvertListActive(List<GameObject> gameObjects)
    {
        SetListActive(gameObjects, !IsFullListActive(gameObjects));
    }
}