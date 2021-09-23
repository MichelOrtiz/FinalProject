using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemGetter : MonoBehaviour
{
    [SerializeField]protected Item neededItem;
    public bool GetItem(Item item){
        if(item == neededItem){
            Interaction();
            return true;
        }
        else{
            return false;
        }
    }
    protected abstract void Interaction();
}
