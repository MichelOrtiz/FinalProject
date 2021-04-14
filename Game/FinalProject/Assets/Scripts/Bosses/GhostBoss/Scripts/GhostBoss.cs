using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoss : MonoBehaviour
{
    [SerializeField] private List<Door> doors;
    void Start()
    {
        //UpdateDoorList();
        doors = ScenesManagers.GetObjectsOfType<Door>();

    }

    // Update is called once per frame
    void Update()
    {
        Door door = doors.Find(d => d.isOpen);
        if (door != null)
        {
            door.GetComponent<Door>().enabled = false;

            door.GetComponent<LineRenderer>().enabled = false;
            doors.Remove(door);
            Switch.AllDoors = doors;
        }
    }
}
