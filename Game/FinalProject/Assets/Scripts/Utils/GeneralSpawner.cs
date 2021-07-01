using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSpawner : MonoBehaviour
{
    #region Objects
    [Header("Objects")]
    [SerializeField] private List<GameObject> gameObjects;
    [SerializeField] private List<Vector2> positions;
    #endregion

    #region Respawn
    [SerializeField] private float startWaitTime;
    private float curStartWaiTime;
    [SerializeField] private bool respawnWhenNull;
    [SerializeField] private bool respawnBasedOnTime;
    [SerializeField] private float timeToRespawn;
    private float curTimeToRespawn;
    #endregion


    void Start()
    {
        
    }

    void Update()
    {
        if (curStartWaiTime <= startWaitTime)
        {
            curStartWaiTime += Time.deltaTime;
            return;
        }

        

    }

    void SpawnAllObjects()
    {
        int index = 0;
        try
        {
            foreach (var gameObject in gameObjects)
            {
                Instantiate(gameObject, positions[index], gameObject.transform.rotation);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }

    public static void SpawnSingle(GameObject gameObject, Vector2 position)
    {
        Instantiate(gameObject, position, gameObject.transform.rotation);
    }
}