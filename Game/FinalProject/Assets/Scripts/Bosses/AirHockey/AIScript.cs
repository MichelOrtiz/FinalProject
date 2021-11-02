using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject.Assets.Scripts.Bosses.AirHockey
{
    public class AIScript : MonoBehaviour
    {
        [Header("References")]
        private Rigidbody2D rb;
        private Vector2 startingPosition;
        public Rigidbody2D Puck;


        //public float MaxMovementSpeed;


        [Header("Boundaries")]
        public Transform PlayerBoundaryHolder;
        private Boundary playerBoundary;
        public Transform PuckBoundaryHolder;
        private Boundary puckBoundary;


        [SerializeField] private Vector2 targetPosition;
        private bool isFirstHalfPass = true;

        
        [Header("Offset")]
        [SerializeField] private float minOffset = -5, maxOffset = 5, minOffsetMod, maxOffsetMod;
        private float offsetYFromTarget;
        public float PUoffset;

        

        [Header("Speed")]
        public float aIMaxMovementSpeed = 30;
        [SerializeField] private float returnSpeed;

        private void Start(){
            rb = GetComponent<Rigidbody2D>();
            startingPosition = rb.position;
            playerBoundary = new Boundary(PlayerBoundaryHolder.GetChild(0).position.y,
                                        PlayerBoundaryHolder.GetChild(1).position.y,
                                        PlayerBoundaryHolder.GetChild(2).position.x,
                                        PlayerBoundaryHolder.GetChild(3).position.x);
            playerBoundary = new Boundary(PuckBoundaryHolder.GetChild(0).position.y,//weird
                                        PuckBoundaryHolder.GetChild(1).position.y,
                                        PuckBoundaryHolder.GetChild(2).position.x,
                                        PuckBoundaryHolder.GetChild(3).position.x);
            //MaxMovementSpeed = 0;
        }

        private void FixedUpdate(){
            if (!PuckScript.WasGoal)
            { 
                float movementSpeed;
                /*switch (scoreScript.playerScore)
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
                }*/
                if (Puck.position.x < puckBoundary.Up)
                {
                    if (isFirstHalfPass)
                    {
                        isFirstHalfPass = false;
                        offsetYFromTarget = Random.Range(minOffset, maxOffset);
                        PUoffset = Random.Range(PUoffset, -PUoffset);
                    }
                    movementSpeed = aIMaxMovementSpeed * returnSpeed;
                    targetPosition = new Vector2(startingPosition.x, Mathf.Clamp(Puck.position.y + offsetYFromTarget + PUoffset, playerBoundary.Down, playerBoundary.Up));
                }else
                {
                    isFirstHalfPass = true;
                    movementSpeed = Random.Range(aIMaxMovementSpeed * returnSpeed, aIMaxMovementSpeed);
                    targetPosition = new Vector2(Mathf.Clamp(Puck.position.x, playerBoundary.Left, playerBoundary.Right),
                                                Mathf.Clamp(Puck.position.y, playerBoundary.Down, playerBoundary.Up));
                }
                rb.MovePosition(Vector2.MoveTowards(rb.position,targetPosition, movementSpeed * Time.fixedDeltaTime));
            }
        }
        
        public void  CenterPosition(){
            rb.position = startingPosition;
        }

        public void ModValues()
        {
            minOffset *= minOffsetMod;
            maxOffset *= maxOffsetMod;
            minOffset *= minOffsetMod;
            maxOffsetMod *= maxOffsetMod;
        }
    }
}