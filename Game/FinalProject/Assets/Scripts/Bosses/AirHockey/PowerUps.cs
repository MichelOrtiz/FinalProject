using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject Player, AI;
    PuckScript puckScript;
    AIScript aIScript;
    float aIMaxMovementSpeed;
    int powerUpType;
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Spit")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            switch (powerUpType)
            {
                case 1:
                    Debug.Log(powerUpType);
                    puckScript.MaxSpeed=80;
                    TimePowerUpActive(5);
                break;
                case 2:
                    Debug.Log(powerUpType);
                    aIMaxMovementSpeed = aIScript.MaxMovementSpeed;
                    aIScript.MaxMovementSpeed = aIScript.MaxMovementSpeed * 1.5f;
                    TimePowerUpActive(5);
                break;
                case 3:
                    Debug.Log(powerUpType);
                    TimePowerUpActive(2);
                break;
                case 4:
                    Debug.Log(powerUpType);
                    if (other.tag == "Fruit")
                    {
                        Player.gameObject.GetComponent<AirHockeyPlayerMovement>().enabled = false;
                    }else if(other.tag == "Bomb")
                    {
                        AI.gameObject.GetComponent<AIScript>().enabled = false;
                    }
                    TimePowerUpActive(3);
                break;
                case 5:
                    Debug.Log(powerUpType);
                    
                    TimePowerUpActive(5);
                break;
            }
        }
    }
    private IEnumerator Respawn(){
        yield return new WaitForSecondsRealtime(15);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        powerUpType = Random.Range(1,5);
    }
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        powerUpType = Random.Range(1,5);
    }
    void Update()
    {
        
    }
    private IEnumerator TimePowerUpActive(float Time){
        yield return new WaitForSecondsRealtime(Time);
        switch (powerUpType)
        {
            case 1:
                puckScript.MaxSpeed=30;
                Respawn();
            break;
            case 2:
                aIScript.MaxMovementSpeed = aIMaxMovementSpeed;
                Respawn();
            break;
            case 3:
                Respawn(); 
            break;
            case 4:
                AI.gameObject.GetComponent<AIScript>().enabled = true;
                Player.gameObject.GetComponent<AirHockeyPlayerMovement>().enabled = true;
                Respawn();
            break;
            case 5:

                Respawn();
            break;
        }
    }
}
