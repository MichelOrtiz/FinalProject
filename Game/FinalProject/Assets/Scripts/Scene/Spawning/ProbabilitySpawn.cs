using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class ProbabilitySpawn
{
    public GameObject gameObject;
    public List<Transform> positions;
    public float probability;
    public byte minQuantity;
    public byte maxQuantity;
    
    //public Transform SpawnedPos { get; set; }

    public ProbabilitySpawn(GameObject gameObject, List<Transform> positions, float probability, byte minQuantity, byte maxQuantity)
    {
        this.gameObject = gameObject;
        this.positions = positions;
        this.probability = probability;
        this.minQuantity = minQuantity;
        this.maxQuantity = maxQuantity;
    }
}