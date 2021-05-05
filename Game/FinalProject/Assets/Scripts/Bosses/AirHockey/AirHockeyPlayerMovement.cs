using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHockeyPlayerMovement : MonoBehaviour
{
    public bool wasJustClicked, canMove, pegarle, overPuck, agarrado, frozen;
    public int playerSpeed = 50;
    Rigidbody2D rb;
    Vector2 centerPosition;
    public Transform BoundaryHolder;
    Boundary playerBoundary;
    [SerializeField]Collider2D playerCollider;
    Vector2 mousePos;

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
        overPuck = false;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (playerCollider.OverlapPoint(mousePos))
        {
            overPuck = true;
        }
        else
        {
            overPuck = false;
        }
        if (Input.GetMouseButton(0))
        {
            if (wasJustClicked)
            {
                wasJustClicked = false;
                if (playerCollider.OverlapPoint(mousePos))
                {
                    canMove = true;
                    overPuck = true;
                }
                else
                {
                    canMove = false;
                }
            }
            if (canMove)
            {
                if (playerCollider.OverlapPoint(mousePos))
                {
                    canMove = true;
                    overPuck = true;
                    agarrado = true;
                }else
                {
                    canMove = false;
                    agarrado = false;
                }
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left, playerBoundary.Right),
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Down, playerBoundary.Up));
                if (pegarle)
                {
                    rb.MovePosition(clampedMousePos);
                }else
                {
                    transform.position = Vector2.MoveTowards(transform.position, clampedMousePos, Time.deltaTime*playerSpeed);
                }if (agarrado == true)
                {
                    StartCoroutine(Congelamiento());

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
    private IEnumerator Congelamiento(){
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (playerCollider.OverlapPoint(mousePos))
        {
            yield return new WaitForSecondsRealtime(.1f);
            if (playerCollider.OverlapPoint(mousePos))
            {
                canMove = false;
                agarrado = false;
                frozen = true;
                StartCoroutine(Congelado());
            }
        }
        frozen = false;
    }
    private IEnumerator Congelado(){
        
        yield return new WaitForSecondsRealtime(2);
        canMove = true;
        agarrado = true;
        frozen = false;
    }
}
