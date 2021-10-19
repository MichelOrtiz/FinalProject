using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterCondition : ScriptableObject {
    public bool isDone {get => checkIsDone();}
    protected abstract bool checkIsDone();
    public virtual void RestardValues(){
        //No todas las condiciones necesitan reiniciar variables
    }
}
