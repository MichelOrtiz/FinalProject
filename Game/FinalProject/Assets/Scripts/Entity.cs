using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Entity : MonoBehaviour
{
    public bool isParalized;
    public bool isCaptured;
    public bool isInFear;
    public bool isDizzy;
    public bool isBrainFrozen;
    public bool isResting;
    public bool isChasing;

    public IEnumerator Paralized(int time)
    {
        // paralized animation here
        isParalized = true;
        yield return new WaitForSeconds(time);
        isParalized = false;
    }
    IEnumerator Captured(int time)
    {
        
    }
    IEnumerator Fear(int time);
    IEnumerator Dizzy(int time);
    IEnumerator FrozenBragvin(int time);
    IEnumerator Rest(int time);
    IEnumerator Chase(int time);
}