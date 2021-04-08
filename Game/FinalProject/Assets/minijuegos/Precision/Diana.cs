using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    public bool activado;
    void Start()
    {
        activado = false;        
    }

    // Update is called once per frame
    void Update(Collider2D target)
    {
    }
    void OnTriggerEnter2D(Collider2D target){
        if (target.tag == "Fruit")
        {
            StartCoroutine(SpawnRandomGameObject(target));
            ScoreController.score++;
        }
        
    }
    IEnumerator SpawnRandomGameObject(Collider2D target){
        yield return new WaitForSeconds(.5f);
        GetComponent<Collider2D>().enabled=false;
    }
}
