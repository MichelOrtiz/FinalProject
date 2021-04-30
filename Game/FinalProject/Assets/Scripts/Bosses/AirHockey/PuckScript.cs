using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    public ScoreScript ScoreScriptInstance;
    
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
            rb.position = new Vector2(2, 0);
        }
    }

    public void CenterPuck(){
        rb.position = new Vector2(0,0);
    }
    private void FixedUpdate(){
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    }
}
