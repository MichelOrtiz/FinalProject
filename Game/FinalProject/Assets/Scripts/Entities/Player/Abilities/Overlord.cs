using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : Ability
{
    public bool IsOverlording = false;
    protected override void Update() {
        if (isUnlocked)
        {
            IsOverlording = true;
        }else
        {
            IsOverlording = false;
        }
    }
}