using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : IntRemoteTrigger
{
    [SerializeField] WorldState isTutorialDone;
    
    protected override void Start()
    {
        busy = true;
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
        foreach(Interaction inter in interactions){
            inter.gameObject = this.gameObject;
        }
        busy = false;
    }
    protected override void NextInteraction(){
        if(cola.Count == 0){
            currentInter = null;
            Debug.Log("No hay mas interacciones");
            busy = false;
            Destroy(gameObject);
            return;
        }
        Interaction inter = cola.Dequeue();
        currentInter = inter;
        Debug.Log("Current interaction: " + currentInter.name);
        inter.RestardCondition();
        inter.DoInteraction();
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(triggerPoint.position, radius);
    }
}
