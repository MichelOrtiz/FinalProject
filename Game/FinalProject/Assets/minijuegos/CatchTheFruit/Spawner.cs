using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] fruits;
    public GameObject bomb;
    public GameObject square;
    public Transform parents;
    public float xBound1, xBound2, yBound1, yBound2;
    float time = 1;
    public bool cuadradito;
    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());
    }
    IEnumerator SpawnRandomGameObject(){
        //Debug.Log("NoPaseElYield");
        //Debug.Log("PaseElYield");
        if(cuadradito){
            yield return new WaitForSecondsRealtime(.5f);
            var position = new Vector2 (Random.Range(xBound1, xBound2), Random.Range(yBound1, yBound2));
            var obj = Instantiate(square);
            obj.transform.SetParent(parents);
            obj.transform.localPosition = position;
        }else
        {
            yield return new WaitForSeconds(Random.Range(time -.5f, 1f));
            int randomFruit = Random.Range(0,fruits.Length);
            if(Random.value <= .8f){
                Instantiate(fruits[randomFruit], new Vector2(Random.Range(xBound1, xBound2), Random.Range(yBound1, yBound2)), Quaternion.identity);
            }else{
                Instantiate(bomb, new Vector2(Random.Range(xBound1, xBound2), Random.Range(yBound1, yBound2)), Quaternion.identity);
            }
            time -= .05f;
        }
        StartCoroutine(SpawnRandomGameObject());
        DestroyGameObject();
        //yield return new WaitForSeconds(Random.Range(1,2));
    }
    IEnumerator DestroyGameObject(){
        StartCoroutine(SpawnRandomGameObject());
        yield return new WaitForSeconds(5);
        if(tag == "Fruit" || tag == "Square"){
            Destroy(gameObject);
        }
    }
    
}
