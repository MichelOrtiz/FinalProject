using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDirPointer : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;

    private Vector2 mousePosition; 
    private Vector2 mouseDirection;
    public Vector2 MouseDirection { get => mouseDirection; }
    private Vector2 pointerPos;
    public Vector2 PointerDir { get => pointerPos; }
    private float angle;
    // Update is called once per frame
    void Update()
    {
        if (Pause.active) return;
        if (CameraFollow.instance.HasMouseMoved() && !Pause.active)
        {
            mousePosition = CameraFollow.instance.GetMousePosition();
            angle = MathUtils.GetAngleBetween(transform.position, mousePosition);
            mouseDirection = MathUtils.GetVectorFromAngle(angle);
            pointerPos = (Vector2)transform.position + mouseDirection * maxRadius;
            pointer.transform.position = pointerPos;
        }
    }
}
