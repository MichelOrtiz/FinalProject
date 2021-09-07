using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public float zCam;
    public Bounds Bounds;
    void Start()
    {
        Bounds = GetComponent<BoxCollider2D>().bounds;
    }

    void Update()
    {
     //4.487261   
    }
}
