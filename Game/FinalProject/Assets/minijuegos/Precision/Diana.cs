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

    // Update is called once per frame
    void Update()
    {
    }
    /*void OnTriggerEnter2D(Collider2D target){
        Debug.Log("Punto");
        if (target.tag == "Fruit")
        {
            GetComponent<Collider2D>().enabled=false;
            activado = false;
            StartCoroutine(SpawnRandomGameObject(target));
            Debug.Log("Increased score");
            ScoreController.score++;
        }
    }*/
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
