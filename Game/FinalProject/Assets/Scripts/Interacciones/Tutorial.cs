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
                Destroy(gameObject);
            }
        }else{
            partida.WorldStates.Add(isTutorialDone);
        }
    }
    protected override void NextInteraction(){
        if(cola.Count == 0){
            Debug.Log("No hay mas interacciones");
            Destroy(gameObject);
            busy = false;
            return;
        }
        Interaction inter = cola.Dequeue();
        Debug.Log("Current interaction: " + inter.name);
        if(inter.condition != null){
            inter.condition.RestardValues(gameObject);
        }
        inter.DoInteraction();
        lastInter = inter;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(triggerPoint.position, radius);
    }
}
