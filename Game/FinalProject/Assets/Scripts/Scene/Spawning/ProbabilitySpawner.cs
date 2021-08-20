using UnityEngine;
using System;
using System.Collections.Generic;
public class ProbabilitySpawner : MonoBehaviour
{
    [SerializeField] private List<ProbabilitySpawn> probabilitySpawns;
    public List<ProbabilitySpawn> ProbabilitySpawns { get => probabilitySpawns; }
    [SerializeField] private List<SpawnedObject> spawnedObjects;
    public List<SpawnedObject> SpawnedObjects {get => spawnedObjects; }

    void Awake()
    {
        spawnedObjects = new List<SpawnedObject>();
    }

    void Start()
    {
        // Spawn All
        Spawn(probabilitySpawns);
    }
    void Update()
    {
        
    }

    public void Spawn(ProbabilitySpawn spawn)
    {
        List<Transform> positions = new List<Transform> (spawn.positions);
        byte maxSpawns = (byte)positions.Count;
        byte spawns = (byte)RandomGenerator.NewRandom((byte)spawn.minQuantity, spawn.maxQuantity);
        //Debug.Log("spawns for " + spawn.gameObject + ": " + spawns);
        //if (spawns > positions.Count) continue;

        for (int i = 0; i < spawns; i++)
        {
            if (i > maxSpawns)
            {
                break;
            }
            Transform spawnPos = RandomGenerator.RandomElement<Transform>(positions);
            //spawn.SpawnedPos = spawnPos;

            GameObject instantiated = Instantiate(spawn.gameObject, spawnPos.position, spawn.gameObject.transform.rotation);
            
            spawnedObjects.Add(new SpawnedObject(instantiated, spawnPos.position));

            positions.Remove(spawnPos);
        }
    }
    public void Spawn(List<ProbabilitySpawn> probSpawns)
    {
        foreach (var spawn in probSpawns)
        {
            if (RandomGenerator.MatchProbability(spawn.probability))
            {
                Spawn(spawn);
            }
        }
    }


    public ProbabilitySpawn GetProbabilitySpawnFrom(GameObject gameObject)
    {
        var spawn = probabilitySpawns.Find(p => p.gameObject == gameObject);
        return spawn;
    }

}