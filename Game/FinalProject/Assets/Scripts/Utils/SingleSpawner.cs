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
    
    [Obsolete ("Useless. Don't use it")]
    [Tooltip ("Useless. Don't use it")]
    [SerializeField] private bool respawnWhenNullInInventoryAndScene;

    [SerializeField] private bool respawnWhenNullInInventory;


    [SerializeField] private bool respawnBasedOnTime;
    [SerializeField] private bool independentOnTime;
    [SerializeField] private float timeToRespawn;
    private float curTimeToRespawn;
    private bool canSpawn;
    #endregion

    private PlayerManager player;
    private Inventory inventory;


    void Awake()
    {
        if (independentOnTime) respawnBasedOnTime = true;
    }

    void Start()
    {
        player = PlayerManager.instance;
        inventory = Inventory.instance;

        /*respawnHandler += HandleRespawnOptions;
        if (!independentOnTime && startWaitTime > 0)
        {
            respawnHandler += HandleRespawnTime;
        }*/
        canSpawn = true;
    }


    void Update()
    {
        if (curStartWaitTime < startWaitTime)
        {
            curStartWaitTime += Time.deltaTime;
            return;
        }
        
        if (canSpawn)
        {
            SpawnObject();
            canSpawn = false;
        }
        else
        {
            HandleRespawnOptions();
        }


    }

    void HandleRespawnOptions()
    {
        if (respawnWhenNullInScene)
        {
            if (!ScenesManagers.ExistsGameObject(spawnedObject))
            {
                HandleRespawnTime();
            }
        }
        if (respawnWhenNullInInventory)
        {
            Item item = gmObject.GetComponentInChildren<Inter>()?.item;
            if (!inventory.items.Contains(item))
            {
                HandleRespawnTime();
            }
        }
        if (independentOnTime)
        {
            HandleRespawnTime();
        }
    }

    void HandleRespawnTime()
    {
        if (respawnBasedOnTime)
        {
            curTimeToRespawn += Time.deltaTime;
            canSpawn = curTimeToRespawn >= timeToRespawn;
            if (canSpawn) curTimeToRespawn = 0;
        }
        else
        {
            canSpawn = true;
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
    