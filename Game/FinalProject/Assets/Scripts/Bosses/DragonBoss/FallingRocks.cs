using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{
    public static FallingRocks instance;
    private void Awake() {
        if(instance!=null){
            return;
        }
        instance=this;
    }
    [SerializeField]private Transform[] positions;
    [SerializeField]private GameObject rockPrefab;
    private int prevRandom = 0;

    public void GenerateRandomRock(){
        int min = (int) positions[0].position.x;
        int max = (int) positions[1].position.x;
        int r = RandomGenerator.NewRandom(min,max);
        while(r==prevRandom) r=RandomGenerator.NewRandom(min,max);
        Vector2 spawnPosition = new Vector2(r,positions[0].position.y);
        Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        prevRandom = r;
    }
}
