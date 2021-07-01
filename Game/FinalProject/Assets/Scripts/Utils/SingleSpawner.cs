using System;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawner : MonoBehaviour
{
    #region Spawn
    [SerializeField] private GameObject gmObject;
    [SerializeField] private Vector2 position;
    [SerializeField] private float startWaitTime;
    private float curStartWaitTime;
    private GameObject spawnedObject;
    #endregion

    #region Respawn
    [SerializeField] private bool respawnWhenNullInScene;
    [SerializeField] private bool respawnWhenNullInInventoryAndScene;
    [SerializeField] private bool respawnBasedOnTime;
    [SerializeField] private float timeToRespawn;
    private float curTimeToRespawn;
    private bool spawned;
    #endregion

    private PlayerManager player;
    private Inventory inventory;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        player = PlayerManager.instance;
        inventory = Inventory.instance;
    }


    void Update()
    {
        if (curStartWaitTime <= startWaitTime)
        {
            curStartWaitTime += Time.deltaTime;
            return;
        }
        
        if (!spawned)
        {
            SpawnObject();
            spawned = true;
        }
        else
        {
            if (respawnWhenNullInScene)
            {
                //if (ScenesManagers.FindGameObject(g => g.name == gmObject.name + "(Clone)")  == null)
                if (!ScenesManagers.ExistsGameObject(spawnedObject))
                {
                    SpawnObject();
                }
            }
            if (respawnWhenNullInInventoryAndScene)
            {
                Item item = gmObject.GetComponentInChildren<Inter>()?.item;

                if (!ScenesManagers.ExistsGameObject(spawnedObject) && !inventory.items.Contains(item))
                {
                    SpawnObject();
                }
            }
            if (respawnBasedOnTime)
            {
                if (curTimeToRespawn > timeToRespawn)
                {
                    SpawnObject();
                    curTimeToRespawn = 0;
                }
                else
                {
                    curTimeToRespawn += Time.deltaTime;
                }
            }
        }


    }

    void SpawnObject()
    {
        spawnedObject = Instantiate(gmObject, position, gmObject.transform.rotation);
    }

    void OnDestroy()
    {
        if (ScenesManagers.ExistsGameObject(spawnedObject))
        {
            Destroy(spawnedObject);
        }
    }
}
    