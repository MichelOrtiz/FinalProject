using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZone : MonoBehaviour
{
    [SerializeField] private GameObject lightZone;
    private BoxCollider2D lightCollider;
    private Door door;
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
            if (!lightCollider.enabled)
            {
                lightCollider.enabled = true;
            }
        }
        else
        {
            if (lightCollider.enabled)
            {
                lightCollider.enabled = false;
            }
        }
    }
}
