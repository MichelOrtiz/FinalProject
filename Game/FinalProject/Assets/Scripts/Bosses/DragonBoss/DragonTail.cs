using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTail : ItemGetter
{
    [SerializeField]private BossFight manager;
    private void Start() {
        manager=BossFight.instance;
    }
    protected override void Interaction()
    {
        manager.NextStage();
    }
}
