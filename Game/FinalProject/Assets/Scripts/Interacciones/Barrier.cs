using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] InterCondition condition;
    private void Start() {
        condition.RestardValues(gameObject);
    }
    private void Update() {
        if(condition.isDone){
            Destroy(gameObject);
        }
    }
}
