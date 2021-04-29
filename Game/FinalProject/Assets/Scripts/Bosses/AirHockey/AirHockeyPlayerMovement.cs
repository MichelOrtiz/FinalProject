using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHockeyPlayerMovement : MonoBehaviour
{
    public bool wasJustClicked, canMove;
    int i;
    Rigidbody2D rb;
    Vector2 centerPosition;
    public Transform BoundaryHolder;
    Boundary playerBoundary;
    Collider2D playerCollider;
    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        wasJustClicked = true; 
        wasJustClicked = false; 
        rb = GetComponent<Rigidbody2D>();
        centerPosition = rb.position;
        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (wasJustClicked)
            {
                wasJustClicked = false;
                if (playerCollider.OverlapPoint(mousePos))
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
            if (canMove)
            {
                while (i<10000)
                {
                    i++;
                }
                i=0;
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left, playerBoundary.Right),
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Down, playerBoundary.Up));
                rb.MovePosition(clampedMousePos);
            }
        }
        else
        {
            wasJustClicked = true;
        }
    }
    public void  CenterPosition(){
        rb.position = centerPosition;
    }
}
