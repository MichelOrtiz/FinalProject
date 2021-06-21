using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAmongUs : MasterMinigame
{   
    private List<PoweredWireStats> wireStats;
    
    void Start()
    {
        wireStats = ScenesManagers.GetComponentsInChildrenList<PoweredWireStats>(gameObject);
    }

    void Update()
    {
        if (AllWiresConnected())
        {
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
