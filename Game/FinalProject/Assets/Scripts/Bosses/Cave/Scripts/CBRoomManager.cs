using System.Collections.Generic;
using UnityEngine;

public class CBRoomManager : MonoBehaviour
{
    public List<GameObject> walls;

    public List<GameObject> grounds;

    private PlayerManager player;

    public GameObject currentPlayerWall;

    void Start()
    {
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var contact in player.collisionHandler.Contacts)
        {
            if (walls.Contains(contact) || grounds.Contains(contact))
            {
                currentPlayerWall = contact;
            }
        }
    }
}
