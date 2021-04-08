using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colo {blue, red, yellow, green};
public class PoweredWireStats : MonoBehaviour
{
    public bool movable = false;
    public bool moving = false;
    public bool connected = false;
    public Vector3 connectedPosition;
    public Vector3 startPosition;
    public Colo objectColor;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
