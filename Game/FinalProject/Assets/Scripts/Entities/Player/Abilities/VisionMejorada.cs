using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEngine.Experimental.Rendering.Universal.Light2D;
using UnityEngine.Experimental.Rendering.Universal;

public class VisionMejorada : Ability
{
    PlayerManager Player;
    [SerializeField] Light2D lt;
    [SerializeField] float Range;
    private void Awake()
    {
        Range = lt.pointLightOuterRadius;
    }
    protected override void Update() {
        if (!isUnlocked)
        {
            Range = 3;
        }else
        {
            Range = 7;
        }
        lt.pointLightOuterRadius = Range;
    }
    void OnEnable(){
    }
}
