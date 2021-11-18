using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAmongUs : MasterMinigame
{   
    private List<PoweredWireStats> wireStats;
    [SerializeField] GameObject text;
    
    void Start()
    {
        wireStats = ScenesManagers.GetComponentsInChildrenList<PoweredWireStats>(gameObject);
    }

    void Update()
    {
        if (AllWiresConnected())
        {
            text.SetActive(true);
            OnWinMinigame();
        }
    }

    bool AllWiresConnected()
    {
        foreach (var wire in wireStats)
        {
            if (!wire.connected)
            {
                return false;
            }
        }
        return true;
    }
}
