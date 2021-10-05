using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Instantiates 
/// </summary>
public class GameObjectCloner : MonoBehaviour
{
    [Header("Clones")]
    [SerializeField] private GameObject sourceObject;
    [SerializeField] private Transform dividePos;
    [SerializeField] private byte maxClones;
    private List<GameObject> clones;
    
    
    /// <summary>
    /// To remove this script if the instantiated clone has it
    /// </summary>
    [Tooltip("To remove this script if the instantiated clone has it")]
    [SerializeField] private bool forceRemoveNewCloner;


    [Header("Time")]
    [SerializeField] private float divisionInterval;
    private float curTime;

    void Awake()
    {
        clones = new List<GameObject>();
    }

    void Update()
    {
        if (curTime >= divisionInterval)
        {
            if (clones.Count < maxClones)
            {
                Divide();
                curTime = 0;
            }
        }
        else
        {
            curTime += Time.deltaTime;
        }
        
    }

    private void Divide()
    {
        GameObject clone = Instantiate(sourceObject, dividePos.position, Quaternion.identity);
        if (forceRemoveNewCloner)
        {
            var cloner = clone.GetComponentInChildren<GameObjectCloner>();
            if (cloner != null)
            {
                Destroy(cloner);
            }
        }
        clones.Add(clone);
    }
}