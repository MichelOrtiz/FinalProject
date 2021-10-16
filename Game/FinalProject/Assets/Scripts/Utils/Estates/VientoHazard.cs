using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VientoHazard : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private float damageThreshold = defaultDamageThreshold;
    public const float defaultDamageThreshold = 0; 
    [SerializeField] private State hazardState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
