using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHockeyPlayerMovement : MonoBehaviour
{
    public bool wasJustClicked, canMove, pegarle, overPuck, agarrado, frozen, congelando;
    public int playerSpeed = 50;
    Rigidbody2D rb;
    Vector2 centerPosition;
    public Transform BoundaryHolder;
    Boundary playerBoundary;
    [SerializeField]Collider2D playerCollider;
    [SerializeField] private Collider2D mouseCollider;
    Vector2 mousePos;
    public float offset;

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
        frozen = false;
    }

    void Update()
    {
        if (!frozen)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mouseCollider.OverlapPoint(mousePos))
            {
                overPuck = true;
                if (Input.GetMouseButton(0))
                {
                    if (wasJustClicked)
                    {
                        wasJustClicked = false;
                        if (mouseCollider.OverlapPoint(mousePos))
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
                        if (mouseCollider.OverlapPoint(mousePos))
                        {
                            canMove = true;
                            overPuck = true;
                            agarrado = true;
                        }else
                        {
                            canMove = false;
                            agarrado = false;
                        }
                            Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x + Random.Range(offset, -offset), playerBoundary.Left, playerBoundary.Right),
                                                            Mathf.Clamp(mousePos.y + Random.Range(offset, -offset), playerBoundary.Down, playerBoundary.Up));
                        if (pegarle)
                        {
                            rb.MovePosition(clampedMousePos);
                        }else
                        {
                            transform.position = Vector2.MoveTowards(transform.position, clampedMousePos, Time.deltaTime*playerSpeed);
                        }
                        if (agarrado == true && !congelando)
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
            else
            {
                overPuck = false;
            }
            
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
            congelando = true;
            yield return new WaitForSecondsRealtime(5);
            if (playerCollider.OverlapPoint(mousePos))
            {
                canMove = false;
                agarrado = false;
                frozen = true;
                StartCoroutine(Congelado());
            }
        }else
        {
            congelando = false;
        }
        frozen = false;
    }
    private IEnumerator Congelado(){
        canMove = false;
        agarrado = false;
        frozen = true;
        yield return new WaitForSecondsRealtime(2);
        canMove = true;
        agarrado = true;
        frozen = false;
        congelando = false;
    }
}
