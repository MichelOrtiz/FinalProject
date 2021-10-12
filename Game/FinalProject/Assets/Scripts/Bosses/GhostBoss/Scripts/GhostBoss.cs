using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoss : MonoBehaviour
{
    private List<Door> doors;
    private BossFight bossFight;


    private List<GameObject> currentChildren;

    [Header("Push")]
    [SerializeField] private float pushForce;
    private Vector2 direction;
    private Vector2 push;
    public bool inPush;

    void Start()
    {
        //UpdateDoorList();
        doors = ScenesManagers.GetObjectsOfType<Door>();
        bossFight = GetComponent<BossFight>();
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Door door = doors.Find(d => d.isOpen);
        /*if (door != null)
        {
            door.GetComponent<Door>().enabled = false;

            door.GetComponent<LineRenderer>().enabled = false;
            doors.Remove(door);
            Switch.AllDoors = doors;
        }*/

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


    void FixedUpdate()
    {
        if (inPush)
        {
            //rb.AddForce(push * Time.fixedDeltaTime, ForceMode2D.Force);
            currentChildren.ForEach(g => g?.GetComponent<Rigidbody2D>().AddForce(push * Time.fixedDeltaTime, ForceMode2D.Force));
        }
    }


    public void StartPush(Transform firstChild, List<GameObject> siblings)
    {
        //tempParent.position = firstChild.position;

        //firstChild.SetParent(tempParent);
        //siblings.ForEach(c => c.transform.SetParent(tempParent));

        currentChildren = siblings;

        direction = (PlayerManager.instance.GetPosition() - firstChild.position).normalized;
        push = new Vector2(direction.x * pushForce, direction.y * pushForce);

        inPush = true;
    }

    public void StopPush()
    {
        //tempParent.DetachChildren();
        inPush = false;
    }

}
