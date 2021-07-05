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
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
        //transform.position = loadlevel.instance.startPosition.transform.position;
    }
}
