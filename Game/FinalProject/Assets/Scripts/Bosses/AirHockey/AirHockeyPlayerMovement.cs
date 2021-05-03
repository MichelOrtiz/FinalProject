using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHockeyPlayerMovement : MonoBehaviour
{
    public bool wasJustClicked, canMove;
    public int playerSpeed = 50;
    Rigidbody2D rb;
    Vector2 centerPosition;
    public Transform BoundaryHolder;
    Boundary playerBoundary;
    [SerializeField]Collider2D playerCollider;
    public bool pegarle;
    void Start()
    {
        pegarle = false;
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
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left, playerBoundary.Right),
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Down, playerBoundary.Up));
                if (pegarle)
                {
                    rb.MovePosition(clampedMousePos);
                }else
                {
                    transform.position = Vector2.MoveTowards(transform.position, clampedMousePos, Time.deltaTime*playerSpeed);
                }
                //rb.velocity = Vector2.MoveTowards(transform.position, clampedMousePos, Time.deltaTime*50);
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
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Spit"){   
        pegarle = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        pegarle = false;
    }
}
