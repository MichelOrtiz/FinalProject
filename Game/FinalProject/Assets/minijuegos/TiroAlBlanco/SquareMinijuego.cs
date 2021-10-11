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
    public int dif;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        dif = 0;
        speed = Random.Range(100,250);
    }
    void Update()
    {
        transform.position += (Vector3)rotation * speed * Time.unscaledDeltaTime;
        if (transform.position.x<149 || transform.position.x>851 || transform.position.y<99 || transform.position.y>451)
        {
            Destroy(gameObject);
        }
    }
    public void Pres()
    {
        dif++;
        if (dif == 2)
        {
            Destroy(gameObject);
            ScoreController.score++;
        }else
        {
            speed = speed/2;
            transform.position += (Vector3)rotation * speed * Time.unscaledDeltaTime;
        }
    }
}
