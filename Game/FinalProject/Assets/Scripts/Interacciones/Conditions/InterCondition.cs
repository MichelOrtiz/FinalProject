using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterCondition : ScriptableObject {
    [System.NonSerialized] public GameObject gameObject;
    public bool isDone {get => checkIsDone();}
    protected abstract bool checkIsDone();
    public virtual void RestardValues(GameObject gameObject){
        this.gameObject = gameObject;
        //No todas las condiciones necesitan reiniciar variables
    }
}
