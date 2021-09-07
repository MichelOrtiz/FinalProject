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
    private Vector2 pointerPos;
    public Vector2 PointerDir { get => pointerPos; }
    private float angle;

    //private Vector2 mouseDirection;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraFollow.instance.HasMouseMoved() && !Pause.active)
        {
            mousePosition = CameraFollow.instance.GetMousePosition();
            angle = MathUtils.GetAngleBetween(transform.position, mousePosition);
        }
        /*float distance = Vector2.Distance(transform.position, mousePosition);
        if (distance < maxRadius && distance > minRadius)
        {
            pointerPos = mousePosition;
        }
        else*/
        {
            mouseDirection = MathUtils.GetVectorFromAngle(angle);
            
            //if (distance < maxRadius)
            {
            //    pointerPos = (Vector2)transform.position + mouseDirection * minRadius;
            }
            //else
            {
                pointerPos = (Vector2)transform.position + mouseDirection * maxRadius;
            }
            
        }
        pointer.transform.position = pointerPos;
    }
}
