using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FrutaMiniJuego : MonoBehaviour
{
    private enum Tipo{Fruta, Dinero}
    [SerializeField] float Grav;
    [SerializeField] float Tiempo;
    [SerializeField] Tipo tipo;
    float Tiempoact;
    Rigidbody2D Body;
    private int score;
    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Tiempoact > Tiempo)
        {
            Body.gravityScale += Grav;
            Tiempoact = 0;
        }else
        {
            Tiempoact += Time.deltaTime;
        }

    }
    void OnTriggerEnter2D(Collider2D target){
        if (target.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if (target.tag == "Player")
        {
            if (tipo == Tipo.Fruta)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }else
            {
                Destroy(gameObject);
                ScoreController.score++;
            }
        }
    }
}
