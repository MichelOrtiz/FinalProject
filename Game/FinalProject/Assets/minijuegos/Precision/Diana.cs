using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    public float MaxDist;
    public bool activado;
    void Start()
    {
        ScoreController.score = 0;
        activado = false;        
    }
    IEnumerator SpawnRandomGameObject(Collider2D target){
        yield return new WaitForSeconds(.5f);
        GetComponent<Collider2D>().enabled=false;
    }
    public void CheckDistance(GameObject circle){
        if (Vector2.Distance(transform.position, circle.transform.position) <= MaxDist)
        {
            ScoreController.score++;
        }
    }
}
