using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    [SerializeField] float AumentoVel, AumentoAI;
    public CollisionHandler collisionHandler;
    public GameObject PuckGO, Ai;
    public ScoreScript ScoreScriptInstance;
    PowerUps powerUps;
    AIScript aIScript;

    public float MaxSpeed;
    public static bool WasGoal{
        get; 
        private set;
    }
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;
        collisionHandler.ChangedColliderTagHandler += collisionHandler_Changedtag;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (!WasGoal)
        {
            if (other.tag == "Bomb")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                WasGoal = true;
                StartCoroutine(ResetPuck(false));
            }else if(other.tag == "Fruit")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                WasGoal = true;
                StartCoroutine(ResetPuck(true));
            }
            
        }
    }
    private IEnumerator ResetPuck(bool AIScored){
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        rb.velocity = rb.position = new Vector2(0,0);
        if (AIScored)
        {
            rb.position = new Vector2(-2, 0);
        }else
        {
            rb.position = new Vector2(3, 0);
        }
        switch (ScoreScriptInstance.playerScore)
            {
                case 0:    
                    PuckGO.gameObject.GetComponent<PuckScript>().MaxSpeed += AumentoVel;
                    Ai.gameObject.GetComponent<AIScript>().aIMaxMovementSpeed += AumentoAI;
                    break;
                case 1:
                    PuckGO.gameObject.GetComponent<PuckScript>().MaxSpeed += AumentoVel;
                    Ai.gameObject.GetComponent<AIScript>().aIMaxMovementSpeed += AumentoAI;
                    break;
                case 2:
                    PuckGO.gameObject.GetComponent<PuckScript>().MaxSpeed += AumentoVel;
                    Ai.gameObject.GetComponent<AIScript>().aIMaxMovementSpeed += AumentoAI;
                    break;
                case 3:
                    PuckGO.gameObject.GetComponent<PuckScript>().MaxSpeed += AumentoVel;
                    Ai.gameObject.GetComponent<AIScript>().aIMaxMovementSpeed += AumentoAI;
                    break;
                case 4:
                    PuckGO.gameObject.GetComponent<PuckScript>().MaxSpeed += AumentoVel;
                    Ai.gameObject.GetComponent<AIScript>().aIMaxMovementSpeed += AumentoAI;
                    break;
            }
    }

    public void CenterPuck(){
        rb.position = new Vector2(0,0);
    }
    private void FixedUpdate(){
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    }
    
    private void collisionHandler_Changedtag(){
        Debug.Log(collisionHandler.lastColliderTag);
        
    }
}
