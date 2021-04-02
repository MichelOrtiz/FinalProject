using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisBoss : MonoBehaviour
{
    [SerializeField] private int timesToGiveItem;
    private List<Mantis> mantises;
    void Start()
    {
        UpdateMantisList();
    }

    void Update()
    {
        if (!mantises.Exists(m => m.timesItemGiven < timesToGiveItem))
        {
            GetComponent<BossFight>().NextStage();
            UpdateMantisList();
        }
    }


    private void UpdateMantisList()
    {
        mantises = ScenesManagers.GetObjectsOfType<Mantis>();
    }
}
