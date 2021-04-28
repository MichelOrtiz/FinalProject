using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornHead : MonoBehaviour
{
    PlayerManager player;
    GroundChecker playerGroundChecker;  
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
        playerGroundChecker = player.groundChecker;

        playerGroundChecker.ChangedGroundTagHandler += playerGroundChecker_ChangedGroundTagHandler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playerGroundChecker_ChangedGroundTagHandler()
    {
        Debug.Log("ChangedGroundChecker");
    }
}
