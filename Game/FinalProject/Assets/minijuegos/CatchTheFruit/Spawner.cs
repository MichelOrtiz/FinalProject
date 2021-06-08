using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] fruits;
    public GameObject bomb;
    public GameObject square;
    public GameObject parents;
    public float xBound1, xBound2, xBounds, yBound1, yBound2, yBounds;
    public float speed;
    public bool cuadradito;
    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());
    }
    IEnumerator SpawnRandomGameObject(){
        yield return new WaitForSeconds(Random.Range(1,2));
        if(cuadradito){
            Instantiate(square,  new Vector2(Random.Range(xBound1, xBound2), Random.Range(yBound1, yBound2)), Quaternion.identity, transform.parent);
        }else
        {
            int randomFruit = Random.Range(0,fruits.Length);
            if(Random.value <= .6f){
                Instantiate(fruits[randomFruit], new Vector2(Random.Range(xBound1, xBound2), Random.Range(yBound1, yBound2)), Quaternion.identity);
            }else{
                Instantiate(bomb, new Vector2(Random.Range(xBound1, xBound2), Random.Range(yBound1, yBound2)), Quaternion.identity);
            }
        }
        DestroyGameObject();
        StartCoroutine(SpawnRandomGameObject());
    }
    IEnumerator DestroyGameObject(){
        yield return new WaitForSeconds(10);
        if(tag == "Fruit" || tag == "Square"){
            Destroy(gameObject);
        }
    }
}
