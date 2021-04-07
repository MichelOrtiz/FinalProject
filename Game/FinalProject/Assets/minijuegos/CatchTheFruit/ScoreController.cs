using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    public static int score { get; set; }
    void Start()
    {
        scoreText.text = 0.ToString();

    }
    void Update()
    {
        scoreText.text ="Puntaje: "+ score.ToString();
    }
}
