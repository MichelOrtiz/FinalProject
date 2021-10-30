using System;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public float zCam;
    public Bounds Bounds;

    public Action<ZoomCamera> EnterBounds;

    void Start()
    {
        Bounds = GetComponent<BoxCollider2D>().bounds;
    }

    void Update()
    {
     //4.487261   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnterBounds?.Invoke(this);
    }
}