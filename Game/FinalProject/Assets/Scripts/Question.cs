using UnityEngine;
[System.Serializable]
public class Question 
{
    public string pregunta;
    [TextArea(1, 2)]
    public string[] respuesta;
}
