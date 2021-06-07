using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBMinigameManager : MonoBehaviour
{
    [SerializeField] private List<AccessMinigame> minigameAccess;
    [SerializeReference] private AccessMinigame currentMinigameAccess;
    [SerializeReference] private byte index;
    private bool assignedEvent;

    // Start is called before the first frame update
    void Start()
    {
        SetNextMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMinigameAccess.MasterMinigame != null)
        {
            // so it is only assigned once per object
            if (!assignedEvent)
            {
                currentMinigameAccess.MasterMinigame.WinMinigameHandler += access_WinMinigame;
                assignedEvent = true;
            }
        }
    }

    void SetNextMinigame()
    {
        Destroy(currentMinigameAccess);
        currentMinigameAccess = Instantiate(minigameAccess[index], transform);
        assignedEvent = false;
    }

    void access_WinMinigame()
    {
        index++;
        SetNextMinigame();
    }
}
