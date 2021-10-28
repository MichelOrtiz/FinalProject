using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : IntRemoteTrigger
{
    [SerializeField] WorldState isTutorialDone;
    protected override void Start()
    {
        cola = new Queue<Interaction>();
        busy = false;
        SaveFile partida = SaveFilesManager.instance.currentSaveSlot;
        if(partida.WorldStates.Exists(x => x.id == isTutorialDone.id)){
            WorldState w = partida.WorldStates.Find(x => x.id == isTutorialDone.id);
            if(w.state){
                Destroy(this);
            }
        }else{
            partida.WorldStates.Add(isTutorialDone);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(triggerPoint.position, radius);
    }
}
