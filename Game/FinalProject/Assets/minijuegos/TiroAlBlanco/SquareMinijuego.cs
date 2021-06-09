using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SquareMinijuego : MasterMinigame
{
    private enum Tipo{Cuadrado}
    [SerializeField] Tipo tipo;
    [SerializeField] Vector2 rotation;
    int speed;
    Rigidbody2D Body;
    private int score;
    public int dif;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        dif = 0;
        speed = Random.Range(100,250);
    }
    void Update()
    {
        transform.position += (Vector3)rotation * speed * Time.deltaTime;
        if (transform.position.x<99 || transform.position.x>651 || transform.position.y<49 || transform.position.y>401)
        {
            Destroy(gameObject);
        }
    }
    public void Pres(){
        Debug.Log("SePudo");
        dif++;
        if (dif == 2)
        {
            Destroy(gameObject);
            ScoreController.score++;
            if (ScoreController.score == 5)
            {
                OnWinMinigame();
            }
        }else
        {
            speed = speed/2;
            transform.position += (Vector3)rotation * speed * Time.deltaTime;
        }
    }
}
