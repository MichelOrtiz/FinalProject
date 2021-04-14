using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoss : MonoBehaviour
{
    private List<Door> doors;
    void Start()
    {
        doors = ScenesManagers.GetObjectsOfType<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var door in doors)
        {
            if (door.isOpen)
            {
                doors.Remove(door);
                Destroy(door.GetComponentInParent<GameObject>());
            }
        }    
    }
}
