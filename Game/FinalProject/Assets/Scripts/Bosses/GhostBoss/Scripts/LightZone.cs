using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZone : MonoBehaviour
{
    [SerializeField] private GameObject lightZone;
    private BoxCollider2D lightCollider;
    private Door door;
    private GhostBossEnemy ghost;

    void Start()
    {
        lightCollider = lightZone.GetComponent<BoxCollider2D>();
        door = GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        if (door.isOpen)
        {
            /*if (!lightCollider.enabled)
            {
                lightCollider.enabled = true;
            }*/
            if (!lightZone.activeSelf)
            {
                lightZone.SetActive(true);
            }
        }
        else
        {
            /*if (lightCollider.enabled)
            {
                lightCollider.enabled = false;
            }*/
            if (lightZone.activeSelf)
            {
                lightZone.SetActive(false);
            }
        }
    }

    public void UnableDoor()
    {
        door.enabled = false;
        lightCollider.enabled = false;
        gameObject.SetActive(false);
    }
}
