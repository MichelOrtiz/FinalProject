using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject.Assets.Scripts.Bosses.AirHockey
{
    public class PuckScript : MonoBehaviour
    {
        [SerializeField] float AumentoVel, AumentoAI;
        public CollisionHandler collisionHandler;
        public GameObject PuckGO, Ai;
        public ScoreScript ScoreScriptInstance;
        AIScript aIScript;

        public float MaxSpeed;
        public static bool WasGoal{
            get; 
            private set;
        }
        private Rigidbody2D rb;

        public string lastTag;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            WasGoal = false;
            collisionHandler.ChangedColliderTagHandler += collisionHandler_Changedtag;

            aIScript = Ai.GetComponent<AIScript>();
        }
        private void OnTriggerEnter2D(Collider2D other){
            if (!WasGoal)
            {
                if (other.tag == "Bomb")
                {
                    ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                    aIScript.ModValues();
                    aIScript.aIMaxMovementSpeed += AumentoAI;
                    MaxSpeed += AumentoVel;
                    
                    //aIScript
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
        private IEnumerator ResetPuck(bool AIScored)
        {
            rb.velocity = new Vector2();
            yield return new WaitForSeconds(1f);
            WasGoal = false;
            rb.velocity = rb.position = new Vector2(0,0);
            if (AIScored)
            {
                rb.position = new Vector2(-2, 0);
            }else
            {
                rb.position = new Vector2(3, 0);
            }
        }

        public void CenterPuck(){
            rb.position = new Vector2(0,0);
        }
        private void FixedUpdate(){
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
        }
        
        private void collisionHandler_Changedtag()
        {
            var tag = collisionHandler.lastColliderTag;
            if (tag == "Enemy" || tag == "Player")
            {
                lastTag = tag;
            }
        }
    }
}
