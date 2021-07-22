using UnityEngine;
using System;
using System.Collections.Generic;
public class ProbabilitySpawner : MonoBehaviour
{
    [SerializeField] private List<ProbabilitySpawn> probabilitySpawns;

    void Start()
    {
        SpawnAll();
    }
    void Update()
    {
        
    }

    public void SpawnAll()
    {
        foreach (var spawn in probabilitySpawns)
        {
            if (RandomGenerator.MatchProbability(spawn.probability))
            {
                List<Transform> positions = spawn.positions;
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
                    positions.Remove(spawnPos);
                    Instantiate(spawn.gameObject, spawnPos.position, spawn.gameObject.transform.rotation);
                }
            }
        }
    }
}