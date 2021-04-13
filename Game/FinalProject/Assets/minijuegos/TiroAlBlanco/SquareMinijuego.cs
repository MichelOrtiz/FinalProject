using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SquareMinijuego : MonoBehaviour
{
    private enum Tipo{Cuadrado}
    [SerializeField] Tipo tipo;
    [SerializeField] Vector2 rotation;
    int speed;
    Rigidbody2D Body;
    private int score;
    private int dif;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        dif = 0;
        speed = Random.Range(10,15);
    }
    void Update()
    {
        transform.position += (Vector3)rotation * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D target){
        if (target.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
    void OnMouseDown(){
        dif++;
        if (dif == 2)
        {
            Destroy(gameObject);
            ScoreController.score++;
        }else
        {
            speed = speed/2;
            transform.position += (Vector3)rotation * speed * Time.deltaTime;
        }
    }
}
