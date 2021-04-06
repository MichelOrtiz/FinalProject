using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] fruits;
    public GameObject bomb;
    public float xBound1, xBound2, yBounds;
    public float speed;
    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());
    }
    IEnumerator SpawnRandomGameObject(){
        yield return new WaitForSeconds(Random.Range(1,2));
        int randomFruit = Random.Range(0,fruits.Length);
        if(Random.value <= .6f){
            Instantiate(fruits[randomFruit], new Vector2(Random.Range(xBound1, xBound2), yBounds), Quaternion.identity);
        }else{
            Instantiate(bomb, new Vector2(Random.Range(xBound1, xBound2), yBounds), Quaternion.identity);
        }
        StartCoroutine(SpawnRandomGameObject());
    }
    IEnumerator DestroyGameOnject(){
        yield return new WaitForSeconds(10);
        if(tag == "Fruit"){
            Destroy(gameObject);
        }
    }
}
