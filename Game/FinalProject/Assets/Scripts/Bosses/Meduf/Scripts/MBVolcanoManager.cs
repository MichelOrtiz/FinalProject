using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBVolcanoManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> waterZones;
    private int lastActiveZone;

    [SerializeField] private float timeBtwActive;
    private float curTimeBtwActive;
    /*[SerializeField] private float timeActive;
    [SerializeField] private float curTimeActive;*/

    void Awake()
    {
        foreach (var zone in waterZones)
        {
            var spore = zone.GetComponentInChildren<Spore>();
            if (spore != null)
            {
                spore.EmmissionEndedHandler += spore_EndedCollision;
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (curTimeBtwActive > timeBtwActive)
        {
            lastActiveZone = GetNextZone();
            waterZones[lastActiveZone].SetActive(true);
            curTimeBtwActive = 0;
        }
        else
        {
            curTimeBtwActive += Time.deltaTime;
        }
    }

    private int GetNextZone()
    {
        int zone = 0;
        do
        {
            zone = RandomGenerator.NewRandom(0, waterZones.Count-1);
        }
        while (zone == lastActiveZone);
        return zone;
    }

    void spore_EndedCollision(Spore spore)
    {
        spore.transform.parent.gameObject.SetActive(false);
    }
}
