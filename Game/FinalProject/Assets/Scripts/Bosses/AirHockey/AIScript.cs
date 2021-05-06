using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public GameObject PuckGO;
    public float MaxMovementSpeed;
    private Rigidbody2D rb;
    private Vector2 startingPosition;
    public Rigidbody2D Puck;
    public Transform PlayerBoundaryHolder;
    private Boundary playerBoundary;
    public Transform PuckBoundaryHolder;
    private Boundary puckBoundary;
    private Vector2 targetPosition;
    private bool isFirstHalfPass = true;
    private float offsetYFromTarget, offset1, offset2, returned;
    public float aIMaxMovementSpeed;
    public ScoreScript scoreScript;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;
        playerBoundary = new Boundary(PlayerBoundaryHolder.GetChild(0).position.y,
                                      PlayerBoundaryHolder.GetChild(1).position.y,
                                      PlayerBoundaryHolder.GetChild(2).position.x,
                                      PlayerBoundaryHolder.GetChild(3).position.x);
        playerBoundary = new Boundary(PuckBoundaryHolder.GetChild(0).position.y,
                                      PuckBoundaryHolder.GetChild(1).position.y,
                                      PuckBoundaryHolder.GetChild(2).position.x,
                                      PuckBoundaryHolder.GetChild(3).position.x);
        MaxMovementSpeed = 0;
        offset1 = -5;
        offset2 = 5;
        aIMaxMovementSpeed = 30;
    }

    private void FixedUpdate(){
        if (!PuckScript.WasGoal)
        { 
            float movementSpeed;
            switch (scoreScript.playerScore)
            {
                case 0:    
                    MaxMovementSpeed = aIMaxMovementSpeed;
                    offset1 = -8;
                    offset2 = 8;
                    returned = .3f;
                    break;
                case 1:
                    MaxMovementSpeed = aIMaxMovementSpeed;
                    offset1 = -6f;
                    offset2 = 6f;
                    returned = .3f;
                    break;
                case 2:
                    MaxMovementSpeed = aIMaxMovementSpeed;
                    offset1 = -4;
                    offset2 = 4;
                    returned = .5f;
                    break;
                case 3:
                    MaxMovementSpeed = aIMaxMovementSpeed;
                    offset1 = -2f;
                    offset2 = 2f;
                    returned = .5f;
                    break;
                case 4:
                    MaxMovementSpeed = aIMaxMovementSpeed;
                    offset1 = -1f;
                    offset2 = 1f;
                    returned = .7f;
                    break;
            }
            if (Puck.position.x < puckBoundary.Up)
            {
                if (isFirstHalfPass)
                {
                    isFirstHalfPass = false;
                    offsetYFromTarget = Random.Range(offset1, offset2);
                }
                movementSpeed = MaxMovementSpeed * returned;
                targetPosition = new Vector2(startingPosition.x, Mathf.Clamp(Puck.position.y + offsetYFromTarget, playerBoundary.Down, playerBoundary.Up));
            }else
            {
                isFirstHalfPass = true;
                movementSpeed = Random.Range(MaxMovementSpeed * returned, MaxMovementSpeed);
                targetPosition = new Vector2(Mathf.Clamp(Puck.position.x, playerBoundary.Left, playerBoundary.Right),
                                            Mathf.Clamp(Puck.position.y, playerBoundary.Down, playerBoundary.Up));
            }
            rb.MovePosition(Vector2.MoveTowards(rb.position,targetPosition, movementSpeed * Time.fixedDeltaTime));
        }
    }
    
    public void  CenterPosition(){
        rb.position = startingPosition;
    }
}
