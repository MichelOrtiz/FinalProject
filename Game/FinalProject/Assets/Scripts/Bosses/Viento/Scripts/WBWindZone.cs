using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WBWindZone : MonoBehaviour
{
    [SerializeField] private float interval;
    private float currentInterval;
    [SerializeField] private float timeActive;
    private float currentTimeActive;
    [Header("Groups")]
    [SerializeField] private List<GameObject> groupA;
    [SerializeField] private bool groupAIsActive;
    [SerializeField] private List<GameObject> groupB;
    [SerializeField] private bool groupBIsActive;

    private bool invertedZoneActive;


    void Start()
    {
        ScenesManagers.SetListActive(groupA, groupAIsActive);
        ScenesManagers.SetListActive(groupB, groupBIsActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInterval > interval)
        {
            if (!invertedZoneActive)
            {
                ScenesManagers.InvertListActive(groupA);
                ScenesManagers.InvertListActive(groupB);

                groupAIsActive = ScenesManagers.IsFullListActive(groupA);
                groupBIsActive = ScenesManagers.IsFullListActive(groupB);


                invertedZoneActive = true;
            }
            if (currentTimeActive > timeActive)
            {
                currentInterval = 0;
                currentTimeActive = 0;
                invertedZoneActive = false;
            }
            else
            {
                currentTimeActive += Time.deltaTime;
            }
        }
        else
        {
            currentInterval += Time.deltaTime;
        }
    }

    /*void ActivateWindZone(List<GameObject> group)
    {
        foreach (var gameObject in group)
        {
            gameObject.SetActive(true); 
        }
    }

    void DeactivateWindZone(List<GameObject> group)
    {
        foreach (var gameObject in group)
        {
            gameObject.SetActive(false); 
        }
    }

    void GroupActive(List<GameObject> group)
    {
        foreach (var gameObject in group)
        {
            
        }
    }*/
}
