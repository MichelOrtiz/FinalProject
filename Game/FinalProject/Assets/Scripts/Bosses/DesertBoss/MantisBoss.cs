using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisBoss : BossFight
{
    [SerializeField] private GameObject itemToSpawn;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float timeToRespawnItem;
    [SerializeField] private int timesToGiveItem;
    private GameObject spawnedItem;
    private float currentTime;
    private List<Mantis> mantises;
    private Inventory inventory;
    private bool pickedUpItem;

    new void Start()
    {
        base.Start();
        inventory = Inventory.instance;
        spawnPos.position = currentStage.positions[currentStage.gameObjects.IndexOf(itemToSpawn)];
        UpdateMantisList();
    }

    new void Update()
    {
        if (!mantises.Exists(m => m.timesItemGiven < timesToGiveItem))
        {
            NextStage();
            UpdateMantisList();
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


        base.Update();
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
}
