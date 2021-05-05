using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject Puck, Player, AI;
    AirHockeyPlayerMovement airHockeyPlayerMovement;
    PuckScript puckScript;
    AIScript aIScript;
    float PUMaxMovementSpeed, MaxPuckSpeed;
    int powerUpType;
    private void OnTriggerEnter2D(Collider2D othe){
        if (othe.tag == "Spit")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            powerUpType = 2;
            switch (powerUpType)
            {
                case 1:
                    Debug.Log(powerUpType);
                    MaxPuckSpeed = Puck.gameObject.GetComponent<PuckScript>().MaxSpeed;
                    Puck.gameObject.GetComponent<PuckScript>().MaxSpeed=MaxPuckSpeed*2;
                    StartCoroutine(TimePowerUpActive(5));
                break;
                case 2:
                    Debug.Log(powerUpType);
                    Player.gameObject.GetComponent<AirHockeyPlayerMovement>().playerSpeed = 10000000;
                    PUMaxMovementSpeed = AI.gameObject.GetComponent<AIScript>().aIMaxMovementSpeed;
                    AI.gameObject.GetComponent<AIScript>().aIMaxMovementSpeed = PUMaxMovementSpeed * 1.5f;

                    StartCoroutine(TimePowerUpActive(5));
                break;
                case 3:
                    Debug.Log(powerUpType);
                    StartCoroutine(TimePowerUpActive(2));
                break;
                case 4:
                    Debug.Log(powerUpType);
                    if (othe.tag == "Fruit")
                    {
                        Player.gameObject.GetComponent<AirHockeyPlayerMovement>().enabled = false;
                    }else if(othe.tag == "Bomb")
                    {
                        AI.gameObject.GetComponent<AIScript>().enabled = false;
                    }
                    StartCoroutine(TimePowerUpActive(3));
                break;
                case 5:
                    Debug.Log(powerUpType);
                    
                    StartCoroutine(TimePowerUpActive(5));
                break;
            }
        }
    }
    private IEnumerator Respawn(){
        yield return new WaitForSecondsRealtime(15);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        powerUpType = Random.Range(1,2);
    }
    public void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        powerUpType = Random.Range(1,2);
    }
    void Update()
    {
        
    }
    private IEnumerator TimePowerUpActive(float Time){
        yield return new WaitForSecondsRealtime(Time);
        switch (powerUpType)
        {
            case 1:
                Puck.gameObject.GetComponent<PuckScript>().MaxSpeed=MaxPuckSpeed;
                StartCoroutine(Respawn());
            break;
            case 2:
                Player.gameObject.GetComponent<AirHockeyPlayerMovement>().playerSpeed = 50;
                AI.gameObject.GetComponent<AIScript>().aIMaxMovementSpeed = PUMaxMovementSpeed;
                StartCoroutine(Respawn());
            break;
            case 3:
                StartCoroutine(Respawn());
            break;
            case 4:
                AI.gameObject.GetComponent<AIScript>().enabled = true;
                Player.gameObject.GetComponent<AirHockeyPlayerMovement>().enabled = true;
                StartCoroutine(Respawn());
            break;
            case 5:

                StartCoroutine(Respawn());
            break;
        }
    }
}
