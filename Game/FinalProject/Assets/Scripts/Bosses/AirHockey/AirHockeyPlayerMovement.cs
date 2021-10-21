using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CollisionHandler))]
public class AirHockeyPlayerMovement : MonoBehaviour
{
    public float playerSpeed = 50;
    public bool hitPuck;
    public Transform BoundaryHolder;
    private Boundary playerBoundary;
    public float offset;
    
    [SerializeField] private float startDelay;
    
    private CollisionHandler collisionHandler;
    private Rigidbody2D rb;
    private Vector2 centerPosition;
    private Vector2 mousePos;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionHandler = GetComponent<CollisionHandler>();
        
        /*collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterContact;
        collisionHandler.ExitTouchingContactHandler += collisionHandler_ExitContact;*/
    }

    void Start()
    {
        hitPuck = false;
        centerPosition = rb.position;
        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);
    }

    void Update()
    {
        if (startDelay > 0)
        {
            startDelay -= Time.deltaTime;
            return;
        }
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x + Random.Range(offset, -offset), playerBoundary.Left, playerBoundary.Right),
                                                        Mathf.Clamp(mousePos.y + Random.Range(offset, -offset), playerBoundary.Down, playerBoundary.Up));

               //
            if (hitPuck)
            {
                rb.MovePosition(clampedMousePos);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, clampedMousePos, Time.deltaTime*playerSpeed);
            }
        }
    }
    public void  CenterPosition()
    {
        rb.position = centerPosition;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Spit")
        {   
            hitPuck = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        hitPuck = false;
    }
}
