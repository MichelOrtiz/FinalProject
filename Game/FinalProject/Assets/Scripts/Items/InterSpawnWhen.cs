using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterSpawnWhen : Inter
{
    [SerializeField] InterCondition condition;
    protected override void Start()
    {
        base.Start();
        if(!condition.isDone){
            Destroy(gameObject);
        }
    }
}
