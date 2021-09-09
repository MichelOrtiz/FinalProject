using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPosition : MonoBehaviour
{
    PlayerManager player;
    void Start()
    {
        FindStartPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnLevelWasLoaded(int level){
        //base.Start();
        FindStartPos();
        /*players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length >1)
        {
            Destroy(players[1]);
        }*/
    }

    void FindStartPos(){
        try
        {
            var spawnPos = GameObject.FindWithTag("RespawnPos").transform.position;
            if (spawnPos != null)
            {
                transform.position = spawnPos;
            }
        }
        catch (System.NullReferenceException)
        {
            
        }
    }
}
