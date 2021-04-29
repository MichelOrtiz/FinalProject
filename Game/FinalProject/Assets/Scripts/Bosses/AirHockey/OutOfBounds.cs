using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    
    public PuckScript puckScript;
    public AirHockeyPlayerMovement airHockeyPlayerMovement;
    public AIScript aIScript;
    public void OnTriggerExit2D(){
        puckScript.CenterPuck();
        airHockeyPlayerMovement.CenterPosition();
        aIScript.CenterPosition();
    }
}
