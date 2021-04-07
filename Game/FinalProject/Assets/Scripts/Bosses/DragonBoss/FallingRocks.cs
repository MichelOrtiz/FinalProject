using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{
    [SerializeField]private Transform[] positions;
    [SerializeField]private GameObject rockPrefab;
    [SerializeField]private float intervalDrop;

    void GenerateRandomRock(){
        int r = RandomGenerator.NewRandom(0,positions.Length-1);
        Instantiate(rockPrefab,positions[r].position,Quaternion.identity);
    }

    void GenerateSequenceRock(){
        for(int i=0;i<positions.Length;i++){
            Instantiate(rockPrefab,positions[i].position,Quaternion.identity);
            StartCoroutine(intervalNextRock());
        }
    }

    IEnumerator intervalNextRock(){
        yield return new WaitForSeconds(intervalDrop);
    }
}
