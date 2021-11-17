using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Bosses.DesertBoss;
using UnityEngine;

public class MantisBoss : BossFight
{
    //[SerializeField] private GameObject itemToSpawn;
    //[SerializeField] private Transform spawnPos;
    //[SerializeField] private float timeToRespawnItem;
    
    //private GameObject spawnedItem;
    private List<Mantis> mantises;
    public List<Platform> allPlatforms;
    [SerializeField] private SingleSpawner singleSpawner;

    new void Start()
    {
        base.Start();
        //UpdateMantisList();
        UpdatePlatformsList();
    }

    new void Update()
    {
        /****/
        // uncomment if want to test with 'L' to change stage 
        /****/
        base.Update();

        if (ScenesManagers.GetObjectsOfType<Mantis>().Count == 0)
        {
            NextStage();
            //UpdateMantisList();
            UpdatePlatformsList();
        }
    }
    private void UpdateMantisList()
    {
        mantises =  ScenesManagers.GetObjectsOfType<Mantis>();
    }

    private void UpdatePlatformsList()
    {
        allPlatforms = ScenesManagers.GetObjectsOfType<Platform>();
    }

    public override void EndBattle()
    {
        base.EndBattle();
        Destroy(singleSpawner);
    }
}
