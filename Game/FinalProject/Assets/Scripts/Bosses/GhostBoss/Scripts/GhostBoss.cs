using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoss : MonoBehaviour
{
    private List<Door> doors;
    private BossFight bossFight;
    void Start()
    {
        //UpdateDoorList();
        doors = ScenesManagers.GetObjectsOfType<Door>();
        bossFight = GetComponent<BossFight>();
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

        if (!bossFight.isCleared)
        {
            if (ScenesManagers.GetObjectsOfType<GhostBossEnemy>().Count == 0)
            {
                /*List<SeekerProjectile> projectiles = ScenesManagers.GetObjectsOfType<SeekerProjectile>();
                foreach (var projectile in projectiles)
                {
                    Destroy(projectile);
                }*/
                bossFight.EndBattle();
            }
        }
    }
}
