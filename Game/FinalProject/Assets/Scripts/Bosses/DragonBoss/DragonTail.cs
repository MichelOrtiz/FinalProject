using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTail : ItemGetter
{
    [SerializeField]private BossFight manager;
    protected override void Interaction()
    {
        manager.NextStage();
    }
}
