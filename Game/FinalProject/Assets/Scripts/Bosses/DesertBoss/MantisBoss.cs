using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisBoss : MonoBehaviour
{
    [SerializeField] private GameObject itemToSpawn;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float timeToRespawnItem;
    
    private GameObject spawnedItem;
    private float currentTime;
    private List<Mantis> mantises;
    private Inventory inventory;
    private bool pickedUpItem;

    public List<Platform> allPlatforms;

    void Start()
    {
        inventory = Inventory.instance;
        UpdateMantisList();
        UpdatePlatformsList();
    }

    void Update()
    {
        /****/
        // uncomment if want to test with 'L' to change stage 
        // UpdatePlatformsList();
        /****/
        if (ScenesManagers.GetObjectsOfType<Mantis>().Count == 0)
        {
            GetComponent<BossFight>().NextStage();
            UpdateMantisList();
            UpdatePlatformsList();
            spawnPos.position = GetComponent<BossFight>().currentStage.positions[GetComponent<BossFight>().currentStage.gameObjects.IndexOf(itemToSpawn)];
        }

        if (!SpawnedItem())
        {
            if (currentTime > timeToRespawnItem)
            {
                spawnedItem = Instantiate(itemToSpawn, spawnPos.position, Quaternion.identity);
                currentTime = 0;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    bool SpawnedItem()
    {
        if (spawnedItem != null)
        {
            return ScenesManagers.GetObjectsOfType<Inter>().Contains(spawnedItem.GetComponent<Inter>());
        }
        return false;
    }
    private void UpdateMantisList()
    {
        mantises = ScenesManagers.GetObjectsOfType<Mantis>();
    }

    private void UpdatePlatformsList()
    {
        allPlatforms = ScenesManagers.GetObjectsOfType<Platform>();
    }
}
