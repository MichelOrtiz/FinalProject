using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
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
    void OnTriggerStay2D(Collider2D target){
        if (target.tag == "Fruit")
        {
            GetComponent<Collider2D>().enabled=false;
            activado = false;
            StartCoroutine(SpawnRandomGameObject(target));
            Debug.Log("Increased score");
            ScoreController.score++;
        }
        
    }
    IEnumerator SpawnRandomGameObject(Collider2D target){
        yield return new WaitForSeconds(.5f);
        GetComponent<Collider2D>().enabled=false;
    }
}
