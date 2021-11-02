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
    public int dif;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        dif = 0;
        speed = Random.Range(100,250);
    }
    void Update()
    {
        transform.localPosition += (Vector3)rotation * speed * Time.unscaledDeltaTime;
        if (transform.localPosition.x<-351 || transform.localPosition.x>351 || transform.localPosition.y<-201 || transform.localPosition.y>201)
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
            transform.localPosition += (Vector3)rotation * speed * Time.unscaledDeltaTime;
        }
    }
}
