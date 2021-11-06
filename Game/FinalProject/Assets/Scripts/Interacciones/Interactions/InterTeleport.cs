using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/Teleport")]
public class InterTeleport : Interaction
{
    [SerializeField] Vector3 teleport;
    public override void DoInteraction()
    {
        if(condition!=null){
            if(condition.isDone){
                Teleport();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            Teleport();
        }
    }

    void Teleport(){
        PlayerManager.instance.gameObject.transform.position = teleport;
        onEndInteraction?.Invoke();
    }
}
