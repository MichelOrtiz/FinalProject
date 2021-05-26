using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionMejorada : Ability
{
    public GameObject SpriteMask;
    protected override void Update() {
        if (isUnlocked)
        {
            SpriteMask.transform.localScale=new Vector3(10,10,0);
        }else
        {
            SpriteMask.transform.localScale=new Vector3(5,5,0);
        }
    }
}
