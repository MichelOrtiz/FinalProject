using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Instantiates 
/// </summary>
public class GameObjectCloner : MonoBehaviour
{
    [Header("Clones")]
    public GameObject sourceObject;
    [SerializeField] private Transform dividePos;
    [SerializeField] private byte maxClones;
    private List<GameObject> clones;
    
    
    /// <summary>
    /// To remove this script if the instantiated clone has it
    /// </summary>
    [Tooltip("To remove this script if the instantiated clone has it")]
    public bool forceRemoveNewCloner;


    [Header("Time")]
    [Tooltip("Enables the use of time to divide in a loop until max clones intanstiated")]
    [SerializeField] private bool loop;
    [SerializeField] private float divisionInterval;
    private float curTime;

    void Awake()
    {
        clones = new List<GameObject>();
    }

    void Update()
    {
        if (loop)
        {

            if (curTime >= divisionInterval)
            {
                if (clones.Count < maxClones)
                {
                    Divide(checkMax: true);
                    curTime = 0;
                }
            }
            else
            {
                curTime += Time.deltaTime;
            }
        }
        
    }

    public void Divide(bool checkMax)
    {
        if (checkMax)
        {
            if (clones.Count == maxClones) return;
        }
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