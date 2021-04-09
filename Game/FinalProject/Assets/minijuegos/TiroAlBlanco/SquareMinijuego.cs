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
    [SerializeField] int speed;
    Rigidbody2D Body;
    private int score;
    public UnityEvent buttonClick;
    void Awake(){
        if (buttonClick==null)
        {
            buttonClick = new UnityEvent();
        }
    }
    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        speed = Random.Range(10,15);
        transform.position += (Vector3)rotation * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D target){
        if (target.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
    void onMouseUp(){
        buttonClick.Invoke();
    }
}
