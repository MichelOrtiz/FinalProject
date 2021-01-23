using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    public delegate void EnemyTouchedPlayer();
    public event EnemyTouchedPlayer OnEnemyTouchedPlayer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
