using UnityEngine;

namespace FinalProject.Assets.Scripts.Bosses.AirHockey
{
    public class OutOfBounds : MonoBehaviour
    {
        
        public PuckScript puckScript;
        public AirHockeyPlayerMovement airHockeyPlayerMovement;
        public AIScript aIScript;
        public void OnTriggerEnter2D(Collider2D other){
            if (other.tag == "Spit"){
                puckScript.CenterPuck();
                airHockeyPlayerMovement.CenterPosition();
                aIScript.CenterPosition();
            }
        }
    }
}
