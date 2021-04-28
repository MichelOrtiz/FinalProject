using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    private float MaxMovementSpeed;
    private Rigidbody2D rb;
    private Vector2 startingPosition;
    public Rigidbody2D Puck;
    public Transform PlayerBoundaryHolder;
    private Boundary playerBoundary;
    public Transform PuckBoundaryHolder;
    private Boundary puckBoundary;
    private Vector2 targetPosition;
    private bool isFirstHalfPass = true;
    private float offsetYFromTarget, offset1, offset2;
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
        MaxMovementSpeed = 10;
        offset1 = -5;
        offset2 = 5;
    }

    private void FixedUpdate(){
        if (!PuckScript.WasGoal)
        { 
            float movementSpeed;
            switch (scoreScript.playerScore)
            {
                case 0:
                    MaxMovementSpeed = 10;
                    offset1 = -5;
                    offset2 = 5;
                    break;
                case 1:
                    MaxMovementSpeed = 20;
                    offset1 = -4;
                    offset2 = 4;
                    break;
                case 2:
                    MaxMovementSpeed = 30;
                    offset1 = -3;
                    offset2 = 3;
                    break;
                case 3:
                    MaxMovementSpeed = 40;
                    offset1 = -2;
                    offset2 = 2;
                    break;
                case 4:
                    MaxMovementSpeed = 50;
                    offset1 = -1;
                    offset2 = 1;
                    break;
            }
            if (Puck.position.x < puckBoundary.Up)
            {
                if (isFirstHalfPass)
                {
                    isFirstHalfPass = false;
                    offsetYFromTarget = Random.Range(offset1, offset2);
                }
                movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);
                targetPosition = new Vector2(startingPosition.x, Mathf.Clamp(Puck.position.y + offsetYFromTarget, playerBoundary.Down, playerBoundary.Up));
            }else
            {
                isFirstHalfPass = true;
                movementSpeed = Random.Range(MaxMovementSpeed * 0.4f, MaxMovementSpeed);
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
