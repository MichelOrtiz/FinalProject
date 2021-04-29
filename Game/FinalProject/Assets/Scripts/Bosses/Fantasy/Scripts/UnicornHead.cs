using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornHead : MonoBehaviour
{
    PlayerManager player;
    GroundChecker playerGroundChecker;  
    [SerializeField] private List<UBLamp> lamps;
    [SerializeField] private GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
        playerGroundChecker = player.groundChecker;

        foreach (var lamp in lamps)
        {
            lamp.ActivatedHandler += lamp_Activated;
        }

        playerGroundChecker.ChangedGroundTagHandler += playerGroundChecker_ChangedGroundTagHandler;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = child.transform.localPosition;
    }

    void playerGroundChecker_ChangedGroundTagHandler()
    {
//        Debug.Log("ChangedGroundChecker");
    }

    void lamp_Activated()
    {
        Debug.Log("Activated");
    }
}
